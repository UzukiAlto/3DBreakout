using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PanelObserver : MonoBehaviour
    {
        public GameObject currentPanel;
        // public GameObject currentPanel { get; private set; }
        [SerializeField] private PlayerRaycastHandler playerRaycastHandler;
        public event Action<GameObject> OnPanelChanged;

        void Update()
        {
            ObservePanel();
        }

        private void ObservePanel()
        {
            if (playerRaycastHandler.hitObject.TryGetComponent(out PlayerPanelArea newPanel))
            {
                if (currentPanel != newPanel.gameObject)
                {
                    currentPanel = newPanel.gameObject;
                    OnPanelChanged?.Invoke(currentPanel);
                }
            }
            else
            {
                if (currentPanel != null)
                {
                    currentPanel = null;
                    OnPanelChanged?.Invoke(null);
                }
            }
        }


    }
}