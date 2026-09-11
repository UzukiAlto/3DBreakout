using System.Collections.Generic;
using UnityEngine;

namespace MainSystem
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        public enum BGMType
        {
            Main
        }

        public enum SEType
        {
            Ball,
            Fail,
            Next,
            Transition,
            Select
        }

        [System.Serializable]
        public struct BGMData
        {
            public BGMType type;
            public AudioClip clip;
        }

        [System.Serializable]
        public struct SEData
        {
            public SEType type;
            public AudioClip clip;
        }

        [SerializeField] private AudioSource bgmAudioSource;
        [SerializeField] private AudioSource seAudioSource;

        [SerializeField] private List<BGMData> bgmDataList = new List<BGMData>();
        [SerializeField] private List<SEData> seDataList = new List<SEData>();

        private Dictionary<BGMType, AudioClip> bgmDictionary = new Dictionary<BGMType, AudioClip>();
        private Dictionary<SEType, AudioClip> seDictionary = new Dictionary<SEType, AudioClip>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeDictionaries();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitializeDictionaries()
        {
            foreach (var data in bgmDataList)
            {
                if (!bgmDictionary.ContainsKey(data.type))
                {
                    bgmDictionary.Add(data.type, data.clip);
                }
            }

            foreach (var data in seDataList)
            {
                if (!seDictionary.ContainsKey(data.type))
                {
                    seDictionary.Add(data.type, data.clip);
                }
            }
        }

        public void PlayBGM(BGMType type)
        {
            if (bgmAudioSource == null) return;

            if (bgmDictionary.TryGetValue(type, out AudioClip clip))
            {
                if (bgmAudioSource.clip == clip && bgmAudioSource.isPlaying)
                {
                    return; // 既に同じBGMが再生中の場合は何もしない
                }

                bgmAudioSource.clip = clip;
                bgmAudioSource.loop = true;
                bgmAudioSource.Play();
            }
            else
            {
                Debug.LogWarning($"BGM {type} がAudioManagerに登録されていません。");
            }
        }

        public void StopBGM()
        {
            if (bgmAudioSource != null && bgmAudioSource.isPlaying)
            {
                bgmAudioSource.Stop();
            }
        }

        public void PlaySE(SEType type)
        {
            if (seAudioSource == null) return;

            if (seDictionary.TryGetValue(type, out AudioClip clip))
            {
                seAudioSource.PlayOneShot(clip);
            }
            else
            {
                Debug.LogWarning($"SE {type} がAudioManagerに登録されていません。");
            }
        }
    }
}
