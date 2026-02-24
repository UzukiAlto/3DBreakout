using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Obsolete("リファクタリング移行中")]
public class TutorialCamera : MonoBehaviour
{
    public GameObject tutorialCamera;
    private Vector3 tutorialCameraPos;
    public GameObject cubeObj;
    private Vector3 cubeObjPos;
    public PlatformManager platformManager;
    public SEManager seManager;

    private float rayLength;
    public LayerMask panelLayer;
    public Color defaultTextCol;
    public Color selectedTextCol;
    public List<TMP_Text> textList;
    private GameObject currentText;

    private float rotationSpeed = 2.5f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Raycast();
        CheckPlayerTap();

    }
    private Vector3 newAngle;
    private bool canRotate = true;
    private void Rotate()
    {
        if (Input.GetMouseButton(0))
        {

            newAngle = Vector3.zero;

            newAngle.x = rotationSpeed * Input.GetAxis("Mouse X");
            newAngle.y = rotationSpeed * Input.GetAxis("Mouse Y");

            if(canRotate) 
            {
                transform.RotateAround(cubeObj.transform.position, transform.up, newAngle.x);
                transform.RotateAround(cubeObj.transform.position, transform.right, -newAngle.y);
            }
        }
        
    }
    private Ray ray;

    private void Raycast()
    {
        tutorialCameraPos = tutorialCamera.transform.position;
        cubeObjPos = cubeObj.transform.position;
        rayLength = Vector3.Distance(tutorialCameraPos, cubeObjPos);

        ray = new Ray(cubeObjPos, tutorialCameraPos - cubeObjPos);

        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);

        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, rayLength, panelLayer))
        {

            foreach (TMP_Text text in textList)
            {
                text.color = defaultTextCol;
            }
            if (hit.collider.gameObject.CompareTag("SelectText"))
            {
                GameObject textObj = hit.collider.transform.Find("Canvas/AdjustTextRotate/Text (TMP)").gameObject;
                if (textObj != currentText)
                {
                    seManager.PlaySE(SEManager.SoundName.select);
                    currentText = textObj;
                }
                TMP_Text text = textObj.GetComponent<TMP_Text>();
                text.color = selectedTextCol;
            }

        }
    }
    private Vector3 startMousePos;
    private float startMouseTime;

    private float limitTapSecond = 0.4f;

    private float limitTapDistance = 20f;
    [SerializeField]
    private bool isSwiping = false;
    private void CheckPlayerTap()
    {
        if(Input.GetMouseButtonDown(0))
        {
            startMousePos = Input.mousePosition;
            startMouseTime = Time.time;
            isSwiping = false;
        }
        Vector3 diffMousePos = new Vector3();
        float elapsedTime = 0f;
        if (Input.GetMouseButton(0))
        {
            diffMousePos = Input.mousePosition - startMousePos;
            elapsedTime = Time.time - startMouseTime;

            if (diffMousePos.sqrMagnitude > limitTapDistance * limitTapDistance
            || elapsedTime > limitTapSecond)
            {
                isSwiping = true;
            }
        }
        if(Input.GetMouseButtonUp(0)) //タップしていたかの判定
        {
            if (diffMousePos.sqrMagnitude <= limitTapDistance * limitTapDistance
            && elapsedTime <= limitTapSecond && isSwiping == false && platformManager.isSmartphone)
            {
                SelectGameObject();
            }
        }
        
        if(platformManager.isSmartphone == false && Input.GetKey(KeyCode.Space))
        {
            SelectGameObject();
        }
    }
    private void SelectGameObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLength, panelLayer))
        {
            Debug.Log(hit.collider.gameObject.name);
            // gameSelectManager.SelectObj(hit.collider.gameObject);
        }
    }

}
