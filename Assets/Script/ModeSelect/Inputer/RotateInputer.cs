using UnityEngine;
using MainSystem;

namespace ModeSelect
{
    /// <summary>
    /// プレイヤーのキューブに対する回転入力を処理するクラス
    /// </summary>
    public class RotateInputer : InputHandlerBase
    {
        private Vector3 rotationInput;
        private bool isClicking = false;

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                isClicking = true;
                rotationInput.x = Input.GetAxis("Mouse X");
                rotationInput.y = Input.GetAxis("Mouse Y");

                Debug.Log($"rotationInput: {rotationInput}");
            }
            else
            {
                isClicking = false;
                rotationInput = Vector3.zero;
            }
        }
        public override bool IsInputReceived()
        {
            return isClicking;
        }
        public Vector3 GatPlayerRotationInput()
        {
            return rotationInput;
        }
    }
}