using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Tutorial
{
    /// <summary>
    /// マウス画像のスプライト切り替え（通常時/クリック時）と左右移動で
    /// 「左クリックしたまま動かす（ドラッグ）」操作を示すアニメーション
    /// </summary>
    public class MouseDragVisual : TutorialVisualBase
    {
        [Header("References")]
        [SerializeField] private Image mouseImage;

        [Header("Sprites")]
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private Sprite clickedSprite;

        [Header("Motion Settings")]
        [SerializeField] private float moveRange = 40f;
        [SerializeField] private float moveDuration = 0.5f;
        [SerializeField] private float clickHoldDuration = 0.15f;
        [SerializeField] private float loopInterval = 0.4f;

        private Vector2 initialIconPosition;
        private bool isInitialized = false;
        private Sequence loopSequence;

        private void Awake()
        {
            CacheInitialState();
        }

        public override void Play()
        {
            // オブジェクトのアクティブ化タイミングに依存せず初期状態を安全に保持するため
            if (!isInitialized)
            {
                CacheInitialState();
            }

            Stop();

            if (mouseImage == null)
            {
                Debug.LogWarning("MouseDragVisual の参照が未設定のため、アニメーションを再生できません。");
                return;
            }

            RectTransform mouseTransform = mouseImage.rectTransform;
            float startX = initialIconPosition.x;

            // クリック → 右 → 左 → 中央 → クリック解除 の順にすることで、「押したまま動かす」ことを1枚の画像で表現
            loopSequence = DOTween.Sequence()
                .AppendCallback(() => SetClicked(true))
                .AppendInterval(clickHoldDuration)
                .Append(mouseTransform.DOAnchorPosX(startX + moveRange, moveDuration).SetEase(Ease.InOutSine))
                .Append(mouseTransform.DOAnchorPosX(startX - moveRange, moveDuration * 2f).SetEase(Ease.InOutSine))
                .Append(mouseTransform.DOAnchorPosX(startX, moveDuration).SetEase(Ease.InOutSine))
                .AppendCallback(() => SetClicked(false))
                .AppendInterval(loopInterval)
                .SetLoops(-1)
                .SetUpdate(true)
                .SetLink(gameObject);
        }

        public override void Stop()
        {
            loopSequence?.Kill();
            loopSequence = null;

            // Awakeより前にOnDisable経由で呼ばれた場合に、未取得の初期値で上書きしないため
            if (!isInitialized)
            {
                return;
            }

            SetClicked(false);
            mouseImage.rectTransform.anchoredPosition = initialIconPosition;
        }

        private void CacheInitialState()
        {
            if (mouseImage == null)
            {
                return;
            }

            // インスペクターで通常時スプライトが未指定の場合、現在のスプライトを初期値として保持するため
            if (defaultSprite == null)
            {
                defaultSprite = mouseImage.sprite;
            }

            initialIconPosition = mouseImage.rectTransform.anchoredPosition;
            isInitialized = true;
        }

        private void SetClicked(bool isClicked)
        {
            if (mouseImage == null)
            {
                return;
            }

            Sprite targetSprite = isClicked ? (clickedSprite != null ? clickedSprite : defaultSprite) : defaultSprite;
            if (targetSprite != null)
            {
                mouseImage.sprite = targetSprite;
            }
        }
    }
}
