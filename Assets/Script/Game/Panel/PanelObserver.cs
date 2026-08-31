using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PanelObserver : MonoBehaviour
    {
        public GameObject currentSelectedPanel { get; private set; }
        public GameObject currentOperatingPanel { get; private set; }
        [SerializeField] private PlayerRaycastHandler playerRaycastHandler;
        public event Action<GameObject> OnSelectedPanelChanged;
        public event Action<GameObject> OnOperatingPanelChanged;

        void Update()
        {
            ObservePanel();
        }

        private void ObservePanel()
        {
            if (playerRaycastHandler.hitObject == null) 
            {
                return;
            }

            PlayerPanelArea newPanel = playerRaycastHandler.hitObject.GetComponent<PlayerPanelArea>();
            GameObject newPanelObject = newPanel?.gameObject;

            if (currentSelectedPanel != newPanelObject)
            {
                currentSelectedPanel = newPanelObject;
                OnSelectedPanelChanged?.Invoke(currentSelectedPanel);
            }
        }

        public void SetCurrentOperatingPanel(GameObject panel)
        {
            if (panel == null)
            {
                Debug.Log("SetCurrentOperatingPanel: panel is null");
                return;
            }
            currentOperatingPanel = panel;
            OnOperatingPanelChanged?.Invoke(currentOperatingPanel);
        }


    }
}