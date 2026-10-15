using UnityEngine;
using MainSystem;

namespace Tutorial
{
    /// <summary>
    /// スペースキーによる操作面変更入力を案内・検知するチュートリアルステップ
    /// </summary>
    public class ChangePanelTutorialStep : TutorialStepBase
    {
        [SerializeField] private GameObject playerMoveInputObject;

        private IPlayerMoveInput playerMoveInput;
        private bool isCompleted = false;

        private void Awake()
        {
            if (playerMoveInputObject == null)
            {
                return;
            }

            playerMoveInput = playerMoveInputObject.GetComponent<IPlayerMoveInput>();
        }

        public override void EnterStep()
        {
            isCompleted = false;

            if (playerMoveInput == null)
            {
                Debug.LogWarning("IPlayerMoveInput が未設定のため、操作面変更ステップを開始できません。");
                return;
            }

            playerMoveInput.OnChangePanel += HandleChangePanel;
        }

        public override void ExitStep()
        {
            if (playerMoveInput == null)
            {
                return;
            }

            playerMoveInput.OnChangePanel -= HandleChangePanel;
        }

        private void HandleChangePanel(bool isPressed)
        {
            // ボタンが離された時のイベントや完了後の再入を無視するため
            if (!isPressed || isCompleted)
            {
                return;
            }

            isCompleted = true;
            CompleteStep();
        }
    }
}
