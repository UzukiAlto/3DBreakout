using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainSystem;

namespace Game
{
    public class GameManager : MonoBehaviour
    {        
        [SerializeField] private GameObject systemInputObject; 
        [SerializeField] private ScreenBase gameScreen;
        private ISystemInput systemInput;
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

        public void StartGame(bool isPressed)
        {
            if (!isPressed) return; // ボタンが押されたときのみ反応
            Debug.Log("Start Game");

            gameScreen.SetEnableOperation(true);
        }
    }
}