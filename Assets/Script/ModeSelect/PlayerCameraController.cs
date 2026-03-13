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
        // プレイヤーのカメラ入力を受け取るためのインターフェース
        private IPlayerCameraInput playerCameraInput;
        private void Awake()
        {
            playerCameraInput = playerCameraInputObject.GetComponent<IPlayerCameraInput>();
        }
        private void OnEnable() 
        {
            // イベントの購読
            playerCameraInput.OnCameraMoveStart += OnCameraMoveStart;
            playerCameraInput.OnCameraMovingDelta += OnCameraMovingDelta;
            playerCameraInput.OnCameraMoveEnd += OnCameraMoveEnd;
        }

        private void OnDisable() 
        {
            if (playerCameraInput != null)
            {
                // イベントの購読解除
                playerCameraInput.OnCameraMoveStart -= OnCameraMoveStart;
                playerCameraInput.OnCameraMovingDelta -= OnCameraMovingDelta;
                playerCameraInput.OnCameraMoveEnd -= OnCameraMoveEnd;
            }
        }
        private void OnCameraMoveStart(Vector2 pointerPos)
        {
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
            // マウスカーソルをロック解除して表示する
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
    }
}