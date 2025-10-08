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
        [SerializeField] private InputHandlerBase decisionInputer;
        [SerializeField] private PlayerRaycast playerRaycast;
        [SerializeField] private SelectingTextColorChanger selectingTextColorChanger;


        // Update is called once per frame
        void Update()
        {
            SelectMode();
        }

        private void SelectMode()
        {

            GameObject selectedObject = playerRaycast.GetRaycastHitGameObject();

            // 何も選択されていないときは色をリセットして終了
            if (selectedObject == null)
            {
                selectingTextColorChanger.ResetTextColor();
                return;
            }
            selectingTextColorChanger.ChangeToSelectedColor(selectedObject);

            if (decisionInputer.GetIsInputReceived())
            {
                var modeSelectTarget = selectedObject.GetComponent<IModeSelectionTarget>();
                if (modeSelectTarget != null)
                {
                    modeSelectTarget.SwitchMode();
                }
            }
            
        }
    }
}
