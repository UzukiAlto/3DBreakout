using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class BallController : MonoBehaviour
    {
        public event Action OnBallTouched;
        // OnCollisionEnterがBallオブジェクト内でしか検知できないため、イベントを通して検知
        [SerializeField] private GameObject ballObject;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private Ball ball;
        // 外から確認するためのSerializeField
        [SerializeField] private Vector3 moveVec = Vector3.forward;
        [SerializeField] float baseSpeed = 3f;
        private Rigidbody ballRB; 
        private Vector3 initialPos = new Vector3(0f, 0f, -3.25f);
        private Vector3 latestPos; // ボールを進行方向に向けるときに、前フレームの位置を保存
        private Vector3 diff; // ボールの進行方向を計算するために、前フレームの位置との差分を計算
        private Vector3 lastBallVelocity; // ゲームが一時停止されたときのボールの速度を保存


        private void Awake() 
        {
            ballRB = ballObject.GetComponent<Rigidbody>();
            ball = ballObject.GetComponent<Ball>();
        }

        private void OnEnable()
        {
            gameManager.OnGameReady += InitializeBall;
            gameManager.OnGameStarted += LaunchBall;
            gameManager.OnGameFailed += InitializeBall;
            gameManager.OnGameOver += PauseBall;
            gameManager.OnGamePaused += PauseBall;
            gameManager.OnGameUnpaused += UnpauseBall;
            ball.OnCollided += HandleBallCollision;
        }

        private void OnDisable()
        {
            gameManager.OnGameReady -= InitializeBall;
            gameManager.OnGameStarted -= LaunchBall;
            gameManager.OnGameFailed -= InitializeBall;
            gameManager.OnGameOver -= PauseBall;
            gameManager.OnGamePaused -= PauseBall;
            gameManager.OnGameUnpaused -= UnpauseBall;
            ball.OnCollided -= HandleBallCollision;
        }

        public void InitializeBall()
        {
            ballRB.velocity = Vector3.zero;
            ballObject.transform.position = initialPos;
        }  
        public void LaunchBall()
        {
            moveVec.x = UnityEngine.Random.Range(20f, -20f);
            moveVec.y = UnityEngine.Random.Range(20f, -20f);
            moveVec.z = 0f;

            transform.rotation = Quaternion.Euler(moveVec);
            ballRB.AddForce(baseSpeed * transform.forward * GameState.gameSpeed);

            // forecastBall.SetActive(true);
        }

        // ballが何かに当たったときの処理を発火
        public void HandleBallCollision(Collision collision)
        {
            OnBallTouched?.Invoke();
            if (collision.gameObject.TryGetComponent(out IBallTarget target))
            {
                target.OnHitBallObject(ballRB, baseSpeed); 
            }
        }
        void Update()
        {
            LookForward();
        }

        private void LookForward()
        {
            
            // Raycastを前方向に飛ばすために常に進行方向を向かせる
            diff = ballObject.transform.position - latestPos;   
            latestPos = ballObject.transform.position; 

            if (diff.magnitude > 0.01f)
            {
                ballObject.transform.rotation = Quaternion.LookRotation(diff); 
            }
        }

        private void PauseBall()
        {
            lastBallVelocity = ballRB.velocity;
            ballRB.velocity = Vector3.zero;
        }
        private void UnpauseBall(bool wasPlayingBeforePause)
        {
            ballRB.velocity = lastBallVelocity;
        }
    }
}