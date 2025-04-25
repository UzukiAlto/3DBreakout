using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Build.Content;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Vector3 moveVec = Vector3.forward;
    public Player player;
    private Rigidbody myRB;
    public GM gm;
    public GameObject forecastBall;
    public Material forecastBallMaterial;
    public PlayerController playerController;
    public BlockManager blockManager;
    public SEManager seManager;

    private Vector3 initialPos = new Vector3(0f, 0f, -3.25f);

    private float rotateAdjust = 0.8f;

    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        InitializeBall();
        // forecastColorDic = new Dictionary<string, Color>(){
        //     {"hitPlayer", hitPlayerColor}, {"hitPanel", hitPanelColor},
        //     {"hitBlock", hitBlockColor},
        // };
    }

    public void InitializeBall()
    {
        myRB.velocity = Vector3.zero;
        transform.position = initialPos;
        forecastBall.SetActive(false);
        // transform.position

    }
    public void GameOverBall()
    {
        myRB.velocity = Vector3.zero;
        forecastBall.SetActive(false);
    }
    public void LaunchBall()
    {
        moveVec.x = Random.Range(20f, -20f);
        moveVec.y = Random.Range(20f, -20f);
        moveVec.z = 0f;
        // Debug.Log("moveVec " + moveVec);

        transform.rotation = Quaternion.Euler(moveVec);
        // transform.Rotate(moveVec);
        myRB.AddForce(transform.forward * GM.gameSpeed);

        forecastBall.SetActive(true);
    }

    private Vector3 latestPos;
    private Vector3 diff;
    void Update()
    {
        diff = transform.position - latestPos;   
        latestPos = transform.position; 

        if (diff.magnitude > 0.01f && GM.playing)
        {
            transform.rotation = Quaternion.LookRotation(diff); 
        }

        RaycastHit hit;
        Debug.DrawRay(gameObject.transform.position, transform.forward * 100);
        if (GM.playing && Physics.SphereCast(gameObject.transform.position, 0.5f, transform.forward, out hit))
        {
            SetForecast(hit);

            // Gizmos.DrawSphere(transform.position + transform.forward * (hit.distance), 0.5f);
        }

        // transform.position += moveVec * speed * Time.deltaTime;
    }

    private float forecastLength = 5f;
    private float ballAlpha;
    private float maxAlpha = 0.7f;

    private Color hitPlayerColor = new Color(0.46f, 0.92f, 0.40f, 1f);
    private Color hitPanelColor = new Color(0.69f, 0.40f, 0.91f, 1f);
    private Color hitBlockColor = new Color(0.89f, 0.91f, 0.40f, 1f);
    private void SetForecast(RaycastHit hit)
    {
        forecastBall.transform.position = hit.point;
        if(hit.distance <= forecastLength)
        {
            float constA = forecastLength + 1 / (1 + maxAlpha);
            float constB = 1 / constA + maxAlpha;
            ballAlpha = Mathf.Max(0, 1 / (hit.distance - constA) + constB);

        }else{
            ballAlpha = 0f;
        }
        switch (hit.collider.gameObject.tag)
        {
            case "Player":
                forecastBallMaterial.color = hitPlayerColor;
                break;
            case "Block":
                forecastBallMaterial.color = hitBlockColor;
                break;
            case "GameOverRange":
                forecastBallMaterial.color = hitPanelColor;
                break;
        }
        Color ballColor = forecastBallMaterial.color;
        ballColor.a = ballAlpha;
        forecastBallMaterial.color = ballColor;

        // Debug.Log("ballAlpha: " + ballAlpha);
    }

    private Vector3 reflectVec;
    void OnCollisionEnter(Collision collision)
    {
        // moveVec = Vector3.Lerp(moveVec, diff, rotateAdjust);
        // moveVec = Vector3.Reflect(moveVec, collision.contacts[0].normal);

        if(collision.gameObject.CompareTag("Player"))
        {

            reflectVec = transform.position - collision.transform.position;
            reflectVec.Normalize();            
            myRB.velocity = Vector3.zero;
            myRB.AddForce(reflectVec * GM.gameSpeed);

            blockManager.ChangeDefaultLayer();
            seManager.PlaySE(SEManager.SoundName.ball);

        }

        if (collision.gameObject.CompareTag("Block"))
        {
            blockManager.DestroyBlock(collision.gameObject);
            seManager.PlaySE(SEManager.SoundName.ball);
        }

        if(collision.gameObject.CompareTag("GameOverRange"))
        {
            gm.Fail();

            seManager.PlaySE(SEManager.SoundName.fail);
        }


        // Debug.Log("Hit to " + collision.gameObject.name);
        // Debug.Log("法線ベクトル " + collision.contacts[0].normal);
        // Debug.Log("moveVec " + moveVec);
    }
}
