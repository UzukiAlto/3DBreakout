using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MainSystem
{
    public class SystemInputManager : MonoBehaviour, ISystemInput
    {
        public event Action<bool> OnSubmit;
        public event Action<bool> OnRetry;
        public event Action<bool> OnPause;
        // Input Systemのアクション
        private PlayerInputActions inputActions;
        public Dictionary<SystemInputType, (bool Enabled, InputAction Action)> inputStates { get; private set; }
        private void Awake()
        {
            inputActions = new PlayerInputActions();
            inputStates = new Dictionary<SystemInputType, (bool, InputAction)>
            {
                { SystemInputType.Submit, (true, inputActions.System.Submit) },
                { SystemInputType.Retry, (true, inputActions.System.Retry) },
                { SystemInputType.Pause, (true, inputActions.System.Pause) }
            };
        }
        
        // ポーズ中はPauseの入力のみ有効化するために、各入力の有効/無効状態を別々に管理するメソッドを追加
        public void ChangeInputEnableState(SystemInputType inputType, bool isEnabled)
        {
            inputStates[inputType] = (isEnabled, inputStates[inputType].Action);
            if (isEnabled)
            {
                inputStates[inputType].Action.Enable();
            }
            else
            {
                inputStates[inputType].Action.Disable();
            }
        }
        
        private void OnEnable()
        {
            inputActions.System.Enable();
            foreach (var kvp in inputStates)
            {
                kvp.Value.Action?.Enable();
            }

            // Input Systemのアクションにイベントハンドラーを登録
            inputActions.System.Submit.performed += HandleSubmit;
            inputActions.System.Submit.canceled += HandleSubmit;
            inputActions.System.Retry.performed += HandleRetry;
            inputActions.System.Retry.canceled += HandleRetry;
            inputActions.System.Pause.performed += HandlePause;
            inputActions.System.Pause.canceled += HandlePause;
        }
        private void OnDisable()
        {
            // Input Systemのアクションからイベントハンドラーを解除
            inputActions.System.Submit.performed -= HandleSubmit;
            inputActions.System.Submit.canceled -= HandleSubmit;
            inputActions.System.Retry.performed -= HandleRetry;
            inputActions.System.Retry.canceled -= HandleRetry;
            inputActions.System.Pause.performed -= HandlePause;
            inputActions.System.Pause.canceled -= HandlePause;

            foreach (var kvp in inputStates)
            {
                kvp.Value.Action?.Disable();
            }
            inputActions.System.Disable();
        }
        
        private void HandleSubmit(InputAction.CallbackContext context)
        {
            bool isPressed = context.ReadValueAsButton();
            OnSubmit?.Invoke(isPressed);
        }

        private void HandleRetry(InputAction.CallbackContext context)
        {
            bool isPressed = context.ReadValueAsButton();
            OnRetry?.Invoke(isPressed);
        }

        private void HandlePause(InputAction.CallbackContext context)
        {
            bool isPressed = context.ReadValueAsButton();
            OnPause?.Invoke(isPressed);
        }

    }
}