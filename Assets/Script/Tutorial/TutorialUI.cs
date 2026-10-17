using System;
using UnityEngine;
using DG.Tweening;
using MainSystem;

namespace Tutorial
{
    /// <summary>
    /// チュートリアルの操作アニメーションの切り替えと演出（フェード・達成演出・SE）を制御するUIコンポーネント
    /// </summary>
    public class TutorialUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float fadeDuration = 0.3f;
        [SerializeField] private bool playSoundOnStepComplete = true;

        [Header("Complete Effect")]
        [SerializeField] private float completePunchStrength = 0.25f;
        [SerializeField] private float completeEffectDuration = 0.3f;

        private Tween currentFadeTween;
        private Tween transitionTween;
        private TutorialVisualBase currentVisual;

        private void Awake()
        {
            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
            }

            // シーン上で表示部品をアクティブのまま置いていても、最初は何も見えない状態から始めるため
            foreach (TutorialVisualBase visual in GetComponentsInChildren<TutorialVisualBase>(true))
            {
                visual.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// 操作アニメーションを切り替えて表示する
        /// 前のアニメーションを確実に停止・非表示にしてから新しいアニメーションを開始する
        /// </summary>
        public void ShowVisual(TutorialVisualBase nextVisual)
        {
            if (nextVisual == null)
            {
                Debug.LogWarning("[TutorialUI] 表示する TutorialVisual が未設定です。");
                return;
            }

            gameObject.SetActive(true);
            FadeTo(1f, null);

            TutorialVisualBase previousVisual = currentVisual;
            currentVisual = nextVisual;

            // 前の操作アニメーションが残っている場合、Tween完了待ちで次が詰まるのを防ぐため確実に片付ける
            if (previousVisual != null && previousVisual != nextVisual)
            {
                previousVisual.Stop();
                previousVisual.gameObject.SetActive(false);
            }

            ActivateVisual(nextVisual);
        }

        /// <summary>
        /// ステップ完了時のSEを再生する
        /// </summary>
        public void NotifyStepComplete()
        {
            // 正しいアクションを行えたことをプレイヤーに直感的にフィードバックするため
            if (playSoundOnStepComplete)
            {
                AudioManager.PlaySE(AudioManager.SEType.Select);
            }
        }

        /// <summary>
        /// 現在表示中の操作アニメーションを停止し、UIを非表示にする
        /// 次のステップが即時表示されない場合（遅延表示ステップ）に使用
        /// </summary>
        public void DismissCurrentVisual()
        {
            if (currentVisual == null)
            {
                FadeOutAndDeactivate();
                return;
            }

            TutorialVisualBase visualToDismiss = currentVisual;
            currentVisual = null;

            visualToDismiss.Stop();
            visualToDismiss.gameObject.SetActive(false);
            FadeOutAndDeactivate();
        }

        /// <summary>
        /// 操作アニメーションを停止し、UI全体をフェードアウトして非表示にする
        /// </summary>
        public void HideUI()
        {
            TutorialVisualBase lastVisual = currentVisual;
            currentVisual = null;

            if (lastVisual != null)
            {
                lastVisual.Stop();
                lastVisual.gameObject.SetActive(false);
            }

            FadeOutAndDeactivate();
        }

        /// <summary>
        /// アニメーションを介さず即座に非表示状態へ初期化する
        /// </summary>
        public void ForceHideImmediate()
        {
            currentFadeTween?.Kill();
            transitionTween?.Kill(true);

            if (currentVisual != null)
            {
                currentVisual.Stop();
                currentVisual.gameObject.SetActive(false);
                currentVisual = null;
            }

            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0f;
            }

            gameObject.SetActive(false);
        }

        private void ActivateVisual(TutorialVisualBase visual)
        {
            if (visual == null)
            {
                return;
            }

            Debug.Log($"[TutorialUI] ActivateVisual: {visual.gameObject.name}");
            visual.gameObject.SetActive(true);
            visual.Play();
        }

        private void FadeOutAndDeactivate()
        {
            FadeTo(0f, () =>
            {
                gameObject.SetActive(false);
            });
        }

        private void FadeTo(float targetAlpha, Action onComplete)
        {
            if (canvasGroup == null)
            {
                onComplete?.Invoke();
                return;
            }

            // 前のフェードアニメーションと競合しないよう停止
            currentFadeTween?.Kill();
            currentFadeTween = canvasGroup.DOFade(targetAlpha, fadeDuration)
                .SetUpdate(true)
                .SetLink(gameObject)
                .OnComplete(() =>
                {
                    onComplete?.Invoke();
                });
        }
    }
}
