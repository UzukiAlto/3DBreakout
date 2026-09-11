using UnityEngine;
using MainSystem;

namespace ModeSelect
{
    /// <summary>
    /// プレイヤーの設定を管理し、UIとデータを紐づけるクラス
    /// </summary>
    public class ConfigManager : MonoBehaviour
    {
        [SerializeField] private ConfigData configData;

        [Header("Sliders")]
        [SerializeField] private ConfigSlider bgmSlider;
        [SerializeField] private ConfigSlider seSlider;
        [SerializeField] private ConfigSlider sensitivitySlider;

        private void Start()
        {
            if (configData == null)
            {
                Debug.LogWarning("ConfigData is not assigned in ConfigManager.");
                return;
            }

            PlayerConfig config = configData.GetPlayerConfig();

            // BGMの初期化と購読
            if (bgmSlider != null)
            {
                bgmSlider.Initialize(config.bgmVolume);
                bgmSlider.OnValueChanged += (val) => configData.UpdateBgmVolume(val);
            }

            // SEの初期化と購読
            if (seSlider != null)
            {
                seSlider.Initialize(config.seVolume);
                seSlider.OnValueChanged += (val) => configData.UpdateSeVolume(val);
            }

            // 感度の初期化と購読
            if (sensitivitySlider != null)
            {
                sensitivitySlider.Initialize(config.sensitivity);
                sensitivitySlider.OnValueChanged += (val) => configData.UpdateSensitivity(val);
            }
        }
    }
}