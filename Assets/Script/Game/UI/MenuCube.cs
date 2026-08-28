using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Game
{
    public class MenuCube : MonoBehaviour
    {
        private Tweener rotationTween;
        private void Awake()
        {
            Vector3 currentRotation = this.transform.rotation.eulerAngles;
            Vector3 targetRotation = currentRotation + new Vector3(180, 360, 0);
            rotationTween = this.transform.DORotate(targetRotation, 2f, RotateMode.FastBeyond360)
                                            .SetLoops(-1, LoopType.Restart)
                                            .SetEase(Ease.Linear);
        }

        public void PlayRotation()
        {
            rotationTween.Play();
        }
        public void PauseRotation()
        {
            rotationTween.Pause();
        }


    }
}