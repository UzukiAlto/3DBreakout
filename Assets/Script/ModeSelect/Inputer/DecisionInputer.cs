using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainSystem;

namespace ModeSelect
{
    /// <summary>
    /// プレイヤーのキューブに対する決定入力を処理するクラス
    /// </summary>

    public class DecisionInputer : InputHandlerBase
    {
        public override bool GetIsInputReceived()
        {
            return isInputReceived;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isInputReceived = true;
            }
            else
            {
                isInputReceived = false;
            }
        }
    }
}
