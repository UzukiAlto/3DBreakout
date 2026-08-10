using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainSystem;

namespace Game
{
    public class GameManager : MonoBehaviour
    {        
        [SerializeField] private GameObject systemInputObject; 
        [SerializeField] private LifeManager lifeManager;
        private ISystemInput systemInput;
        public bool isGamePlaying { get; private set; } = false;
        private bool isGameOver = false;
        private bool isGameReady = false;

        public event Action OnGameStarted;
        public event Action OnGameReady;
        public event Action OnAllBlocksRemoved;
        public event Action OnGameFailed;
        public event Action OnGameOver;

        void Update()
        {
            // デバッグ用に全ブロック削除を強制的に呼び出す
            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.N))
            {
                RaiseAllBlocksRemoved();
            }
        }


        private void Awake()
        {
            systemInput = systemInputObject.GetComponent<ISystemInput>();  
        }

        private void OnEnable()
        {
            systemInput.OnSubmit += StartGame;
            systemInput.OnRetry += RetryGame;
            OnAllBlocksRemoved += GameState.NextStage;
            OnGameReady += GameState.Initialize;
        }
        private void OnDisable()
        {
            systemInput.OnSubmit -= StartGame;
            systemInput.OnRetry -= RetryGame;
            OnAllBlocksRemoved -= GameState.NextStage;
            OnGameReady -= GameState.Initialize;
        }

        public void PrepareGame()
        {
            OnGameReady?.Invoke();
            isGameReady = true;
        }

        public void RaiseAllBlocksRemoved()
        {
            OnAllBlocksRemoved?.Invoke();
        }

        public void StartGame(bool isPressed)
        {
            if (!isPressed || isGamePlaying || !isGameReady)
            {
                return;
            }
            Debug.Log("Start Game");
            isGamePlaying = true;
            isGameReady = false;
            OnGameStarted?.Invoke();
        }

        public void FailGame()
        {
            isGamePlaying = false;
            isGameOver = lifeManager.decreaseLife();
            if (isGameOver)
            {
                OnGameOver?.Invoke();
                isGameReady = false;
            }
            else
            {
                OnGameFailed?.Invoke(); 
                isGameReady = true;
            }
        }

        public void RetryGame(bool isPressed)
        {
            if (!isPressed || isGamePlaying || !isGameOver)
            {
                return;
            }
            isGameOver = false;
            PrepareGame();
        }
    }
}