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
            panelObserver.OnSelectedPanelChanged += ChangeSelectingPanelColor;
            panelObserver.OnOperatingPanelChanged += ChangeOperatingPanelColor;
        }
        void OnDisable()
        {
            panelObserver.OnSelectedPanelChanged -= ChangeSelectingPanelColor;
            panelObserver.OnOperatingPanelChanged -= ChangeOperatingPanelColor;
        }

        public void ChangeSelectingPanelColor(GameObject selectObj)
        {
            if (selectObj == null || panelObserver.currentOperatingPanel == null || !selectObj.TryGetComponent(out Renderer selectingRenderer))
            {
                return;
            }
            Renderer operatingRenderer = panelObserver.currentOperatingPanel.GetComponent<Renderer>();
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
        public void ChangeOperatingPanelColor(GameObject operateObj)
        {
            if (operateObj == null || !operateObj.TryGetComponent(out Renderer operatingRenderer))
            {
                Debug.LogWarning("操作対象のオブジェクトがnullまたはRendererがありません");
                return;
            }
            foreach (Renderer renderer in panelRendererList)
            {
                if(renderer != operatingRenderer)
                {
                    renderer.material = defaultMaterial;
                }
            }
            operatingRenderer.material = operatingMaterial;
        }
    }
}