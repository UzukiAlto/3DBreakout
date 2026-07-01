using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// ボールの予測線を表示するクラス
    /// </summary>
    public class HitPredictionRenderer : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private GameObject ballObject;
        [SerializeField] private GameObject predictionBall;
        [SerializeField] private Material predictionBallMaterial;
        [SerializeField] private LayerMask ballTargetLayerMask;
        void Update()
        {
            RaycastHit hit;
            Debug.DrawRay(ballObject.transform.position, ballObject.transform.forward * 100);
            if (gameManager.isGamePlaying && Physics.SphereCast(ballObject.transform.position, 0.5f, ballObject.transform.forward, out hit, Mathf.Infinity, ballTargetLayerMask))
            {
                SetTrajectory(hit);
            }
            
        }

        private float predictionLength = 5f;
        private float maxAlpha = 0.7f;

        private void SetTrajectory(RaycastHit hit)
        {
            Debug.Log("SetTrajectory called: " + hit.collider.gameObject.name);
            predictionBall.transform.position = hit.point;
            float ballAlpha;

            // 対象との距離が近いほど予測を濃く表示
            if(hit.distance <= predictionLength)
            {
                float constA = predictionLength + 1 / (1 + maxAlpha);
                float constB = 1 / constA + maxAlpha;
                ballAlpha = Mathf.Max(0, 1 / (hit.distance - constA) + constB);

            }else{
                ballAlpha = 0f;
            }
            
            if (hit.collider.gameObject.TryGetComponent(out IBallTarget target))
            {
                target.OnHitBallRaycast(predictionBallMaterial, ballAlpha);
                Debug.Log("IBallTarget is found: " + target);
            }
        }
    }
}