using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    public GameObject subCamera;
    private Vector3 subCamePos;
    public GameObject centerObj;
    private Vector3 centerObjPos;

    private float rayLength;
    public LayerMask panelLayer;

    public PlayerPanelManager playerPanelManager;
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        subCamePos = subCamera.transform.position;
        centerObjPos = centerObj.transform.position;
        rayLength = Vector3.Distance(subCamePos, centerObjPos);

        Ray ray = new Ray(centerObjPos, subCamePos);

        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.green);

        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, rayLength, panelLayer))
        {
            playerPanelManager.SelectingPanel(hit.collider.gameObject);

        }

        if(Input.GetKey(KeyCode.Space) && GM.playing)
        {
            playerPanelManager.ChangeOperatingPanel(hit.collider.gameObject);
            playerController.TransformObj(hit.collider.gameObject);
            // GM.Launch()
        }
       

    }
}
