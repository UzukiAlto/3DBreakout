using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainCameraRaycast : MonoBehaviour
{
    public GameObject mainCamera;
    private Vector3 mainCamePos;
    public GameObject cubeObj;
    private Vector3 cubeObjPos;
    public GameSelectManager gameSelectManager;
    public PlatformManager platformManager;
    public SEManager seManager;

    private float rayLength;
    public LayerMask panelLayer;
    public Color defaultTextCol;
    public Color selectedTextCol;
    public List<TMP_Text> textList;
    private GameObject currentText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mainCamePos = mainCamera.transform.position;
        cubeObjPos = cubeObj.transform.position;
        rayLength = Vector3.Distance(mainCamePos, cubeObjPos);

        Ray ray = new Ray(cubeObjPos, mainCamePos - cubeObjPos);

        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);

        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, rayLength, panelLayer))
        {
            if (Input.GetMouseButtonUp(0) && platformManager.isSmartphone)
            {

                Debug.Log(hit.collider.gameObject.name);
                gameSelectManager.SelectObj(hit.collider.gameObject);
            }
            else if(Input.GetKeyDown(KeyCode.Space) && !platformManager.isSmartphone)
            {

                Debug.Log(hit.collider.gameObject.name);
                gameSelectManager.SelectObj(hit.collider.gameObject);
            }
            foreach (TMP_Text text in textList)
            {
                text.color = defaultTextCol;
            }
            if(hit.collider.gameObject.CompareTag("SelectText"))
            {
                GameObject textObj = hit.collider.transform.Find("Canvas/AdjustTextRotate/Text (TMP)").gameObject;
                if(textObj != currentText)
                {
                    seManager.PlaySE(SEManager.SoundName.select);
                    currentText = textObj;
                }
                TMP_Text text = textObj.GetComponent<TMP_Text>();
                text.color = selectedTextCol;
            }

        }



    }
}
