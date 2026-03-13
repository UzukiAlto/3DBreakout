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
        [SerializeField] private ScreenBase currentScreen;
        [SerializeField] protected GameObject playerCameraObj;
        [SerializeField] private GameObject rotateCenterObj;
        [SerializeField] private ConfigData configData;
        public void Rotate(Vector2 rotationAngle)
        {
            // 操作可能でないなら処理を行わない
            if (!currentScreen.canOperate) return;

            rotationAngle *= configData.GetPlayerConfig().sensitivity;

            playerCameraObj.transform.RotateAround(rotateCenterObj.transform.position, playerCameraObj.transform.up, rotationAngle.x);
            playerCameraObj.transform.RotateAround(rotateCenterObj.transform.position, playerCameraObj.transform.right, -rotationAngle.y);
        }
    }
}
