using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MainSystem
{
    /// <summary>
    /// プレイヤーのカメラ回転をInput Systemで処理するクラス
    /// </summary>
    public class PlayerCameraInputManager : MonoBehaviour, IPlayerCameraInput
    {
        // IPlayerCameraInputのイベント
        public event Action<Vector2> OnCameraMoveStart;
        public event Action<Vector2> OnCameraMoving;
        public event Action<Vector2> OnCameraMoveEnd;
        public event Action<Vector2> OnCameraMovingDelta;
        // Input Systemのアクション
        private PlayerInputActions inputActions;
        private bool isMoving = false;
        private void Awake()
        {
            inputActions = new PlayerInputActions();
        }
        private void OnEnable()
        {
            inputActions.Enable();
            // Input Systemのアクションにイベントハンドラーを登録
            inputActions.Player.PointerPress.started += HandleCameraMoveStarted;
            inputActions.Player.PointerPosition.performed += HandleCameraMovePerformed;
            inputActions.Player.PointerPress.canceled += HandleCameraMoveCanceled;
            inputActions.Player.PointerDelta.performed += HandleCameraMoveDeltaPerformed;
        }
        private void OnDisable()
        {
            // Input Systemのアクションからイベントハンドラーを解除
            inputActions.Player.PointerPress.started -= HandleCameraMoveStarted;
            inputActions.Player.PointerPosition.performed -= HandleCameraMovePerformed;
            inputActions.Player.PointerPress.canceled -= HandleCameraMoveCanceled;
            inputActions.Player.PointerDelta.performed -= HandleCameraMoveDeltaPerformed;

            inputActions.Disable();
        }

        // カメラ移動開始時のスクリーン座標を取得し、ビューポート座標に変換してイベント発火
        private void HandleCameraMoveStarted(InputAction.CallbackContext context)
        {
            if (CurrentScreen.currentScreen == null) return; 
            isMoving = true;
            Vector2 startScreenPos = inputActions.Player.PointerPosition.ReadValue<Vector2>();
            Vector2 startViewportPos = CurrentScreen.currentScreen.screenCamera.ScreenToViewportPoint(startScreenPos);
            OnCameraMoveStart?.Invoke(startViewportPos);
        }

        // カメラ移動中のスクリーン座標を取得し、ビューポート座標に変換してイベント発火
        private void HandleCameraMovePerformed(InputAction.CallbackContext context)
        {
            if (CurrentScreen.currentScreen == null) return; 
            if (isMoving == false) return;
            Vector2 currentScreenPos = context.ReadValue<Vector2>();
            Vector2 currentViewportPos = CurrentScreen.currentScreen.screenCamera.ScreenToViewportPoint(currentScreenPos);

            OnCameraMoving?.Invoke(currentViewportPos);
        }

        // カメラ移動終了時のスクリーン座標を取得し、ビューポート座標に変換してイベント発火
        private void HandleCameraMoveCanceled(InputAction.CallbackContext context)
        {
            if (CurrentScreen.currentScreen == null) return; 
            isMoving = false;
            Vector2 endScreenPos = inputActions.Player.PointerPosition.ReadValue<Vector2>();
            Vector2 endViewportPos = CurrentScreen.currentScreen.screenCamera.ScreenToViewportPoint(endScreenPos);
            OnCameraMoveEnd?.Invoke(endViewportPos);
        }

        // カメラ移動の移動量を取得してイベント発火
        private void HandleCameraMoveDeltaPerformed(InputAction.CallbackContext context)
        {
            if (isMoving == false) return;
            Vector2 delta = context.ReadValue<Vector2>() / Screen.height; // 画面の高さで割って正規化
            OnCameraMovingDelta?.Invoke(delta);
        }
    }
}