using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;

namespace ModeSelect
{
    public class ReturnButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        public UnityEvent onClick;

        private Image targetImage;
        private float defaultScale;

        [Header("Animation Settings")]
        [SerializeField] private float animationDuration;
        [SerializeField] private float pushScale;
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color hoverColor;

        void Awake()
        {
            targetImage = GetComponent<Image>();
            defaultScale = transform.localScale.x;

            if (targetImage != null)
            {
                targetImage.color = defaultColor;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (targetImage != null)
            {
                targetImage.DOColor(hoverColor, animationDuration);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (targetImage != null)
            {
                targetImage.DOColor(defaultColor, animationDuration);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            transform.DOScale(pushScale, animationDuration / 2f);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            transform.DOScale(defaultScale, animationDuration / 2f);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onClick?.Invoke();
        }
    }
}