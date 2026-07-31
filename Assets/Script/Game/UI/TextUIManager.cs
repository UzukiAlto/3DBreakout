using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainSystem;
using TMPro;

namespace Game
{
    public class TextUIManager : MonoBehaviour, IInitializable
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private TMP_Text startText;
        [SerializeField] private TMP_Text stageText;
        [SerializeField] private TMP_Text gameSpeedText;
        [SerializeField] private TMP_Text retryText;

        public void OnEnable()
        {
            gameManager.OnGameStarted += OnGameStarted;
            gameManager.OnGameFailed += OnGameFailed;
        }
        public void OnDisable()
        {
            gameManager.OnGameStarted -= OnGameStarted;
            gameManager.OnGameFailed -= OnGameFailed;
        }
        public void Initialize()
        {
            startText.enabled = true;
            stageText.enabled = false;
            gameSpeedText.enabled = false;
            retryText.enabled = false;
        }

        private void OnGameStarted()
        {
            startText.enabled = false;
            stageText.enabled = true;
            gameSpeedText.enabled = true;
        }

        private void OnGameFailed()
        {
            startText.enabled = true;
            stageText.enabled = false;
            gameSpeedText.enabled = false;
        }

    }
}