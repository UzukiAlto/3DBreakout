using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainSystem;

namespace Game
{
    public class PlayerPanelChanger : MonoBehaviour, IInitializable
    {
        [SerializeField] private PanelObserver panelObserver;
        [SerializeField] private GameObject initialPanel;
        [SerializeField] private GameObject playerMoveInputObject;
        [SerializeField] private GameObject playerPanel;
        private IPlayerMoveInput playerMoveInput;
        // playerPanelを前に少しずらすためのオフセット値
        private float panelFrontOffset = 0.75f; 
        public void Initialize()
        {
            panelObserver.SetCurrentOperatingPanel(initialPanel);
        }
        private void Awake()
        {
            playerMoveInput = playerMoveInputObject.GetComponent<IPlayerMoveInput>();
        }

        private void OnEnable()
        {
            playerMoveInput.OnChangePanel += ChangePanel;
        }
        private void OnDisable()
        {
            playerMoveInput.OnChangePanel -= ChangePanel;
        }

        public void ChangePanel(bool isPressed)
        {
            if (!isPressed || panelObserver == null)
            {
                return;
            }

            panelObserver.SetCurrentOperatingPanel(panelObserver.currentSelectedPanel);
            
            Transform operatingPanelTransform = panelObserver.currentOperatingPanel.transform;
            playerPanel.transform.position = operatingPanelTransform.position + operatingPanelTransform.forward * panelFrontOffset;
            playerPanel.transform.rotation = operatingPanelTransform.rotation;


        }
    }
}