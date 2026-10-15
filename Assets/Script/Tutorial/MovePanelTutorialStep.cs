using UnityEngine;
using MainSystem;

namespace Tutorial
{
    /// <summary>
    /// WASDキーによるパネル移動を案内・検知するチュートリアルステップ
    /// </summary>
    public class MovePanelTutorialStep : TutorialStepBase
    {
        [SerializeField] private GameObject playerMoveInputObject;
        [SerializeField] private float requiredInputDuration = 0.5f;

        private IPlayerMoveInput playerMoveInput;
        private float accumulatedDuration = 0f;
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
            accumulatedDuration = 0f;
            isCompleted = false;

            if (playerMoveInput == null)
            {
                Debug.LogWarning("IPlayerMoveInput が未設定のため、パネル移動ステップを開始できません。");
                return;
            }

            playerMoveInput.OnMoving += HandleMoving;
        }

        public override void ExitStep()
        {
            if (playerMoveInput == null)
            {
                return;
            }

            playerMoveInput.OnMoving -= HandleMoving;
        }

        private void HandleMoving(Vector2 moveDelta)
        {
            // 既に完了条件を満たしている場合や、入力が無い（ゼロベクトル）場合は処理しない
            if (isCompleted || moveDelta.sqrMagnitude < 0.01f)
            {
                return;
            }

            // 一瞬触れただけで指示が消えてしまうのを防ぎ、移動操作を体験したことを確かめるため入力時間を累積
            accumulatedDuration += Time.deltaTime;

            if (accumulatedDuration < requiredInputDuration)
            {
                return;
            }

            isCompleted = true;
            CompleteStep();
        }
    }
}
