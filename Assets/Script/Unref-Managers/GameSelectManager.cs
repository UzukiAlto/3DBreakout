
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Obsolete("リファクタリング移行中")]
public class GameSelectManager : MonoBehaviour
{
    public SEManager seManager;
    public GameObject mainCamera;
    public GameObject subCamera;
    public GameObject cubeObj;
    public GameObject gameScene;
    public GameObject homeScene;
    public GameObject gameMode;
    public GameObject difficulty;
    public GameObject configText;
    public GameObject configObjects;
    public GM gm;
    private bool selectStartGame = false;
    private bool FlagGameStartRote = false;
    private Vector3 selectDifficultyPos = new Vector3(0f, 0f, -20f);
    public float slerpSpeed;
    public bool canOperate;
    void Start()
    {
        // gameScene.SetActive(false);
        // homeScene.SetActive(false);
        // homeScene.SetActive(true);
        // gameMode.SetActive(true);
        difficulty.SetActive(false);
        canOperate = true;
    }

    void Update()
    {
        if (selectStartGame)
        {
            SelectStartGame();
            if(endMoveGamePos)
            {
                selectStartGame = false;
                canOperate = true;
            }
        }
        if (FlagGameStartRote)
        {
            SelectDifficulty();
            if(endMoveGamePos && endMoveGameRote)
            {
                FlagGameStartRote = false;
                canOperate = true;
                gm.ChangeToGameCamera();
                homeScene.SetActive(false);
            }
        }
    }
        public void SelectStartGame()
    {
        MoveGamePos();
        mainCamera.transform.LookAt(cubeObj.transform.position);
    }
    private Vector3 lastSlerpPos;
    private bool firstLimit = false;
    private Vector3 limitSlerpPos;
    private float limitTime;
    private float limitSpeed = 3f;
    private float lowLimitVelocity = 0.1f;
    private bool endMoveGamePos = false;

    private void MoveGamePos()
    {
        Debug.Log("MoveGamePos");
        Vector3 endPos = selectDifficultyPos - cubeObj.transform.position;
        Vector3 startPos = mainCamera.transform.position - cubeObj.transform.position;
        Vector3 newSlerpPos = Vector3.Slerp(startPos, endPos, slerpSpeed);
        Vector3 diffSlerpPos = newSlerpPos - lastSlerpPos;
        if (diffSlerpPos.sqrMagnitude < Mathf.Pow(lowLimitVelocity, 2))
        {
            if (!firstLimit)
            {
                firstLimit = true;
                limitSlerpPos = startPos;
                limitTime = 0f;
            }
            else
            {
                newSlerpPos = Vector3.Slerp(limitSlerpPos, endPos, limitTime);
                limitTime += Time.deltaTime * limitSpeed;
            }

        }

        Vector3 slerpPos = newSlerpPos + cubeObj.transform.position;
        mainCamera.transform.position = slerpPos;
        if (Vector3.Distance(mainCamera.transform.position, selectDifficultyPos) < 0.1f)
        {

            mainCamera.transform.position = selectDifficultyPos;
            selectStartGame = false;
            endMoveGamePos = true;
            // canOperate = true;
            firstLimit = false;

            mainCamera.SetActive(false);
            subCamera.SetActive(true);
        }
        lastSlerpPos = newSlerpPos;
    }

    private float lerpSpeedRote = 0.04f;
    private bool endMoveGameRote = false;

    private Quaternion playGameRote = Quaternion.identity;

    private void MoveGameRote()
    {
        Quaternion endRote = playGameRote;
        Quaternion startRote = mainCamera.transform.rotation;
        Quaternion newLerpRote = Quaternion.Lerp(startRote, endRote, lerpSpeedRote);
        mainCamera.transform.rotation = newLerpRote;
        if (Quaternion.Angle(mainCamera.transform.rotation, playGameRote) < 0.1f)
        {
            Debug.Log("end");
            mainCamera.transform.rotation = playGameRote;
            // FlagGameStartRote = false;
            endMoveGameRote = true;
            // canOperate = true;
        }
    }
    public void SelectObj(GameObject obj)
    {
        switch (obj.name)
        {
            case "GAME START":
                canOperate = false;
                FlagGameStartRote = true;
                endMoveGameRote = false;
                endMoveGamePos = false;
                // selectStartGame = true;
                gameScene.SetActive(true);

                // selectStartGame = true;
                // canOperate = false;
                // endMoveGamePos = false;
                // difficulty.SetActive(true);
                seManager.PlaySE(SEManager.SoundName.transition);


                Debug.Log("select GAME START");
                break;

            case "CONFIG":
                canOperate = false;
                configText.SetActive(false);
                configObjects.SetActive(true);
                Debug.Log("select CONFIG");
                break;


        }

        if (obj.name == "Normal" || obj.name == "Hard" || obj.name == "Score Attack")
        {
            canOperate = false;
            FlagGameStartRote = true;
            endMoveGameRote = false;
            endMoveGamePos = false;
            // selectStartGame = true;
            gameScene.SetActive(true);
            // gm.SetAssistPanel();
        }
    }


    public void SelectDifficulty()
    {
        MoveGamePos();
        MoveGameRote();
    }
}
