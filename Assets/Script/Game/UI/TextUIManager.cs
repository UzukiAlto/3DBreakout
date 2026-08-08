using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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
            gameManager.OnGameStarted += UpdateText;
            gameManager.OnGameFailed += OnGameFailed;
            gameManager.OnGameOver += OnGameOver;
            gameManager.OnAllBlocksRemoved += UpdateText;
        }
        public void OnDisable()
        {
            gameManager.OnGameStarted -= OnGameStarted;
            gameManager.OnGameStarted -= UpdateText;
            gameManager.OnGameFailed -= OnGameFailed;
            gameManager.OnGameOver -= OnGameOver;
            gameManager.OnAllBlocksRemoved -= UpdateText;
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
            retryText.enabled = false;
        }

        private void OnGameFailed()
        {
            startText.enabled = true;
            stageText.enabled = false;
            gameSpeedText.enabled = false;
        }

        private void OnGameOver()
        {
            stageText.enabled = false;
            gameSpeedText.enabled = false;
            retryText.enabled = true;
        }
        private void UpdateText()
        {
            stageText.text = $"Stage: {GameState.currentStage}";
            float roundedGameSpeed = (float)Math.Round(GameState.gameSpeed, 2, MidpointRounding.AwayFromZero);
            gameSpeedText.text = $"Game Speed: x{roundedGameSpeed.ToString("F2")}";
        }

    }
}