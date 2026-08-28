using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainSystem;
using DG.Tweening;

namespace Game
{
    /// <summary>
    /// プレイヤーのカメラを制御するクラス
    /// </summary>
    public class PlayerCameraController : PlayerRotationControllerBase
    {
        // インターフェースの参照元オブジェクト
        [SerializeField] private GameObject playerCameraInputObject;
        [SerializeField] private GameManager gameManager;
        private Vector3 defaultCameraPosition;
        private Quaternion defaultCameraRotation;
        private float changeSecond = 0.4f;
        // プレイヤーのカメラ入力を受け取るためのインターフェース
        private IPlayerCameraInput playerCameraInput;
        private bool isCameraMoving = false;
        private void Awake()
        {
            playerCameraInput = playerCameraInputObject.GetComponent<IPlayerCameraInput>();
            defaultCameraPosition = playerCameraObj.transform.position;
            defaultCameraRotation = playerCameraObj.transform.rotation;
        }
        private void OnEnable() 
        {
            // イベントの購読
            // playerCameraInput.OnCameraMoveStart += OnCameraMoveStart;
            playerCameraInput.OnCameraMovingDelta += OnCameraMovingDelta;
            playerCameraInput.OnCameraMoveEnd += OnCameraMoveEnd;
            gameManager.OnGameReady += MoveCameraToDefaultPosition;
            gameManager.OnGameFailed += MoveCameraToDefaultPosition;

            isCameraMoving = false;
        }

        private void OnDisable() 
        {
            if (playerCameraInput != null)
            {
                // イベントの購読解除
                // playerCameraInput.OnCameraMoveStart -= OnCameraMoveStart;
                playerCameraInput.OnCameraMovingDelta -= OnCameraMovingDelta;
                playerCameraInput.OnCameraMoveEnd -= OnCameraMoveEnd;
            }
            if (gameManager != null)
            {
                gameManager.OnGameReady -= MoveCameraToDefaultPosition;
                gameManager.OnGameFailed -= MoveCameraToDefaultPosition;
            }
        }
        private void OnCameraMoveStart()
        {
            // マウスカーソルをロックして非表示にする
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        private void OnCameraMovingDelta(Vector2 delta)
        {
            // クリックを可能にするため、カメラが動き出してからマウスカーソルをロックする
            if (!isCameraMoving)
            {
                OnCameraMoveStart();
                isCameraMoving = true;
            }
            // PlayerRotationControllerBaseのRotateメソッドを呼び出してカメラを回転させる
            Rotate(delta);
        }
        private void OnCameraMoveEnd(Vector2 pointerPos)
        {
            isCameraMoving = false;
            // マウスカーソルをロック解除して表示する
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // ゲーム失敗時にカメラをデフォルト位置に戻す
        private void MoveCameraToDefaultPosition()
        {
            Vector3 endPos = defaultCameraPosition - rotateCenterObj.transform.position;
            Vector3 startPos = playerCameraObj.transform.position - rotateCenterObj.transform.position;

            Quaternion endRotate = defaultCameraRotation;
            Quaternion startRotate = playerCameraObj.transform.rotation;
            float slerpPos = 0f;
            DOTween.To
            (
                () => slerpPos,
                x =>
                {
                    playerCameraObj.transform.position = Vector3.Slerp(startPos, endPos, x) + rotateCenterObj.transform.position;
                    playerCameraObj.transform.rotation = Quaternion.Lerp(startRotate, endRotate, x);
                },
                1f,
                changeSecond
            )
            .SetEase(Ease.OutCubic);
        }
        
    }
}