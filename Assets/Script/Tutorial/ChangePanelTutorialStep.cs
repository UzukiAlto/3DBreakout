using UnityEngine;
using MainSystem;
using Game;

namespace Tutorial
{
    /// <summary>
    /// スペースキーによる操作面変更入力を案内・検知するチュートリアルステップ
    /// 別のパネルを初めて視線で捉えたタイミングでスペースキーのアニメーションを表示します
    /// </summary>
    public class ChangePanelTutorialStep : TutorialStepBase
    {
        [SerializeField] private GameObject playerMoveInputObject;
        [SerializeField] private PanelObserver panelObserver;

        private IPlayerMoveInput playerMoveInput;
        private bool isCompleted = false;
        private bool hasShownVisual = false;

        // 別のパネルを捉えるまではスペースキーのアニメーションを非表示にして待機するため
        public override bool AutoShowVisualOnEnter
        {
            get
            {
                return false;
            }
        }

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
            hasShownVisual = false;

            if (playerMoveInput != null)
            {
                playerMoveInput.OnChangePanel += HandleChangePanel;
            }

            if (panelObserver == null)
            {
                Debug.LogWarning("PanelObserver が未設定のため、パネル選択検知を行わずに即時表示します。");
                RequestShowVisual();
                return;
            }

            panelObserver.OnSelectedPanelChanged += HandleSelectedPanelChanged;

            // ステップ開始時点ですでに別のパネルを視線で捉えている場合は即座に表示
            if (IsDifferentPanelSelected(panelObserver.currentSelectedPanel))
            {
                hasShownVisual = true;
                RequestShowVisual();
            }
        }

        public override void ExitStep()
        {
            if (playerMoveInput != null)
            {
                playerMoveInput.OnChangePanel -= HandleChangePanel;
            }

            if (panelObserver != null)
            {
                panelObserver.OnSelectedPanelChanged -= HandleSelectedPanelChanged;
            }
        }

        private void HandleSelectedPanelChanged(GameObject selectedPanel)
        {
            // 既に表示済みの場合やステップ完了後は重複して表示要求しないため
            if (hasShownVisual || isCompleted)
            {
                return;
            }

            // 現在操作中の面とは異なる新しいパネルに視線を向けた時に初めてスペースキーを表示
            if (!IsDifferentPanelSelected(selectedPanel))
            {
                return;
            }

            hasShownVisual = true;
            RequestShowVisual();
        }

        private bool IsDifferentPanelSelected(GameObject selectedPanel)
        {
            if (selectedPanel == null || panelObserver == null)
            {
                return false;
            }

            // 現在操作中のパネルとは異なるパネルが視線に入っているか判定
            return selectedPanel != panelObserver.currentOperatingPanel;
        }

        private void HandleChangePanel(bool isPressed)
        {
            // ボタンが離された時のイベントや完了後の再入を無視するため
            if (!isPressed || isCompleted)
            {
                return;
            }

            // アニメーション表示前（まだ別のパネルを選択していない状態）の誤爆を防止するため
            if (!hasShownVisual)
            {
                return;
            }

            isCompleted = true;
            CompleteStep();
        }
    }
}
