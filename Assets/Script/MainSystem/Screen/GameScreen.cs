using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

namespace MainSystem
{
    /// <summary>
    /// ゲームスクリーンの管理を行うクラス
    /// </summary>
    public class GameScreen : ScreenBase
    {
        [SerializeField] private GameManager gameManager;
        // 初期化の順番を変更できるようにリストで管理
        [SerializeField] private List<GameObject> initializableList = new List<GameObject>();
        // gameManager.GameStartedとSetEnableOperationは引数が異なるためeventで中継
        private event Action setEnableOperationEvent;
        private event Action setDisableOperationEvent;
        protected override void Awake()
        {
            base.Awake();
            setEnableOperationEvent = () => SetEnableOperation(true);
            setDisableOperationEvent = () => SetEnableOperation(false);
        }
        private void OnEnable()
        {
            gameManager.OnGameStarted += setEnableOperationEvent;
            gameManager.OnGameFailed += setDisableOperationEvent;
            gameManager.OnGameOver += setDisableOperationEvent;
            gameManager.OnGameReady += Initialize;
            gameManager.OnGamePaused += setDisableOperationEvent;
            gameManager.OnGameUnpaused += SetEnableOperation; // ポーズ前にプレイ中だったなら操作可能にする
        }
        private void OnDisable()
        {
            gameManager.OnGameStarted -= setEnableOperationEvent;
            gameManager.OnGameFailed -= setDisableOperationEvent;
            gameManager.OnGameOver -= setDisableOperationEvent;
            gameManager.OnGameReady -= Initialize;
            gameManager.OnGamePaused -= setDisableOperationEvent;
            gameManager.OnGameUnpaused -= SetEnableOperation;
        }
        public override void Hide()
        {
            base.Hide();
            Initialize();
            screenCanvas.SetActive(false);
            SetEnableOperation(false);
        }

        public override void Show()
        {
            base.Show();
            screenCanvas.SetActive(true);
            SetEnableOperation(false);
            gameManager.PrepareGame();
        }

        private void Initialize()
        {
            GameState.Initialize();
            foreach (var item in initializableList)
            {
                item.GetComponent<IInitializable>()?.Initialize();
            }
        }
    }
}
