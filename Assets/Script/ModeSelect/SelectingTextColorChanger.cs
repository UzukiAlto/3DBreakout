using UnityEngine;
using TMPro;
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
                previousText.color = defaultColor;
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