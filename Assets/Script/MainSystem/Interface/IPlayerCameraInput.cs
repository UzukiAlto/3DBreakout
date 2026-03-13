using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainSystem
{
    public interface IPlayerCameraInput
    {
        event Action<Vector2> OnCameraMoveStart;
        event Action<Vector2> OnCameraMoving;
        event Action<Vector2> OnCameraMoveEnd;
        event Action<Vector2> OnCameraMovingDelta;
    }
}