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
        private float baseMoveSpeed = 0.1f;
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


        private void MovePlayerPanel(Vector2 moveDelta)
        {
            if (panelObserver.currentOperatingPanel == null)
            {
                Debug.Log("No panel to move on");
                return;
            }

            Vector3 inputDirection = moveDelta.x * gameScreen.screenCamera.transform.right + moveDelta.y * gameScreen.screenCamera.transform.up; 
            // 移動速度の調整
            inputDirection *= baseMoveSpeed * gameManager.gameSpeed; 

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