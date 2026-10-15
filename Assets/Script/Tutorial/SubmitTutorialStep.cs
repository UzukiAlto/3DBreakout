using UnityEngine;
using MainSystem;

namespace Tutorial
{
    /// <summary>
    /// スペースキーによるモード決定入力を案内・検知するチュートリアルステップ
    /// </summary>
    public class SubmitTutorialStep : TutorialStepBase
    {
        [SerializeField] private GameObject systemInputObject;

        private ISystemInput systemInput;
        private bool isCompleted = false;

        private void Awake()
        {
            if (systemInputObject == null)
            {
                return;
            }

            systemInput = systemInputObject.GetComponent<ISystemInput>();
        }

        public override void EnterStep()
        {
            isCompleted = false;

            if (systemInput == null)
            {
                Debug.LogWarning("ISystemInput が未設定のため、モード決定ステップを開始できません。");
                return;
            }

            systemInput.OnSubmit += HandleSubmit;
        }

        public override void ExitStep()
        {
            if (systemInput == null)
            {
                return;
            }

            systemInput.OnSubmit -= HandleSubmit;
        }

        private void HandleSubmit(bool isPressed)
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
