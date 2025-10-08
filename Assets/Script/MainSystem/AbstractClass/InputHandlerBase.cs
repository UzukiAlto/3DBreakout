using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainSystem
{
    /// <summary>
    /// プレイヤーの操作を受け付ける基底クラス
    /// </summary>
    public abstract class InputHandlerBase : MonoBehaviour
    {
        protected bool isInputReceived = false;
        public abstract bool GetIsInputReceived(); 
        
    }
}
