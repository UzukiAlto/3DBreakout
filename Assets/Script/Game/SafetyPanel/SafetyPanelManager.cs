using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class SafetyPanelManager : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        // Inspector で4枚のSafetyPanelを設定する
        [SerializeField] private List<SafetyPanel> panels;

        // 現在アクティブなパネルのリスト（ランダム選択用）
        private List<SafetyPanel> activePanels = new List<SafetyPanel>();

        private void OnEnable()
        {
            gameManager.OnGameReady += InitializePanels;
            gameManager.OnAllBlocksRemoved += DeactivateOnePanel;
        }

        private void OnDisable()
        {
            gameManager.OnGameReady -= InitializePanels;
            gameManager.OnAllBlocksRemoved -= DeactivateOnePanel;
        }

        /// <summary>
        /// ゲーム開始・リトライ時に全パネルを有効化する
        /// </summary>
        private void InitializePanels()
        {
            activePanels = new List<SafetyPanel>(panels);
            foreach (SafetyPanel panel in panels)
            {
                panel.Activate();
            }
        }

        /// <summary>
        /// ブロックを全て壊したとき、アクティブなパネルからランダムに1枚を無効化する
        /// </summary>
        private void DeactivateOnePanel()
        {
            if (activePanels.Count == 0)
            {
                return;
            }

            int randomIndex = Random.Range(0, activePanels.Count);
            activePanels[randomIndex].Deactivate();
            activePanels.RemoveAt(randomIndex);
        }
    }
}