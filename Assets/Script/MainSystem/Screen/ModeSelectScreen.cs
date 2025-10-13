using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainSystem
{
    /// <summary>
    /// モード選択画面の管理を行うクラス
    /// </summary>
    public class ModeSelectScreen : ScreenBase
    {
        public override void Hide()
        {
            screenCanvas.SetActive(false);
            SetEnableOperation(false);
        }

        public override void Show()
        {
            screenCanvas.SetActive(true);
            SetEnableOperation(true);
        }
    }
}