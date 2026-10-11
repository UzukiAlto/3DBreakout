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

        // 初期化後に購読した側が、通知を待たずに現在値を取得できるように公開する
        public float Value => slider != null ? slider.value : 0f;

        public void Initialize(float initialValue)
        {
            if (slider == null)
            {
                Debug.LogWarning("Slider is not attached to ConfigSlider on " + gameObject.name);
                return;
            }

            // slider.valueへの代入はInspector上の値と同じだと通知されないため、通知せずに値だけ反映する
            slider.SetValueWithoutNotify(initialValue);

            // 値が変更されたらイベントを発火するようにリスナーを登録
            slider.onValueChanged.AddListener(val => OnValueChanged?.Invoke(val));

            // Inspector上の値に左右されず、既に購読している側へ必ず初期値を届けるため明示的に通知する
            OnValueChanged?.Invoke(initialValue);
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
