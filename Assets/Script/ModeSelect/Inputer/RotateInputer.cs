using UnityEngine;
using MainSystem;

namespace ModeSelect
{
    /// <summary>
    /// プレイヤーのキューブに対する回転入力を処理するクラス
    /// </summary>
    public class RotateInputer : InputHandlerBase, IRotateInputable
    {
        private Vector3 rotationInput;

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                isInputReceived = true;
                rotationInput.x = Input.GetAxis("Mouse X");
                rotationInput.y = Input.GetAxis("Mouse Y");
            }
            else
            {
                isInputReceived = false;
                rotationInput = Vector3.zero;
            }
        }
        public override bool GetIsInputReceived()
        {
            return isInputReceived;
        }
        public Vector3 GatRotationInput()
        {
            return rotationInput;
        }
    }
}