using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

namespace MainSystem
{
    /// <summary>
    /// ゲームスクリーンの管理を行うクラス
    /// </summary>
    public class GameScreen : ScreenBase
    {
        [SerializeField] private GameManager gameManager;
        // 初期化の順番を変更できるようにリストで管理
        [SerializeField] private List<GameObject> initializableList = new List<GameObject>();
        public override void Hide()
        {
            base.Hide();
            screenCanvas.SetActive(false);
            SetEnableOperation(false);
        }

        public override void Show()
        {
            base.Show();
            screenCanvas.SetActive(true);
            SetEnableOperation(false);
            foreach (var item in initializableList)
            {
                item.GetComponent<IInitializable>()?.Initialize();
            }
        }
    }
}
