using System;
using UnityEngine;
using DG.Tweening;

namespace MainSystem
{
    public static class CameraMovementUtility
    {
        /// <summary>
        /// カメラを指定した中心点（pivot）の周りをSlerp（球面線形補間）で弧を描くように移動させる
        /// </summary>
        /// <param name="movingCamera">実際にアニメーションさせるカメラのTransform</param>
        /// <param name="pivot">回転の中心となるオブジェクトのTransform（Cube等）</param>
        /// <param name="targetPoint">移動先の目標となる座標と角度を持つTransform</param>
        /// <param name="duration">アニメーションの時間（秒）</param>
        /// <param name="onComplete">アニメーション終了時に実行する処理（省略可）</param>
        public static void MoveAroundPivot(Transform movingCamera, Transform pivot, Transform targetPoint, float duration, Action onComplete = null)
        {
            Vector3 startPos = movingCamera.position - pivot.position;
            Vector3 endPos = targetPoint.position - pivot.position;

            Quaternion startRotate = movingCamera.rotation;
            Quaternion endRotate = targetPoint.rotation;

            float slerpPos = 0f;
            DOTween.To
            (
                () => slerpPos,
                x =>
                {
                    movingCamera.position = Vector3.Slerp(startPos, endPos, x) + pivot.position;
                    movingCamera.rotation = Quaternion.Slerp(startRotate, endRotate, x);
                },
                1f,
                duration
            )
            .OnComplete(() =>
            {
                onComplete?.Invoke();
            })
            .SetEase(Ease.OutCubic);
        }
    }
}