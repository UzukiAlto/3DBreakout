using UnityEngine;

namespace MainSystem
{
    /// <summary>
    /// スクリーンの抽象クラス
    /// </summary>
    public abstract class ScreenBase : MonoBehaviour
    {
        public bool canOperate;
        public abstract void Show();
        public abstract void Hide();
        public void DisableOperation()
        {
            canOperate = false;
        }
        public void EnableOperation()
        {
            canOperate = true;
        }
    }
}