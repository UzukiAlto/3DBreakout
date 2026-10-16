using UnityEngine;
using MainSystem;

namespace Tutorial
{
    /// <summary>
    /// マウスドラッグによる視線移動を案内・検知するチュートリアルステップ
    /// </summary>
    public class CameraRotateTutorialStep : TutorialStepBase
    {
        [SerializeField] private GameObject playerCameraInputObject;
        [SerializeField] private float requiredDragAmount = 0.6f;

        private IPlayerCameraInput playerCameraInput;
        private float accumulatedDrag = 0f;
        private bool isCompleted = false;

        private void Awake()
        {
            if (playerCameraInputObject == null)
            {
                return;
            }

            playerCameraInput = playerCameraInputObject.GetComponent<IPlayerCameraInput>();
        }

        public override void EnterStep()
        {
            accumulatedDrag = 0f;
            isCompleted = false;

            if (playerCameraInput == null)
            {
                Debug.LogWarning("IPlayerCameraInput が未設定のため、視線移動ステップを開始できません。");
                return;
            }

            playerCameraInput.OnCameraMovingDelta += HandleCameraMovingDelta;
        }

        public override void ExitStep()
        {
            if (playerCameraInput == null)
            {
                return;
            }

            playerCameraInput.OnCameraMovingDelta -= HandleCameraMovingDelta;
        }

        private void HandleCameraMovingDelta(Vector2 delta)
        {
            // 既に完了条件を満たしている場合は二重発火を防ぐため処理しない
            if (isCompleted)
            {
                return;
            }

            // 単なるクリックの微小なブレでの誤判定を防ぎ、しっかり見渡したことを検知するため移動量を累積
            accumulatedDrag += delta.magnitude;

            if (accumulatedDrag < requiredDragAmount)
            {
                return;
            }

            isCompleted = true;
            Debug.Log($"[CameraRotateTutorialStep] 視線移動ステップ完了: 累積ドラッグ量 = {accumulatedDrag:F2} / 目標 = {requiredDragAmount:F2} (GameObject: {gameObject.name})");
            CompleteStep();
        }
    }
}
