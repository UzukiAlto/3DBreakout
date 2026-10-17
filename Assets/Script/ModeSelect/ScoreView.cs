using System.Collections.Generic;
using MainSystem;
using TMPro;
using UnityEngine;

namespace ModeSelect
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private List<TMP_Text> scoreTextList;

        private readonly string[] rankPrefixes = { "1st", "2nd", "3rd", "4th", "5th" };
        private const int adjustPosition = 50;

        private void OnEnable()
        {
            // スコア画面が表示されるたびに最新のスコアをUIに反映するため
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            // 必要な参照が存在しない場合は描画処理を行わないため
            if (scoreManager == null || scoreTextList == null)
            {
                return;
            }
            Debug.Log("ScoreView UpdateDisplay");

            List<int> scores = scoreManager.ScoreList;
            for (int i = 0; i < scoreTextList.Count; i++)
            {
                // 各テキストコンポーネントが未割り当ての場合はスキップするため
                if (scoreTextList[i] == null)
                {
                    continue;
                }
                string prefix = i < rankPrefixes.Length ? rankPrefixes[i] : $"{i + 1}th";

                // スコアが記録されていない（-1）場合はハイフン表記にするため
                if (scores == null || i >= scores.Count || scores[i] < 0)
                {
                    scoreTextList[i].text = $"{prefix} <pos={adjustPosition}>: ---";
                }
                else
                {
                    scoreTextList[i].text = $"{prefix} <pos={adjustPosition}>: Stage {scores[i]}";
                }
            }
        }
    }
}