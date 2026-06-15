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
        [SerializeField] private ScreenBase gameScreen;
        [SerializeField] private PanelObserver panelObserver;
        private IPlayerMoveInput playerMoveInput; 
        public void Initialize()
        {
        }
        void Awake()
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
            if (panelObserver.currentOperatedPanel == null)
            {
                Debug.Log("No panel to move on");
                return;
            }

            Vector3 inputDirection = moveDelta.x * gameScreen.screenCamera.transform.right + moveDelta.y * gameScreen.screenCamera.transform.up; 
            inputDirection *= 0.1f; // 移動速度の調整

            // パネルに対して平行に移動させる
            Vector3 normalVector = panelObserver.currentOperatedPanel.GetComponent<PlayerPanelArea>().normalVector;
            Vector3 nextPos = playerPanel.transform.position + Vector3.ProjectOnPlane(inputDirection, normalVector);
            Debug.Log($"ProjectOnPlane result: {Vector3.ProjectOnPlane(inputDirection, normalVector)}");

            playerPanel.transform.position = nextPos;
        }
    }
}