using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainSystem;

namespace Game
{
    public class PanelColorChanger : MonoBehaviour, IInitializable
    {
        [SerializeField] private List<Renderer> panelRendererList;
        [SerializeField] private Renderer defaultRenderer;

        [SerializeField] private Material operatingMaterial;
        [SerializeField] private Material selectingMaterial;
        [SerializeField] private Material defaultMaterial;
        
        [SerializeField] private PanelObserver panelObserver;

        public void Initialize()
        {
            foreach (Renderer renderer in panelRendererList)
            {
                renderer.material = defaultMaterial;
            }
            defaultRenderer.material = operatingMaterial;
        }

        void OnEnable()
        {
            panelObserver.OnSelectedPanelChanged += SelectingPanel;
        }
        void OnDisable()
        {
            panelObserver.OnSelectedPanelChanged -= SelectingPanel;
        }

        public void SelectingPanel(GameObject selectObj)
        {
            Renderer selectingRenderer = selectObj.GetComponent<Renderer>();
            Renderer operatingRenderer = panelObserver.currentOperatedPanel.GetComponent<Renderer>();
            foreach (Renderer renderer in panelRendererList)
            {
                if(renderer != operatingRenderer)
                {
                    renderer.material = defaultMaterial;
                }
            }
            if(selectingRenderer != operatingRenderer)
            {
                selectingRenderer.material = selectingMaterial;
            }
        }
    }
}