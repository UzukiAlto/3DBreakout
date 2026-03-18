using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainSystem;

namespace ModeSelect
{
    /// <summary>
    /// モード変更の指示を出すクラス
    /// </summary>
    public class ModeSelector : MonoBehaviour
    {
        [SerializeField] private IModeSelectionTarget configMode;
        [SerializeField] private IModeSelectionTarget gameMode;
        [SerializeField] private PlayerRaycast playerRaycast;
        [SerializeField] private SelectingTextColorChanger selectingTextColorChanger;

        [SerializeField] private GameObject playerMoveInputObject; 
        private IPlayerMoveInput playerMoveInput;

        private GameObject selectedObject;
        private void Awake()
        {
            playerMoveInput = playerMoveInputObject.GetComponent<IPlayerMoveInput>();
        }

        // プレイヤーの決定入力にSelectModeを登録
        private void OnEnable()
        {
            playerMoveInput.OnSubmit += SelectMode;
        }
        private void OnDisable()
        {
            playerMoveInput.OnSubmit -= SelectMode;
        }

        void Update()
        {
            selectedObject = playerRaycast.GetRaycastHitGameObject();
            ChangeModeTextColor();
        }

        private void ChangeModeTextColor()
        {
            // 何も選択されていないときは色をリセットして終了
            if (selectedObject == null)
            {
                selectingTextColorChanger.ResetTextColor();
                return;
            }
            selectingTextColorChanger.ChangeToSelectedColor(selectedObject);
        }

        private void SelectMode(bool isPressed)
        {
            var modeSelectTarget = selectedObject.GetComponent<IModeSelectionTarget>();
            if (modeSelectTarget != null)
            {
                modeSelectTarget.SwitchMode();
            }
            
        }
    }
}
