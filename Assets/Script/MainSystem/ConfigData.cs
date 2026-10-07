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
        private PlayerConfig _playerConfig = new PlayerConfig(0.5f, 0.5f, 0.5f);

        private event Action<float> OnSensitivityChanged;
        private event Action<float> OnBgmVolumeChanged;
        private event Action<float> OnSeVolumeChanged;

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