using UnityEngine;
using TMPro;
using MainSystem;
using System.Collections.Generic;

namespace ModeSelect
{
    /// <summary>
    /// 選択中テキストの色を変えるクラス
    /// </summary>
    public class SelectingTextColorChanger : MonoBehaviour
    {
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color defaultColor;
        [SerializeField] private List<TMP_Text> modeTextList = new List<TMP_Text>();
        TMP_Text previousText;
        // 選択中のテキストの色を変更
        public void ChangeToSelectedColor(GameObject selectedObject)
        {
            TMP_Text selectedText = selectedObject.transform.Find("Canvas/AdjustTextRotate/Text (TMP)").gameObject.GetComponent<TMP_Text>();
            if (previousText != null)
            {
                if (previousText != selectedText)
                {
                    AudioManager.PlaySE(AudioManager.SEType.Select);
                }
                previousText.color = defaultColor;
            }
            else
            {
                AudioManager.PlaySE(AudioManager.SEType.Select);
            }

            selectedText.color = selectedColor;
            previousText = selectedText;
        }

        // テキストの色をすべてデフォルトに戻す
        public void ResetTextColor()
        {
            foreach (var text in modeTextList)
            {
                text.color = defaultColor;
            }
            previousText = null;
        }
    }
}