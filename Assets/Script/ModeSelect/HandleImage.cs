using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using MainSystem;

namespace ModeSelect
{
    public class HandleImage : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Gradient handleGradient;
        [SerializeField] private ConfigSlider configSlider;
        [SerializeField] private GameObject handleObject;
        private Image handleImage;
        private float handleRotate = 180f;
        private float defaultHandleRotate = 45f;
        private float defaultScale;
        private float selectScale;

        private void Awake()
        {
            defaultScale = transform.localScale.x;
            selectScale = defaultScale * 0.9f;
            if (handleObject != null)
            {
                handleImage = handleObject.GetComponent<Image>();
            }
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
            AudioManager.PlaySE(AudioManager.SEType.Select);
            transform.DOScale(selectScale, 0.1f)
                .SetEase(Ease.OutCubic)
                .SetLink(this.gameObject);
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            transform.DOScale(defaultScale, 0.1f)
                .SetEase(Ease.InCubic)
                .SetLink(this.gameObject);
        }
        public void ChangeHandleColor(float value)
        {
            if (handleGradient != null && handleImage != null)
            {
                // スライダー値に応じた色をグラデーションから取得して反映
                handleImage.color = handleGradient.Evaluate(value);
            }

            float nextHandleRotate = defaultHandleRotate + Mathf.Lerp(handleRotate, -handleRotate, value);
            handleObject.transform.localEulerAngles = new Vector3(0, 0, nextHandleRotate);
        }
    }
}