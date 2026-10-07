using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainSystem
{
    /// <summary>
    /// キューブの回転を制御する基底クラス
    /// 回転のロジックを実装
    /// </summary>
    public abstract class PlayerRotationControllerBase : MonoBehaviour
    {
        [SerializeField] protected ScreenBase currentScreen;
        [SerializeField] protected GameObject playerCameraObj;
        [SerializeField] protected GameObject rotateCenterObj;
        [SerializeField] private ConfigData configData;

        [SerializeField] private float sensitivity;
        private const float MinSensitivity = 40f;
        private const float MaxSensitivity = 100f;

        protected virtual void OnEnable()
        {
            configData.SubscribeSensitivity(SetSensitivity);
        }

        protected virtual void OnDisable()
        {
            configData.UnsubscribeSensitivity(SetSensitivity);
        }

        private void SetSensitivity(float configSensitivity)
        {
            this.sensitivity = Mathf.Lerp(MinSensitivity, MaxSensitivity, configSensitivity);
        }

        public void Rotate(Vector2 rotationAngle)
        {
            // 操作可能でないなら処理を行わない
            if (!currentScreen.canOperate) return;

            rotationAngle *= sensitivity;

            playerCameraObj.transform.RotateAround(rotateCenterObj.transform.position, playerCameraObj.transform.up, rotationAngle.x);
            playerCameraObj.transform.RotateAround(rotateCenterObj.transform.position, playerCameraObj.transform.right, -rotationAngle.y);
        }
    }
}
