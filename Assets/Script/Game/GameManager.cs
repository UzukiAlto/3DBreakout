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
        private bool isGameStarted = false;

        public event Action OnGameStarted;
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

        public void StartGame(bool isPressed)
        {
            if (!isPressed || isGameStarted)
            {
                return;
            }
            Debug.Log("Start Game");
            isGameStarted = true;
            OnGameStarted?.Invoke();
        }
    }
}