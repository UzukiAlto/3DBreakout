using System;
using UnityEngine;
using UnityEngine.UI;

namespace ModeSelect
{
    /// <summary>
    /// 汎用的な設定用スライダークラス。純粋なUI部品として機能します。
    /// </summary>
    public class ConfigSlider : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        
        public event Action<float> OnValueChanged;

        public void Initialize(float initialValue)
        {
            if (slider == null)
            {
                Debug.LogWarning("Slider is not attached to ConfigSlider on " + gameObject.name);
                return;
            }

            // 初期値をセット（この時点では余計なイベントを発火させないよう SetValueWithoutNotify を使用）
            slider.SetValueWithoutNotify(initialValue);
            
            // 値が変更されたらイベントを発火するようにリスナーを登録
            slider.onValueChanged.AddListener(val => OnValueChanged?.Invoke(val));
        }

        private void OnDestroy()
        {
            if (slider != null)
            {
                // オブジェクト破棄時にリスナーを解除
                slider.onValueChanged.RemoveAllListeners();
            }
        }
    }
}
