using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GM : MonoBehaviour
{
    public static GM instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public Ball ballScr;
    public SubCamera subCamera;
    public PlayerPanelManager panelManager;
    public PlayerController playerController;
    public SEManager seManager;
    public BlockManager blockManager;
    public GameObject ballObj;
    public List<GameObject> livesList;
    public GameObject livesObj;
    public GameObject retryText;
    public GameObject startText;
    public GameObject gameSpeedText;
    public Camera gameCamera;
    public Camera homeCamera;
    private GameSpeedText gameSpeedTextScr;
    public static float gameSpeed;
    public static float defaultgameSpeed = 6f;
    public static int defaultLives = 3;
    public static int maxLives = 5;
    public static int currentLives;
    public static int currentStage;
    public static int currentBlockType;
    public static bool playing;
    public static bool selecting;
    public static bool GameOver;
    public static bool GameStart;

    public enum Difficulty
    {
        Normal, Hard, ScoreAttack
    }

    public Difficulty currentDifficulty;

    private void Start() {
        gameSpeed = defaultgameSpeed;
        playing = false;
        GameOver = false;
        GameStart = false;
        currentLives = defaultLives;
        currentStage = 1;
        gameSpeedTextScr = gameSpeedText.GetComponent<GameSpeedText>();
        GetLives();
        startText.SetActive(true);
        // InitializeGame();

    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !playing && !GameOver && GameStart)
        {
            Launch();
            startText.SetActive(false);
        }
        if(GameOver && Input.GetKey(KeyCode.R))
        {
            InitializeGame();
            blockManager.InitializeBloack();
        }
    }
    private void GetLives()
    {
        livesList = new List<GameObject>();
        Transform children = livesObj.GetComponentInChildren<Transform>();
        if(children.childCount == 0)
        {
            return;
        }
        foreach(Transform obj in children)
        {
            if(obj.gameObject.CompareTag("Lives"))
            {
                livesList.Add(obj.gameObject);
                obj.gameObject.SetActive(true);
                Debug.Log("livesList.Add(obj.gameObject)");
            }
        }
        for(int i = defaultLives; i < maxLives; i++)
        {

            livesList[i].SetActive(false);
        }

    }
    private void IncreaseLives()
    {
        if(currentLives < maxLives && currentStage % 5 == 0)
        {
            currentLives++;
            livesList[currentLives - 1].SetActive(true);
        }

    }
    public void Launch()
    {
        playing = true;
        ballScr.LaunchBall();
        seManager.PlaySE(SEManager.SoundName.ball);

    }
    public void Fail()
    {
        playing = false;
        if(currentLives > 0)
        {

            Debug.Log("Fail");
            livesList[currentLives - 1].SetActive(false);
            currentLives--;
            startText.SetActive(true);
            InitializeGame();
        }
        else{
            ballScr.GameOverBall();
            GameOver = true;
            retryText.SetActive(true);
            Debug.Log("Game Over");
        }
    }

    private void InitializeGame()
    {
        if (GameOver)
        {
            gameSpeed = defaultgameSpeed;
            currentLives = defaultLives;
            GameOver = false;
            retryText.SetActive(false);
            startText.SetActive(true);
            GetLives();
        }
        ballScr.InitializeBall();
        subCamera.InitializeCamera();
        panelManager.InitializePanel();
        playerController.InitializePlayer();
        gameSpeedTextScr.ChangeText();
    }

    public void NextStage()
    {
        IncreaseGameSpeed();
        IncreaseLives();
        seManager.PlaySE(SEManager.SoundName.next);
        currentStage ++;
        // playing = false;
        // SetAssistPanel();
        // InitializeGame();
    }

    private void IncreaseGameSpeed()
    {
        if(currentStage % 3 == 0)
        {
            gameSpeed += 1f;
        }
        
        gameSpeedTextScr.ChangeText();
    }

    public void ChangeToGameCamera()
    {
        homeCamera.enabled = false;
        gameCamera.enabled = true;
        GameStart = true;
    }

    // public GameObject assistPanel;
    // private List<Vector3> assistPanelPos = new List<Vector3>(){
    //     new Vector3(4.25f, 0f, 0f), new Vector3(-4.25f, 0f, 0f), new Vector3(0f, 4.25f, 0f),
    //     new Vector3(0f, -4.25f, 0f), new Vector3(0f, 0f, 4.25f)

    // };
    // private List<Vector3> assistPanelRote = new List<Vector3>(){
    //     new Vector3(0f, -90f, 0f), new Vector3(0f, 90f, 0f), new Vector3(90f, 0f, 0f),
    //     new Vector3(-90f, 0f, 0f), new Vector3(0f, 0f, 0f),
    // };

    // public void SetAssistPanel()
    // {
    //     if(currentDifficulty == Difficulty.Normal)
    //     {
    //         List<Vector3> posList = assistPanelPos;
    //         List<Vector3> roteList = assistPanelRote;

    //         for (int i = 0; i < 2; i ++)
    //         {
    //             int rand = Random.Range(0, 4 -i);
    //             Instantiate (assistPanel, posList[rand], Quaternion.Euler(roteList[rand]));
    //             posList.RemoveAt(rand);
    //             roteList.RemoveAt(rand);
    //         }
    //     }
    // }

}
