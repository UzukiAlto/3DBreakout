using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainSystem;

namespace Game
{
    public class PlayerPanelMover : MonoBehaviour, IInitializable
    {
        [SerializeField] private GameObject playerMoveInputObject;
        [SerializeField] private GameObject playerPanel;
        [SerializeField] private GameObject screenCamera;
        private IPlayerMoveInput playerMoveInput; 
        public void Initialize()
        {
        }
        void Start()
        {
            playerMoveInput = playerMoveInputObject.GetComponent<IPlayerMoveInput>();            
        }
        private void OnEnable() {
            playerMoveInput.OnMoving += MovePlayerPanel;
        }
        private void OnDisable() {
            playerMoveInput.OnMoving -= MovePlayerPanel;
        }

        void Update()
        {
            
        }

        private void MovePlayerPanel(Vector2 moveDelta)
        {
            Vector3 inputDirection = moveDelta.x * screenCamera.transform.right + moveDelta.y * screenCamera.transform.forward; 
            Vector3 nextPos = playerPanel.transform.position + inputDirection;
        }
    }
}