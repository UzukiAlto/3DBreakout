using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainSystem;

namespace Game
{
    public class PlayerPanelMover : MonoBehaviour
    {
        [SerializeField] private GameObject playerMoveInputObject;
        [SerializeField] private GameObject playerPanel;
        [SerializeField] private ScreenBase gameScreen;
        [SerializeField] private PanelObserver panelObserver;
        [SerializeField] private GameManager gameManager;
        private IPlayerMoveInput playerMoveInput; 
        private Vector3 initialPlayerPosition = new Vector3(0f, 0f, -4.7f);
        private Quaternion initialPlayerRotation = Quaternion.identity;
        private float baseMoveSpeed = 0.03f;
        void Awake()
        {
            playerMoveInput = playerMoveInputObject.GetComponent<IPlayerMoveInput>();            
        }
        private void OnEnable() {
            playerMoveInput.OnMoving += MovePlayerPanel;
            gameManager.OnGameReady += InitializePlayerPosition;
            gameManager.OnGameFailed += InitializePlayerPosition;
        }
        private void OnDisable() {
            playerMoveInput.OnMoving -= MovePlayerPanel;
            gameManager.OnGameReady -= InitializePlayerPosition;
            gameManager.OnGameFailed -= InitializePlayerPosition;
        }
        private void InitializePlayerPosition()
        {
            playerPanel.transform.position = initialPlayerPosition;
            playerPanel.transform.rotation = initialPlayerRotation;
        }


        private void MovePlayerPanel(Vector2 moveDelta)
        {
            if (!gameScreen.canOperate)
            {
                return;
            }
            if (panelObserver.currentOperatingPanel == null)
            {
                Debug.Log("No panel to move on");
                return;
            }

            Vector3 inputDirection = moveDelta.x * gameScreen.screenCamera.transform.right + moveDelta.y * gameScreen.screenCamera.transform.up; 
            // 移動速度の調整
            inputDirection *= baseMoveSpeed * GameState.gameSpeed; 

            PlayerPanelArea currentPanelArea = panelObserver.currentOperatingPanel.GetComponent<PlayerPanelArea>();
            // パネルに対して平行に移動させる
            Vector3 normalVector = currentPanelArea.normalVector;
            Vector3 nextPos = playerPanel.transform.position + Vector3.ProjectOnPlane(inputDirection, normalVector);

            // プレイヤーパネルが移動できる範囲を制限する
            Bounds panelBounds = currentPanelArea.moveableAreaBounds;
            nextPos = new Vector3(
                Mathf.Clamp(nextPos.x,  panelBounds.min.x, panelBounds.max.x),
                Mathf.Clamp(nextPos.y, panelBounds.min.y, panelBounds.max.y),
                Mathf.Clamp(nextPos.z, panelBounds.min.z, panelBounds.max.z)
            );
            playerPanel.transform.position = nextPos;
        }
    }
}