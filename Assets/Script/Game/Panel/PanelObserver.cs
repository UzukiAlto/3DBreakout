using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PanelObserver : MonoBehaviour
    {
        // public GameObject currentSelectedPanel;
        public GameObject currentSelectedPanel { get; private set; }
        public GameObject currentOperatedPanel { get; private set; }
        [SerializeField] private PlayerRaycastHandler playerRaycastHandler;
        public event Action<GameObject> OnSelectedPanelChanged;

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

            if (playerRaycastHandler.hitObject.TryGetComponent(out PlayerPanelArea newPanel))
            {
                if (currentSelectedPanel != newPanel.gameObject)
                {
                    currentSelectedPanel = newPanel.gameObject;
                    OnSelectedPanelChanged?.Invoke(currentSelectedPanel);
                }
            } else
            {
                if (currentSelectedPanel != null)
                {
                    currentSelectedPanel = null;
                    OnSelectedPanelChanged?.Invoke(null);
                }
            }
        }

        public void SetCurrentOperatedPanel(GameObject panel)
        {
            currentOperatedPanel = panel;
        }


    }
}