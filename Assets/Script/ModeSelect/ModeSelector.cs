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

        [SerializeField] private GameObject systemInputObject; 
        private ISystemInput systemInput;

        private GameObject selectedObject;
        private void Awake()
        {
            systemInput = systemInputObject.GetComponent<ISystemInput>();  
        }

        // プレイヤーの決定入力にSelectModeを登録
        private void OnEnable()
        {
            systemInput.OnSubmit += SelectMode;
        }
        private void OnDisable()
        {
            systemInput.OnSubmit -= SelectMode;
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
            if (!isPressed) return; // ボタンが押されたときのみ反応
            if (selectedObject == null) return; // 何も選択されていないときは終了
            
            var modeSelectTarget = selectedObject.GetComponent<IModeSelectionTarget>();
            if (modeSelectTarget != null)
            {
                modeSelectTarget.SwitchMode();
            }
            
        }
    }
}
