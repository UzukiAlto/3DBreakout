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
            Debug.Log("Show " + name);
            CurrentScreen.currentScreen = this;
        }
        public virtual void Hide()
        {
            Debug.Log("Hide " + name);
            if (CurrentScreen.currentScreen == this)
            {
                Debug.Log("CurrentScreen を null に設定");  
                CurrentScreen.currentScreen = null;
            }
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