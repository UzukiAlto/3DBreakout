using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Game
{
    public class ReturnText : MonoBehaviour
    {
        private Vector3 defaultScale;
        private float zoomScale = 1.1f;
        private float zoomDuration = 0.2f;
        private Tweener zoomInTweener;
        private Tweener zoomOutTweener;
        private void Awake()
        {
            defaultScale = transform.localScale;
        }
        public void OnPointerEnter()
        {
            if (zoomInTweener != null || zoomInTweener.IsActive())
            {
                return;
            }
            zoomOutTweener?.Kill();
            zoomInTweener = this.transform
                                .DOScale(defaultScale * zoomScale, zoomDuration)
                                .SetEase(Ease.OutQuint)
                                .OnKill(() => zoomInTweener = null);
        }
        public void OnPointerExit()
        {
            if (zoomOutTweener != null || zoomOutTweener.IsActive())
            {
                return;
            }
            zoomInTweener?.Kill();
            zoomOutTweener = this.transform
                                .DOScale(defaultScale, zoomDuration)
                                .SetEase(Ease.OutQuint)
                                .OnKill(() => zoomOutTweener = null);
            
        }
    }
}