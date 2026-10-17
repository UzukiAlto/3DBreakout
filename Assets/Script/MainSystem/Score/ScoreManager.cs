using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Game;
using UnityEngine;

namespace MainSystem
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        private const int showScoreCount = 5;
        private List<string> scoreNameList = Enumerable.Range(0, showScoreCount)
                                                        .Select(i => "score" + i)
                                                        .ToList();
        public List<int> ScoreList { get; private set; } = Enumerable.Repeat(-1, showScoreCount).ToList();

        private void Awake()
        {
            LoadScore();
        }
        private void OnEnable()
        {
            gameManager.OnGameOver += AddScore;
        }

        private void OnDisable()
        {
            gameManager.OnGameOver -= AddScore;
        }

        private void LoadScore()
        {
            for (int i = 0; i < showScoreCount; i++)
            {
                ScoreList[i] = PlayerPrefs.GetInt(scoreNameList[i], -1);
            }
        }

        private void SaveScore()
        {
            for (int i = 0; i < showScoreCount; i++)
            {
                PlayerPrefs.SetInt(scoreNameList[i], ScoreList[i]);
            }
            PlayerPrefs.Save();
        }

        public void AddScore()
        {
            ScoreList.Add(GameState.currentStage);
            ScoreList.Sort((a, b) => b.CompareTo(a));
            // 上位showScoreCountまでを取り出す
            ScoreList = ScoreList.Take(showScoreCount).ToList();
            SaveScore();
        }


    }
}