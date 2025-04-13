using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject selectCube;
    public GameObject lookedObj;
    public GameSelectManager gameSelectManager;

    private float rotationSpeed = 2.5f;
    private bool canRote = true;
    void Start()
    {
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

            newAngle = Vector3.zero;

            newAngle.x = rotationSpeed * Input.GetAxis("Mouse X");
            newAngle.y = rotationSpeed * Input.GetAxis("Mouse Y");



            if(gameSelectManager.canOperate)
            {
                transform.RotateAround(selectCube.transform.position, transform.up, newAngle.x);
                transform.RotateAround(selectCube.transform.position, transform.right, -newAngle.y);
            }
        }
    }

}
