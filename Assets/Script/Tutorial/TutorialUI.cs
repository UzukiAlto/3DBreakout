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
        /// 前のアニメーションがある場合は、達成演出を再生してから切り替える
        /// </summary>
        public void ShowVisual(TutorialVisualBase nextVisual)
        {
            if (nextVisual == null)
            {
                Debug.LogWarning("表示する TutorialVisual が未設定です。");
            }

            gameObject.SetActive(true);
            FadeTo(1f, null);

            TutorialVisualBase previousVisual = currentVisual;
            currentVisual = nextVisual;

            if (previousVisual == null)
            {
                ActivateVisual(nextVisual);
                return;
            }

            // 達成演出が終わってから次を出すことで、2つの画像が重なって見えるのを防ぐ
            PlayCompleteEffect(previousVisual, () =>
            {
                ActivateVisual(nextVisual);
            });
        }

        /// <summary>
        /// ステップ完了時のSEを再生する
        /// </summary>
        public void NotifyStepComplete()
        {
            // 正しいアクションを行えたことをプレイヤーに直感的にフィードバックするため
            // if (playSoundOnStepComplete)
            // {
            //     AudioManager.PlaySE(AudioManager.SEType.Select);
            // }
        }

        /// <summary>
        /// 現在表示中の操作アニメーションの達成演出を再生し、UIを非表示にします
        /// 次のステップが即時表示されない場合（遅延表示ステップ）に使用します
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

            PlayCompleteEffect(visualToDismiss, FadeOutAndDeactivate);
        }

        /// <summary>
        /// 最後のアニメーションの達成演出を再生した後、UI全体をフェードアウトして非表示にする
        /// </summary>
        public void HideUI()
        {
            TutorialVisualBase lastVisual = currentVisual;
            currentVisual = null;

            if (lastVisual == null)
            {
                FadeOutAndDeactivate();
                return;
            }

            PlayCompleteEffect(lastVisual, FadeOutAndDeactivate);
        }

        /// <summary>
        /// アニメーションを介さず即座に非表示状態へ初期化する
        /// </summary>
        public void ForceHideImmediate()
        {
            currentFadeTween?.Kill();
            // 演出途中で中断された場合も、演出対象の部品を確実に非表示にするため完了扱いで停止
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

            visual.gameObject.SetActive(true);
            visual.Play();
        }

        private void PlayCompleteEffect(TutorialVisualBase visual, Action onComplete)
        {
            // 前の切り替え演出が残っている場合は即座に完了させ、部品の表示状態の食い違いを防ぐ
            transitionTween?.Kill(true);

            visual.Stop();

            transitionTween = visual.transform
                .DOPunchScale(Vector3.one * completePunchStrength, completeEffectDuration, 6, 0.5f)
                .SetUpdate(true)
                .SetLink(gameObject)
                .OnComplete(() =>
                {
                    visual.gameObject.SetActive(false);
                    onComplete?.Invoke();
                });
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
