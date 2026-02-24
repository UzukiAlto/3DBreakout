using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete("リファクタリング移行中")]
public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float playerSpeed;
    public GameObject subCamera;
    public PlayerController playerController;
    public Vector3 playerRange;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerSpeed = GM.gameSpeed;
        if(!GM.playing)
        {
            return;
        }else{
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += subCamera.transform.up * playerSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position -= subCamera.transform.right * playerSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position -= subCamera.transform.up * playerSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += subCamera.transform.right * playerSpeed * Time.deltaTime;
            }
        }
        

        Vector3 currentPos = transform.position;

        //追加　Mathf.ClampでX,Yの値それぞれが最小～最大の範囲内に収める。
        //範囲を超えていたら範囲内の値を代入する        
        currentPos.x = Mathf.Clamp(currentPos.x, -playerRange.x, playerRange.x);
        currentPos.y = Mathf.Clamp(currentPos.y, -playerRange.y, playerRange.y);
        currentPos.z = Mathf.Clamp(currentPos.z, -playerRange.z, playerRange.z);
        switch (playerController.currentPlayerDirection)
        {
            case PlayerController.Direction.Up:
                currentPos.y = playerRange.y;
                break;

            case PlayerController.Direction.Down:
                currentPos.y = -playerRange.y;
                break;

            case PlayerController.Direction.Left:
                currentPos.x = -playerRange.x;
                break;

            case PlayerController.Direction.Right:
                currentPos.x = playerRange.x;
                break;

            case PlayerController.Direction.Forward:
                currentPos.z = -playerRange.z;
                break;

            case PlayerController.Direction.Behind:
                currentPos.z = playerRange.z;
                break;
        }



        transform.position = currentPos;
    }
}
