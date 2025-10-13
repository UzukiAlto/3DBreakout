using UnityEngine;

namespace MainSystem
{
    /// <summary>
    /// スクリーンの抽象クラス
    /// </summary>
    public abstract class ScreenBase : MonoBehaviour
    {
        public bool canOperate{ get; private set; } = true;
        public abstract void Show();
        public abstract void Hide();
        // インスペクター上で設定できる読み取り専用プロパティ
        [field: SerializeField] public GameObject cameraObject { get; private set; }
        [field: SerializeField] public GameObject screenCanvas { get; private set; }
        public void SetEnableOperation(bool enable)
        {
            canOperate = enable;
        }
    }
}