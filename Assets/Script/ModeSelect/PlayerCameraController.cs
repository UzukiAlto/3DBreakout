using UnityEngine;
using MainSystem;

namespace ModeSelect
{
    public class PlayerCameraController : PlayerRotationControllerBase
    {
        [SerializeField] private GameObject rotateInputerObject;
        private InputHandlerBase rotateInputer;
        private IRotateInputable rotateInputable;

        private void Start()
        {
            rotateInputer = rotateInputerObject.GetComponent<InputHandlerBase>();
            rotateInputable = rotateInputerObject.GetComponent<IRotateInputable>();
        }
        private void Update()
        {
            if (rotateInputer.GetIsInputReceived())
                Rotate(rotateInputable.GatRotationInput());
        }
    }
}