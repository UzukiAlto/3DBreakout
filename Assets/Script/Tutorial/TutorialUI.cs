using UnityEngine;
using TMPro;
using DG.Tweening;
using MainSystem;

namespace Tutorial
{
    /// <summary>
    /// チュートリアルの案内メッセージ表示および演出（フェード・SE）を制御するUIコンポーネント
    /// </summary>
    public class TutorialUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float fadeDuration = 0.3f;
        [SerializeField] private bool playSoundOnStepComplete = true;

        private Tween currentFadeTween;

        private void Awake()
        {
            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
            }
        }

        /// <summary>
        /// 新しい指示メッセージを表示します
        /// </summary>
        public void ShowMessage(string message)
        {
            if (messageText != null)
            {
                messageText.text = message;
            }

            if (canvasGroup == null)
            {
                return;
            }

            // 前のフェードアニメーションと競合しないよう停止
            currentFadeTween?.Kill();

            gameObject.SetActive(true);
            currentFadeTween = canvasGroup.DOFade(1f, fadeDuration)
                .SetUpdate(true)
                .SetLink(gameObject);
        }

        /// <summary>
        /// ステップ完了時の演出を実行し、次のメッセージまたは終了へ繋ぎます
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
        /// チュートリアルUIをフェードアウトして非表示にします
        /// </summary>
        public void HideUI()
        {
            if (canvasGroup == null)
            {
                gameObject.SetActive(false);
                return;
            }

            currentFadeTween?.Kill();
            currentFadeTween = canvasGroup.DOFade(0f, fadeDuration)
                .SetUpdate(true)
                .SetLink(gameObject)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });
        }

        /// <summary>
        /// アニメーションを介さず即座に非表示状態へ初期化します
        /// </summary>
        public void ForceHideImmediate()
        {
            currentFadeTween?.Kill();

            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0f;
            }

            gameObject.SetActive(false);
        }
    }
}
