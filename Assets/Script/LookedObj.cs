using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookedObj : MonoBehaviour
{
    public GameObject mainCamera;
    private Vector3 lastMousePosition;
    public GameObject selectCube;
    public GameObject lookedObj;

    private float rotationSpeed = 2.5f;
    private bool canRote = false;
    private Vector3 diffRote;
    void Start()
    {

        transform.rotation = mainCamera.transform.rotation;
        transform.position = mainCamera.transform.position;
        lastMousePosition = Input.mousePosition;
        canRote = true;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();

    }
    private Vector3 newAngle;
    private void Rotate()
    {
        if (Input.GetMouseButton(0))
        {

            Vector3 nowMouseValue = Input.mousePosition - lastMousePosition;

            newAngle = Vector3.zero;

            newAngle.x = rotationSpeed * Input.GetAxis("Mouse X");
            newAngle.y = rotationSpeed * Input.GetAxis("Mouse Y");



            if(canRote)
            {
                transform.RotateAround(selectCube.transform.position, mainCamera.transform.up, -newAngle.x);
                transform.RotateAround(selectCube.transform.position, mainCamera.transform.right, newAngle.y);

                selectCube.transform.LookAt(transform.position);
            }
        }

        lastMousePosition = Input.mousePosition;
    }


}
