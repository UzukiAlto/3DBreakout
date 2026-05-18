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
        
        // Input Systemのアクション
        private PlayerInputActions inputActions;
        private void Awake()
        {
            inputActions = new PlayerInputActions();
        }
        
        private void OnEnable()
        {
            inputActions.Enable();
            // Input Systemのアクションにイベントハンドラーを登録
            inputActions.System.Submit.performed += HandleSubmit;
            inputActions.System.Submit.canceled += HandleSubmit;
        }
        private void OnDisable()
        {
            // Input Systemのアクションからイベントハンドラーを解除
            inputActions.System.Submit.performed -= HandleSubmit;
            inputActions.System.Submit.canceled -= HandleSubmit;
            inputActions.Disable();
        }
        
        private void HandleSubmit(InputAction.CallbackContext context)
        {
            bool isPressed = context.ReadValueAsButton();
            OnSubmit?.Invoke(isPressed);
        }

    }
}