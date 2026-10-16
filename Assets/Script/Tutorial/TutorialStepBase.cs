using System;
using UnityEngine;

namespace Tutorial
{
    /// <summary>
    /// 各チュートリアルステップの基底クラス
    /// 表示する操作アニメーションの参照や完了通知イベントを提供
    /// </summary>
    public abstract class TutorialStepBase : MonoBehaviour
    {
        [SerializeField] private TutorialVisualBase visual;

        public TutorialVisualBase Visual
        {
            get
            {
                return visual;
            }
        }

        /// <summary>
        /// ステップ開始時に即座にアニメーションを表示するかどうか
        /// 特定の条件（例: 新しいパネルを捉えた時など）まで遅延させたい場合はfalseにします
        /// </summary>
        public virtual bool AutoShowVisualOnEnter
        {
            get
            {
                return true;
            }
        }

        public event Action OnStepCompleted;
        public event Action<TutorialVisualBase> OnRequestShowVisual;

        /// <summary>
        /// ステップの案内を開始し、達成条件の監視を開始
        /// </summary>
        public abstract void EnterStep();

        /// <summary>
        /// ステップの案内を終了し、監視の後始末をする
        /// </summary>
        public abstract void ExitStep();

        /// <summary>
        /// 任意のタイミングでアニメーションの表示をシーケンスへ要求します
        /// </summary>
        protected void RequestShowVisual()
        {
            OnRequestShowVisual?.Invoke(visual);
        }

        /// <summary>
        /// 達成条件を満たした際に呼び出し、シーケンスへ完了を通知
        /// </summary>
        protected void CompleteStep()
        {
            OnStepCompleted?.Invoke();
        }
    }
}
