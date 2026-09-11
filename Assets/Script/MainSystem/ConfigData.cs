using UnityEngine;
using System;

namespace MainSystem
{
    /// <summary>
    /// プレイヤーの設定を保持するクラス
    /// </summary>
    public class ConfigData : MonoBehaviour
    {
        // debug
        private PlayerConfig _playerConfig = new PlayerConfig(100f, 0.5f, 0.5f);

        public event Action<float> OnSensitivityChanged;
        public event Action<float> OnBgmVolumeChanged;
        public event Action<float> OnSeVolumeChanged;

        public PlayerConfig GetPlayerConfig()
        {
            return _playerConfig;
        }

        public void UpdateSensitivity(float value)
        {
            _playerConfig.sensitivity = value;
            OnSensitivityChanged?.Invoke(value);
        }

        public void UpdateBgmVolume(float value)
        {
            _playerConfig.bgmVolume = value;
            OnBgmVolumeChanged?.Invoke(value);
        }

        public void UpdateSeVolume(float value)
        {
            _playerConfig.seVolume = value;
            OnSeVolumeChanged?.Invoke(value);
        }
    }
    public class PlayerConfig
    {
        public float sensitivity;
        public float bgmVolume;
        public float seVolume;

        public PlayerConfig(float sensitivity, float bgmVolume, float seVolume)
        {
            this.sensitivity = sensitivity;
            this.bgmVolume = bgmVolume;
            this.seVolume = seVolume;
        }
    }
}