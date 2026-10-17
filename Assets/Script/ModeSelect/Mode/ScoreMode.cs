using UnityEngine;
using MainSystem;

namespace ModeSelect
{
    public class ScoreMode : MonoBehaviour, IModeSelectionTarget
    {
        [SerializeField] private GameObject scoreObjects;
        [SerializeField] private GameObject scoreText;
        [SerializeField] private ScreenBase modeSelectScreen;
        [SerializeField] private PlayerCameraController playerCameraController;
        [SerializeField] private Transform scoreCameraTransform;
        [SerializeField] private ScoreView scoreView;
        [SerializeField] private GameObject systemInputObject;

        private ISystemInput systemInput;

        private void Awake()
        {
            if (systemInputObject != null)
            {
                systemInput = systemInputObject.GetComponent<ISystemInput>();
            }
        }

        private void OnEnable()
        {
            if (systemInput != null)
            {
                systemInput.OnPause += OnPauseInput;
            }
        }

        private void OnDisable()
        {
            if (systemInput != null)
            {
                systemInput.OnPause -= OnPauseInput;
            }
        }

        public void SwitchMode()
        {

            Debug.Log("SwitchMode ScoreMode");
            scoreObjects.SetActive(true);
            if (scoreText != null)
            {
                scoreText.SetActive(false);
            }

            modeSelectScreen.SetEnableOperation(false);
            playerCameraController.MoveToTargetPosition(scoreCameraTransform);

            if (scoreView != null)
            {
                scoreView.UpdateDisplay();
            }

            Debug.Log("ScoreMode");
        }

        // ReturnButtonのonClick、またはEsc/決定キー入力で呼ばれる
        public void ReturnToModeSelect()
        {
            // スコア画面が開かれていない場合は二重呼び出しを防ぐため
            if (scoreObjects != null && !scoreObjects.activeSelf)
            {
                return;
            }

            if (scoreObjects != null)
            {
                scoreObjects.SetActive(false);
            }

            if (scoreText != null)
            {
                scoreText.SetActive(true);
            }

            modeSelectScreen.SetEnableOperation(true);
            playerCameraController.MoveToPreviousPosition();
        }

        private void OnPauseInput(bool isPressed)
        {
            // ポーズキー（Escキー）が押されたときに元の画面へ戻るため
            if (!isPressed)
            {
                return;
            }

            // スコア画面が開いていないときの入力は無視するため
            if (scoreObjects == null || !scoreObjects.activeSelf)
            {
                return;
            }

            ReturnToModeSelect();
        }

    }
}
