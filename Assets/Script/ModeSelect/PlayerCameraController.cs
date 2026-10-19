using UnityEngine;
using MainSystem;

namespace ModeSelect
{
    /// <summary>
    /// モードセレクト画面におけるプレイヤーのカメラ制御クラス
    /// モード選択に応じたカメラのピボット移動を管理
    /// </summary>
    public class PlayerCameraController : PlayerRotationControllerBase
    {
        private Vector3 previousCameraPosition;
        private Quaternion previousCameraRotation;

        public void MoveToTargetPosition(Transform targetPoint)
        {
            // 移動対象のポイントが設定されていない場合はカメラ移動を行わないため
            if (targetPoint == null)
            {
                return;
            }

            // 画面遷移開始時にドラッグ中だった場合でも、UI操作ができるようにカーソルを復帰させるため
            UnlockCursor();

            previousCameraPosition = this.transform.position;
            previousCameraRotation = this.transform.rotation;
            CameraMovementUtility.MoveAroundPivot(
                movingCamera: this.transform,
                pivot: rotateCenterObj.transform,
                targetPoint: targetPoint,
                duration: 1f
            );
        }

        public void MoveToPreviousPosition()
        {
            CameraMovementUtility.MoveAroundPivot(
                movingCamera: this.transform,
                pivot: rotateCenterObj.transform,
                targetPosition: previousCameraPosition,
                targetRotation: previousCameraRotation,
                duration: 1f
            );
        }
    }
}