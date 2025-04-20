using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateText : MonoBehaviour
{
    public GameObject cameraObj;
    // Start is called before the first frame update
    void Start()
    {
        cameraObj = GameObject.Find("Main Camera");
    }

    public bool isRotateText = true;
    void Update()
    {
        if(isRotateText)
        {
            Vector3 rot = Vector3.zero;
            rot.z = cameraObj.transform.eulerAngles.z;
            // Debug.Log($"rot: {rot}\ntransfom: {gameObject.transform.eulerAngles}");
            gameObject.transform.localEulerAngles = rot;
        }
    }
}
