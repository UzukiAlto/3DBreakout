using UnityEngine;
using MainSystem;

namespace ModeSelect
{
    public class PlayerCameraController : PlayerRotationControllerBase
    {
        [SerializeField] private RotateInputer rotateInputer;
        private void Update()
        {
            if (rotateInputer.IsInputReceived())
                Rotate(rotateInputer.GatPlayerRotationInput());
        }
    }
}