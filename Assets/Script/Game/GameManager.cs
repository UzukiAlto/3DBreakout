using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainSystem;

namespace Game
{
    public class GameManager : MonoBehaviour, IInitializable
    {        
        public float gameSpeed { get; private set; } = 1f;
        [SerializeField] private GameObject systemInputObject; 
        private ISystemInput systemInput;
        public bool isGamePlaying { get; private set; } = false;

        public event Action OnGameStarted;
        public event Action OnGameReady;
        public event Action OnAllBlocksRemoved;

        private void Awake()
        {
            systemInput = systemInputObject.GetComponent<ISystemInput>();  
        }

        private void OnEnable()
        {
            systemInput.OnSubmit += StartGame;
        }
        private void OnDisable()
        {
            systemInput.OnSubmit -= StartGame;
        }
        public void Initialize()
        {
            gameSpeed = 1f;
        }

        public void PrepareGame()
        {
            OnGameReady?.Invoke();
        }

        public void RaiseAllBlocksRemoved()
        {
            OnAllBlocksRemoved?.Invoke();
        }

        public void StartGame(bool isPressed)
        {
            if (!isPressed || isGamePlaying)
            {
                return;
            }
            Debug.Log("Start Game");
            isGamePlaying = true;
            OnGameStarted?.Invoke();
        }
    }
}