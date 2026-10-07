using UnityEngine;
using MainSystem;

namespace ModeSelect
{
    /// <summary>
    /// プレイヤーのカメラを回転させるクラス
    /// </summary>
    public class PlayerCameraController : PlayerRotationControllerBase
    {
        // インターフェースの参照元オブジェクト
        [SerializeField] private GameObject playerCameraInputObject;
        [SerializeField] private Transform configCameraTransform;
        // プレイヤーのカメラ入力を受け取るためのインターフェース
        private IPlayerCameraInput playerCameraInput;
        private Transform previousCameraTransform;
        private void Awake()
        {
            playerCameraInput = playerCameraInputObject.GetComponent<IPlayerCameraInput>();
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            // イベントの購読
            playerCameraInput.OnCameraMoveStart += OnCameraMoveStart;
            playerCameraInput.OnCameraMovingDelta += OnCameraMovingDelta;
            playerCameraInput.OnCameraMoveEnd += OnCameraMoveEnd;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (playerCameraInput != null)
            {
                // イベントの購読解除
                playerCameraInput.OnCameraMoveStart -= OnCameraMoveStart;
                playerCameraInput.OnCameraMovingDelta -= OnCameraMovingDelta;
                playerCameraInput.OnCameraMoveEnd -= OnCameraMoveEnd;
            }
        }
        public void MoveToConfigPosition()
        {
            previousCameraTransform = transform;
            CameraMovementUtility.MoveAroundPivot(
                movingCamera: transform,
                pivot: rotateCenterObj.transform,
                targetPoint: configCameraTransform,
                duration: 1f
            );
        }
        public void MoveToPreviousPosition()
        {
            CameraMovementUtility.MoveAroundPivot(
                movingCamera: transform,
                pivot: rotateCenterObj.transform,
                targetPoint: previousCameraTransform,
                duration: 1f
            );
        }
        private void OnCameraMoveStart(Vector2 pointerPos)
        {

            // 操作可能でないなら処理を行わない
            if (!base.currentScreen.canOperate) return;

            // マウスカーソルをロックして非表示にする
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        private void OnCameraMovingDelta(Vector2 delta)
        {
            // PlayerRotationControllerBaseのRotateメソッドを呼び出してカメラを回転させる
            Rotate(delta);
        }
        private void OnCameraMoveEnd(Vector2 pointerPos)
        {

            // 操作可能でないなら処理を行わない
            if (!base.currentScreen.canOperate) return;

            // マウスカーソルをロック解除して表示する
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }


    }
}