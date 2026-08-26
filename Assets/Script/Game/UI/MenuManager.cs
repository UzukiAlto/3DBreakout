using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MainSystem;
using DG.Tweening;

namespace Game
{
    public class MenuManager : MonoBehaviour, IInitializable
    {
        [SerializeField] private ScreenChanger screenChanger;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject centerCubeObj;
        [SerializeField] private GameObject systemInputObject;
        [SerializeField] private Image pausePanelBackground;
        private ISystemInput systemInput;
        [SerializeField] private Image menuImage;
        [SerializeField] private Sprite startSprite;
        [SerializeField] private Sprite pauseSprite;
        [SerializeField] private MenuCube menuCube;
        private bool isPaused = false;
        private float pausePanelFadeTime = 0.3f; // フェードイン・フェードアウトの時間
        private float pausePanelAlpha = 0.85f; // フェードイン・フェードアウトの透明度
        private void Awake()
        {
            systemInput = systemInputObject.GetComponent<ISystemInput>();
        }
        private void OnEnable()
        {
            gameManager.OnGamePaused += Pause;
            gameManager.OnGameUnpaused += Unpause;
            systemInput.OnPause += OnPauseAction;
        }
        private void OnDisable()
        {
            gameManager.OnGamePaused -= Pause;
            gameManager.OnGameUnpaused -= Unpause;
            systemInput.OnPause -= OnPauseAction;
        }
        public void Initialize()
        {
            menuCube.PlayRotation();
            isPaused = false;
            pausePanel.SetActive(false);
            menuImage.sprite = pauseSprite;
        }

        // インスペクターのEvent Triggerで設定
        public void OnClick()
        {
            if (!isPaused)
            {
                gameManager.PauseGame();
            }
            else
            {
                gameManager.UnpauseGame();
            }
        }

        // Input SystemのPauseアクションに対応するメソッド
        private void OnPauseAction(bool isPressed)
        {
            if (!isPressed)
            {
                return;
            }

            if (!isPaused)
            {
                gameManager.PauseGame();
            }
            else
            {
                gameManager.UnpauseGame();
            }
        }

        private void Pause()
        {
            menuCube.PauseRotation();
            isPaused = true;

            pausePanelBackground.color = new Color(pausePanelBackground.color.r, pausePanelBackground.color.g, pausePanelBackground.color.b, 0f);
            pausePanel.SetActive(true);
            pausePanelBackground.DOFade(pausePanelAlpha, pausePanelFadeTime).SetEase(Ease.InOutQuad);

            menuImage.sprite = startSprite;
        }
        private void Unpause(bool wasPlayingBeforePause)
        {
            menuCube.PlayRotation();
            isPaused = false;
            
            pausePanelBackground.DOFade(0f, pausePanelFadeTime)
                                .SetEase(Ease.InOutQuad)
                                .OnComplete(() => pausePanel.SetActive(false));

            menuImage.sprite = pauseSprite;
        }

        public void ReturnToModeSelect()
        {
            
            gameManager.UnpauseGame();
            screenChanger.ChangeScreen(ScreenEnum.ModeSelect, centerCubeObj);
        } 
    }
}