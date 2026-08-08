using System;
using UnityEngine;

namespace MainSystem 
{
    public interface IPlayerMoveInput
    {
        event Action<Vector2> OnMoveStart;
        event Action<Vector2> OnMoving;
        event Action<Vector2> OnMoveEnd;
        event Action<bool> OnChangePanel;
    }
}