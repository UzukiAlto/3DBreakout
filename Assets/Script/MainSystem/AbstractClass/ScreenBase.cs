using UnityEngine;

namespace MainSystem
{
    /// <summary>
    /// スクリーンの抽象クラス
    /// </summary>
    public abstract class ScreenBase : MonoBehaviour
    {
        public bool canOperate{ get; private set; } = true;
        public virtual void Show()
        {
            CurrentScreen.currentScreen = this;
        }
        public virtual void Hide()
        {
            CurrentScreen.currentScreen = null;
        }
        // インスペクター上で設定できる読み取り専用プロパティ
        [field: SerializeField] public GameObject cameraObject { get; private set; }
        [field: SerializeField] public GameObject screenCanvas { get; private set; }
        [field: SerializeField] public Camera screenCamera { get; private set; }
        public void SetEnableOperation(bool enable)
        {
            canOperate = enable;
        }
    }
}