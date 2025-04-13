using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCamera : MonoBehaviour
{
    public GameObject centerObj;
    public PlayerController playerController;

    private float rotationSpeed;           //回転速度
    private Vector3 lastMousePosition;      //最後のマウス座標

    private Vector3 initializePos = new Vector3(0f,0f, -20f);
    private Vector3 initializeRot = Vector3.zero;



    void Start()
    {
        lastMousePosition = Input.mousePosition;
    }
    void Update()
    {
        rotationSpeed = GM.gameSpeed / 2f;
        if(GM.playing)
        {

            Rotate();
        }
        if(initializing)
        {
            transform.position = Vector3.Slerp(transform.position, initializePos, slerpSpeed);
            transform.LookAt(centerObj.transform.position);
            if(Vector3.Distance(transform.position, initializePos) < 0.1f)
            {
                Debug.Log("transform.position == initializePos");
                transform.position = initializePos;
                initializing = false;
            }
        }
    }


    void Rotate()
    {
        if (Input.GetMouseButton(0))
        {

            Vector3 nowMouseValue = Input.mousePosition - lastMousePosition;

            var newAngle = Vector3.zero;
            // newAngle.x = rotationSpeed.x * nowMouseValue.x;
            // newAngle.y = rotationSpeed.y * nowMouseValue.y;

            newAngle.x = rotationSpeed * Input.GetAxis("Mouse X");
            newAngle.y = rotationSpeed * Input.GetAxis("Mouse Y");

            transform.RotateAround(centerObj.transform.position, transform.up, newAngle.x);
            transform.RotateAround(centerObj.transform.position, transform.right, -newAngle.y);

        }

        lastMousePosition = Input.mousePosition;
    }

    private bool initializing = false;
    private float slerpSpeed = 0.05f;
    public void InitializeCamera()
    {
        initializing = true;
    }
}
