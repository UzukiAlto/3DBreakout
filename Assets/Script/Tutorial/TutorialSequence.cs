using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    /// <summary>
    /// チュートリアルの各ステップを順番に実行・管理するシーケンサ
    /// 展示用として画面ごとの表示頻度（リセット/セッションスキップ）を制御します
    /// </summary>
    public class TutorialSequence : MonoBehaviour
    {
        [Header("UI & Steps")]
        [SerializeField] private TutorialUI tutorialUI;
        [SerializeField] private List<TutorialStepBase> steps = new List<TutorialStepBase>();

        [Header("Exhibition Settings")]
        [Tooltip("有効化されるたびに毎回チュートリアルを実行するか（モード選択画面等）")]
        [SerializeField] private bool resetOnEnable = false;

        [Tooltip("ゲーム画面用チュートリアルとしてセッション管理を行うか")]
        [SerializeField] private bool isGameScreenTutorial = false;

        // 展示会で同一プレイヤーがリトライした際に重複して案内しないよう、セッション単位で完了状態を保持
        private static bool hasCompletedGameTutorial = false;

        private int currentStepIndex = -1;
        private bool isRunning = false;

        private void OnEnable()
        {
            // モード選択画面など、画面が開くたびにプレイヤー交代の可能性があるため毎回リセット
            if (resetOnEnable)
            {
                StartSequence();
                return;
            }

            // ゲーム画面の場合、同じプレイヤーがリトライ中であればスキップ
            if (isGameScreenTutorial && hasCompletedGameTutorial)
            {
                if (tutorialUI != null)
                {
                    tutorialUI.ForceHideImmediate();
                }
                return;
            }

            StartSequence();
        }

        private void OnDisable()
        {
            // 画面遷移等で中断された際にイベント購読が残るのを防ぐため
            StopSequence();
        }

        /// <summary>
        /// チュートリアルシーケンスを先頭から開始します
        /// </summary>
        public void StartSequence()
        {
            StopSequence();

            if (steps == null || steps.Count == 0)
            {
                return;
            }

            isRunning = true;
            currentStepIndex = -1;
            AdvanceToNextStep();
        }

        /// <summary>
        /// チュートリアルシーケンスを中断し、後始末を行います
        /// </summary>
        public void StopSequence()
        {
            if (!isRunning)
            {
                return;
            }

            isRunning = false;

            if (currentStepIndex >= 0 && currentStepIndex < steps.Count)
            {
                TutorialStepBase activeStep = steps[currentStepIndex];
                if (activeStep != null)
                {
                    activeStep.OnStepCompleted -= HandleStepCompleted;
                    activeStep.ExitStep();
                }
            }

            if (tutorialUI != null)
            {
                tutorialUI.ForceHideImmediate();
            }
        }

        private void AdvanceToNextStep()
        {
            // 直前のステップの後始末
            if (currentStepIndex >= 0 && currentStepIndex < steps.Count)
            {
                TutorialStepBase previousStep = steps[currentStepIndex];
                if (previousStep != null)
                {
                    previousStep.OnStepCompleted -= HandleStepCompleted;
                    previousStep.ExitStep();
                }
            }

            currentStepIndex++;

            // 全ステップ完了
            if (currentStepIndex >= steps.Count)
            {
                OnAllStepsCompleted();
                return;
            }

            TutorialStepBase currentStep = steps[currentStepIndex];
            if (currentStep == null)
            {
                Debug.LogWarning($"インデックス {currentStepIndex} のステップが null のため、次のステップへスキップします。");
                AdvanceToNextStep();
                return;
            }

            if (tutorialUI != null)
            {
                tutorialUI.ShowMessage(currentStep.GuideMessage);
            }

            currentStep.OnStepCompleted += HandleStepCompleted;
            currentStep.EnterStep();
        }

        private void HandleStepCompleted()
        {
            if (!isRunning)
            {
                return;
            }

            if (tutorialUI != null)
            {
                tutorialUI.NotifyStepComplete();
            }

            AdvanceToNextStep();
        }

        private void OnAllStepsCompleted()
        {
            isRunning = false;

            if (isGameScreenTutorial)
            {
                hasCompletedGameTutorial = true;
            }

            if (tutorialUI != null)
            {
                tutorialUI.HideUI();
            }
        }

        /// <summary>
        /// 展示会で次のプレイヤーのためにゲーム用チュートリアルの完了状態をリセットします
        /// タイトル画面に戻った際などに呼び出します
        /// </summary>
        public static void ResetGameTutorialSession()
        {
            hasCompletedGameTutorial = false;
        }

        [ContextMenu("Reset All Tutorial Flags")]
        private void ResetAllFlagsForDebug()
        {
            hasCompletedGameTutorial = false;
            Debug.Log("[TutorialSequence] チュートリアルフラグをリセットしました。");
        }
    }
}
