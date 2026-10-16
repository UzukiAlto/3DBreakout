using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Tutorial
{
    /// <summary>
    /// キーが押し込まれる様子をスプライト切り替えで示すアニメーション
    /// 登録したキーを順番に押すため、Space（1キー）にもWASD（4キー）にも使い回せる
    /// </summary>
    public class KeyPressVisual : TutorialVisualBase
    {
        [System.Serializable]
        public class KeyVisualData
        {
            [SerializeField] private Image keyImage;
            [SerializeField] private Sprite defaultSprite;
            [SerializeField] private Sprite pressedSprite;

            public Image KeyImage => keyImage;

            public void Initialize()
            {
                // インスペクターで通常時スプライトが明示指定されていない場合、現在のスプライトを自動採用するため
                if (defaultSprite == null && keyImage != null)
                {
                    defaultSprite = keyImage.sprite;
                }
            }

            public void SetPressed(bool isPressed)
            {
                if (keyImage == null)
                {
                    return;
                }

                Sprite targetSprite = isPressed ? (pressedSprite != null ? pressedSprite : defaultSprite) : defaultSprite;
                if (targetSprite != null)
                {
                    keyImage.sprite = targetSprite;
                }
            }
        }

        [Header("Keys Settings")]
        [Tooltip("押す順番に登録する（例: W, A, S, D または Space単体）")]
        [SerializeField] private List<KeyVisualData> keys = new List<KeyVisualData>();

        [Header("Motion Settings")]
        [Tooltip("押し込み時のスケール倍率（1.0でスケール変化なし）")]
        [SerializeField] private float pressedScale = 0.95f;
        [SerializeField] private float pressDuration = 0.05f;
        [SerializeField] private float holdDuration = 0.25f;
        [SerializeField] private float intervalBetweenKeys = 0.15f;
        [SerializeField] private float loopInterval = 0.5f;

        private readonly List<Vector3> initialScales = new List<Vector3>();
        private bool isInitialized = false;
        private Sequence loopSequence;

        private void Awake()
        {
            CacheInitialState();

            // エディタ上で配置・確認のためにアクティブのまま残されていても、開始時は非表示にするため
            gameObject.SetActive(false);
        }

        public override void Play()
        {
            Stop();

            if (!isInitialized || keys.Count == 0)
            {
                Debug.LogWarning("KeyPressVisual にキーが登録されていないため、アニメーションを再生できません。");
                return;
            }

            loopSequence = DOTween.Sequence();

            for (int i = 0; i < keys.Count; i++)
            {
                KeyVisualData keyData = keys[i];
                if (keyData == null || keyData.KeyImage == null)
                {
                    continue;
                }

                RectTransform keyTransform = keyData.KeyImage.rectTransform;
                Vector3 initialScale = initialScales[i];

                // スプライト切り替えと微小なスケール変化を連動させ、キートップが沈み込む演出を行う
                loopSequence
                    .AppendCallback(() => keyData.SetPressed(true))
                    .Append(keyTransform.DOScale(initialScale * pressedScale, pressDuration))
                    .AppendInterval(holdDuration)
                    .AppendCallback(() => keyData.SetPressed(false))
                    .Append(keyTransform.DOScale(initialScale, pressDuration))
                    .AppendInterval(intervalBetweenKeys);
            }

            loopSequence
                .AppendInterval(loopInterval)
                .SetLoops(-1)
                .SetUpdate(true)
                .SetLink(gameObject);
        }

        public override void Stop()
        {
            loopSequence?.Kill();
            loopSequence = null;

            // Awakeより前にOnDisable経由で呼ばれた場合に、未取得の初期値で上書きしないため
            if (!isInitialized)
            {
                return;
            }

            for (int i = 0; i < keys.Count; i++)
            {
                KeyVisualData keyData = keys[i];
                if (keyData == null || keyData.KeyImage == null)
                {
                    continue;
                }

                keyData.SetPressed(false);
                keyData.KeyImage.rectTransform.localScale = initialScales[i];
            }
        }

        private void CacheInitialState()
        {
            initialScales.Clear();

            foreach (KeyVisualData keyData in keys)
            {
                if (keyData == null || keyData.KeyImage == null)
                {
                    initialScales.Add(Vector3.one);
                    continue;
                }

                keyData.Initialize();
                initialScales.Add(keyData.KeyImage.rectTransform.localScale);
                keyData.SetPressed(false);
            }

            isInitialized = true;
        }
    }
}
