using UnityEngine;

namespace MainSystem
{
    /// <summary>
    /// プレイヤーの設定を保持するクラス
    /// </summary>
    public class ConfigData : MonoBehaviour
    {
        // debug
        private PlayerConfig _playerConfig = new PlayerConfig(100f, 0.5f, 0.5f);

        public PlayerConfig GetPlayerConfig()
        {
            return _playerConfig;
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