using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MainSystem
{
    /// <summary>
    /// プレイヤーの移動をInput Systemで処理するクラス
    /// </summary>
    public class PlayerMoveInputManager : MonoBehaviour, IPlayerMoveInput
    {
        
        public event Action<Vector2> OnMoveStart;
        public event Action<Vector2> OnMoving;
        public event Action<Vector2> OnMoveEnd;
        public event Action<bool> OnChangePanel;
        public event Action<bool> OnSubmit;

        private PlayerInputActions inputActions;
        private bool isMoving = false;

        private void Awake()
        {
            inputActions = new PlayerInputActions();
        }
        private void OnEnable()
        {
            inputActions.Enable();

            inputActions.Player.Move.started += HandlePlayerMoveStarted;
            inputActions.Player.Move.performed += HandleMovePerformed;
            inputActions.Player.Move.canceled += HandleMoveCanceled;
            // boolを返すイベントハンドラーはperformedとcanceledの両方に登録して、押下と離したときの両方を検知できるようにする
            inputActions.Player.ChangePanel.performed += HandleChangePanel;
            inputActions.Player.ChangePanel.canceled += HandleChangePanel;
            inputActions.Player.Submit.performed += HandleSubmit;
            inputActions.Player.Submit.canceled += HandleSubmit;
        }

        private void OnDisable()
        {            
            inputActions.Player.Move.started -= HandlePlayerMoveStarted;
            inputActions.Player.Move.performed -= HandleMovePerformed;
            inputActions.Player.Move.canceled -= HandleMoveCanceled;
            inputActions.Player.ChangePanel.performed -= HandleChangePanel;
            inputActions.Player.ChangePanel.canceled -= HandleChangePanel;
            inputActions.Player.Submit.performed -= HandleSubmit;
            inputActions.Player.Submit.canceled -= HandleSubmit;

            inputActions.Disable();
        }

        private void HandlePlayerMoveStarted(InputAction.CallbackContext context)
        {
            isMoving = true;
            Vector2 startMoveInput = context.ReadValue<Vector2>();
            OnMoveStart?.Invoke(startMoveInput);
        }

        private void HandleMovePerformed(InputAction.CallbackContext context)
        {
            if (isMoving == false) return;
            Vector2 currentMoveInput = context.ReadValue<Vector2>();
            OnMoving?.Invoke(currentMoveInput);
        }
        private void HandleMoveCanceled(InputAction.CallbackContext context)
        {
            isMoving = false;
            Vector2 endMoveInput = context.ReadValue<Vector2>();
            OnMoveEnd?.Invoke(endMoveInput);
        }

        private void HandleChangePanel(InputAction.CallbackContext context)
        {
            bool isPressed = context.ReadValueAsButton();
            OnChangePanel?.Invoke(isPressed);
        }

        private void HandleSubmit(InputAction.CallbackContext context)
        {
            bool isPressed = context.ReadValueAsButton();
            OnSubmit?.Invoke(isPressed);
        }
    }
}