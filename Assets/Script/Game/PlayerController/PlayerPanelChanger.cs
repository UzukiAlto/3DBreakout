using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerPanelChanger : MonoBehaviour, IInitializable
    {
        [SerializeField] private PanelObserver panelObserver;
        [SerializeField] private GameObject initialPanel;
        public void Initialize()
        {
            panelObserver.SetCurrentOperatedPanel(initialPanel);
        }
        void Start()
        {
            
        }

        void Update()
        {
            
        }
    }
}