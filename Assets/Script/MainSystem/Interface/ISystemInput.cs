using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

namespace MainSystem
{
    public interface ISystemInput
    {
        event Action<bool> OnSubmit;
        event Action<bool> OnRetry;
        event Action<bool> OnPause;
        // ポーズ中はPauseの入力のみ有効化するために、各入力の有効/無効状態を管理するプロパティを追加
        Dictionary<SystemInputType, (bool Enabled, InputAction Action)> inputStates { get; }
        void ChangeInputEnableState(SystemInputType inputType, bool isEnabled);
    }
}
public enum SystemInputType
{
    Submit,
    Retry,
    Pause
}