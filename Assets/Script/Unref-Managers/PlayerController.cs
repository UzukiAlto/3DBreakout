using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Obsolete("リファクタリング移行中")]
public class PlayerController : MonoBehaviour
{
    public GameObject player;
    // public GameObject playerRange;
    public GameObject mainCamera;
    public GameObject ball;


    public GameObject playerPanels;
    private GameObject panel1, panel2, panel3, panel4, panel5, panel6;
    [SerializeField] private List<GameObject> panelList;
    public enum Direction 
    {
        Up, Down, Left, Right, Forward, Behind
    }

    public Direction currentPlayerDirection;

    private Dictionary<GameObject, Direction> panelDirectionDic;
    private float defaultPos = 4.25f;

    void Start()
    {
        panelList = new List<GameObject>(){
            panel1, panel2, panel3, panel4, panel5, panel6
        };

        for (int i = 0; i < panelList.Count; i++)
        {
            panelList[i] = playerPanels.transform.GetChild(i).gameObject;
        }

        panelDirectionDic = new Dictionary<GameObject,Direction>(){
            {panelList[0], Direction.Forward}, {panelList[1], Direction.Left},
            {panelList[2], Direction.Right}, {panelList[3], Direction.Behind},
            {panelList[4], Direction.Up},{panelList[5], Direction.Down},
        };
        InitializePlayer();
    }
    public void InitializePlayer()
    {
        TransformObj(panelList[0]);
    }


    void Update()
    {
        // if (Input.GetKey(KeyCode.Alpha1))
        // {
        //     TransformObj(Direction.Forward);
        // }
        // if (Input.GetKey(KeyCode.Alpha2))
        // {
        //     TransformObj(Direction.Left);
        // }
        // if (Input.GetKey(KeyCode.Alpha3))
        // {
        //     TransformObj(Direction.Up);
        // }

    }

    public void TransformObj(GameObject panelObj)
    {
        currentPlayerDirection = panelDirectionDic[panelObj];
        Vector3 pos = Vector3.zero;
        Vector3 cameraPos = Vector3.zero;
        Quaternion quaternion = Quaternion.identity;


        // Debug.Log("Transform: " + currentPlayerDirection.ToString());

        switch (currentPlayerDirection)
        {
            case Direction.Up:
                pos = new Vector3(0f, defaultPos, 0f);
                cameraPos = new Vector3(0f, 16f, 0f);
                quaternion = Quaternion.Euler(90f, 0f, 0f);
                break;

            case Direction.Down:
                pos = new Vector3(0f, -defaultPos, 0f);
                cameraPos = new Vector3(0f, -16f, 0f);
                quaternion = Quaternion.Euler(-90f, 0f, 0f);
                break;

            case Direction.Left:
                pos = new Vector3(-defaultPos, 0f, 0f);
                cameraPos = new Vector3(-16f, 0f, 0f);
                quaternion = Quaternion.Euler(0f, 90f, 0f);
                break;

            case Direction.Right:
                pos = new Vector3(defaultPos, 0f, 0f);
                cameraPos = new Vector3(16f, 0f, 0f);
                quaternion = Quaternion.Euler(0f, -90f, 0f);
                break;

            case Direction.Forward:
                pos = new Vector3(0f, 0f, -defaultPos);
                cameraPos = new Vector3(0f, 0f, -16f);
                quaternion = Quaternion.Euler(0f, 0f, 0f);
                break;

            case Direction.Behind:
                pos = new Vector3(0f, 0f, defaultPos);
                cameraPos = new Vector3(0f, 0f, 16f);
                quaternion = Quaternion.Euler(180f, 0f, 0f);
                break;
        }
        // if(!GM.playing)
        // {
        //     ball.transform.position = pos + transform.forward;
        //     ball.transform.LookAt(this.transform);
        //     ball.transform.Rotate(180, 180, 0);
        // }

        player.transform.position = pos;
        player.transform.rotation = quaternion;


    }
}
