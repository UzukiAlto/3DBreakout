using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPanelManager : MonoBehaviour
{
    public SEManager seManager;
    public GameObject playerPanels;
    private GameObject panel1, panel2, panel3, panel4, panel5, panel6;

    [SerializeField] private GameObject operatingObj;
    [SerializeField] private List<GameObject> panelList;

    public Material operatingMaterial;
    public Material selectingMaterial;
    public Material defaultMaterial;
    // Start is called before the first frame update
    void Start()
    {
        panelList = new List<GameObject>(){
            panel1, panel2, panel3, panel4, panel5, panel6
        };

        for (int i = 0; i < panelList.Count; i++)
        {
            panelList[i] = playerPanels.transform.GetChild(i).gameObject;
        }

        foreach (GameObject obj in panelList)
        {
            obj.GetComponent<Renderer>().material = defaultMaterial;
        }

        operatingObj = panelList[0];
        InitializePanel();


    }
    public void InitializePanel()
    {
        ChangeOperatingPanel(panelList[0]);
    }

    public void ChangeOperatingPanel(GameObject panel)
    {
        // Debug.Log("ChangeOperatingPanel: " + panel.name);
        if(operatingObj != null)
        {
            operatingObj.GetComponent<Renderer>().material = defaultMaterial;
            // seManager.PlaySE(SEManager.SoundName.select);
        }

        panel.GetComponent<Renderer>().material = operatingMaterial;
        operatingObj = panel;
    }

    public void SelectingPanel(GameObject selectObj)
    {
        foreach (GameObject obj in panelList)
        {
            if(obj != operatingObj)
            {
                obj.GetComponent<Renderer>().material = defaultMaterial;
            }
        }
        if(selectObj != operatingObj)
        {
            // Debug.Log("SelectingPanel: " + selectObj.name);
            selectObj.GetComponent<Renderer>().material = selectingMaterial;
        }
    }
}
