using UnityEngine;
using System;

namespace MainSystem
{
    /// <summary>
    /// プレイヤーの設定を保持・永続化するクラス
    /// </summary>
    public class ConfigData : MonoBehaviour
    {
        private const string SensitivityKey = "Config_Sensitivity";
        private const string BgmVolumeKey = "Config_BgmVolume";
        private const string SeVolumeKey = "Config_SeVolume";

        private const float DefaultSensitivity = 0.5f;
        private const float DefaultBgmVolume = 0.5f;
        private const float DefaultSeVolume = 0.5f;

        private PlayerConfig _playerConfig = new PlayerConfig(DefaultSensitivity, DefaultBgmVolume, DefaultSeVolume);

        private event Action<float> OnSensitivityChanged;
        private event Action<float> OnBgmVolumeChanged;
        private event Action<float> OnSeVolumeChanged;

        private void Awake()
        {
            // 起動時に保存された設定値を復元するため
            Load();
        }

        private void OnApplicationQuit()
        {
            // アプリケーション終了時に未保存の設定が失われるのを防ぐため
            Save();
        }

        /// <summary>
        /// PlayerPrefsから設定を読み込みます
        /// </summary>
        public void Load()
        {
            float sensitivity = PlayerPrefs.GetFloat(SensitivityKey, DefaultSensitivity);
            float bgmVolume = PlayerPrefs.GetFloat(BgmVolumeKey, DefaultBgmVolume);
            float seVolume = PlayerPrefs.GetFloat(SeVolumeKey, DefaultSeVolume);

            _playerConfig = new PlayerConfig(sensitivity, bgmVolume, seVolume);
        }

        /// <summary>
        /// 現在の設定をPlayerPrefsに保存します
        /// </summary>
        public void Save()
        {
            if (_playerConfig == null)
            {
                return;
            }

            PlayerPrefs.SetFloat(SensitivityKey, _playerConfig.Sensitivity);
            PlayerPrefs.SetFloat(BgmVolumeKey, _playerConfig.BgmVolume);
            PlayerPrefs.SetFloat(SeVolumeKey, _playerConfig.SeVolume);
            PlayerPrefs.Save();
        }

        public IReadOnlyPlayerConfig GetPlayerConfig()
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

        // --- 購読＆初期値通知メソッド ---

        public void SubscribeSensitivity(Action<float> callback)
        {
            Debug.Log("SubscribeSensitivity: " + callback.Method.Name);
            OnSensitivityChanged += callback;
            callback?.Invoke(_playerConfig.Sensitivity);
        }

        public void UnsubscribeSensitivity(Action<float> callback)
        {
            OnSensitivityChanged -= callback;
        }

        public void SubscribeBgmVolume(Action<float> callback)
        {
            OnBgmVolumeChanged += callback;
            callback?.Invoke(_playerConfig.BgmVolume);
        }

        public void UnsubscribeBgmVolume(Action<float> callback)
        {
            OnBgmVolumeChanged -= callback;
        }

        public void SubscribeSeVolume(Action<float> callback)
        {
            OnSeVolumeChanged += callback;
            callback?.Invoke(_playerConfig.SeVolume);
        }

        public void UnsubscribeSeVolume(Action<float> callback)
        {
            OnSeVolumeChanged -= callback;
        }
    }

    public interface IReadOnlyPlayerConfig
    {
        float Sensitivity { get; }
        float BgmVolume { get; }
        float SeVolume { get; }
    }

    public class PlayerConfig : IReadOnlyPlayerConfig
    {
        // 内部で値を保持・更新するためのプロパティ（0~1に制限）
        private float _sensitivity;
        public float sensitivity
        {
            get => _sensitivity;
            set => _sensitivity = Mathf.Clamp01(value);
        }

        private float _bgmVolume;
        public float bgmVolume
        {
            get => _bgmVolume;
            set => _bgmVolume = Mathf.Clamp01(value);
        }

        private float _seVolume;
        public float seVolume
        {
            get => _seVolume;
            set => _seVolume = Mathf.Clamp01(value);
        }

        // IReadOnlyPlayerConfig インターフェース公開用のプロパティ
        public float Sensitivity => sensitivity;
        public float BgmVolume => bgmVolume;
        public float SeVolume => seVolume;

        public PlayerConfig(float sensitivity, float bgmVolume, float seVolume)
        {
            this.sensitivity = sensitivity;
            this.bgmVolume = bgmVolume;
            this.seVolume = seVolume;
        }
    }
}