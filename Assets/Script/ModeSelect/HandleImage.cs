using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace ModeSelect
{
    public class HandleImage : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Color leftColor;
        [SerializeField] private Color rightColor;
        [SerializeField] private ConfigSlider configSlider;
        [SerializeField] private GameObject handleObject;
        private Image handleImage;
        private float handleRotate = 180f;

        private void Awake()
        {
            handleImage = handleObject.GetComponent<Image>();
        }
        private void OnEnable()
        {
            configSlider.OnValueChanged += ChangeHandleColor;
            // 初期値の通知より後に有効化された場合でも、表示時に色を現在値へ合わせるため
            ChangeHandleColor(configSlider.Value);
        }
        private void OnDisable()
        {
            configSlider.OnValueChanged -= ChangeHandleColor;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            transform.DOScale(0.9f, 0.1f)
                .SetEase(Ease.OutCubic)
                .SetLink(this.gameObject);
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            transform.DOScale(1.0f, 0.1f)
                .SetEase(Ease.InCubic)
                .SetLink(this.gameObject);
        }
        public void ChangeHandleColor(float value)
        {
            handleImage.color = Color.Lerp(leftColor, rightColor, value);
            float nextHandleRotate = Mathf.Lerp(handleRotate, -handleRotate, value);
            handleObject.transform.localEulerAngles = new Vector3(0, 0, nextHandleRotate);
        }
    }
}