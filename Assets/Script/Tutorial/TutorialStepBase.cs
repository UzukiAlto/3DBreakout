using System;
using UnityEngine;

namespace Tutorial
{
    /// <summary>
    /// 各チュートリアルステップの基底クラス
    /// 指示メッセージの管理や完了通知イベントを提供
    /// </summary>
    public abstract class TutorialStepBase : MonoBehaviour
    {
        [SerializeField, TextArea(2, 4)] private string guideMessage;

        public string GuideMessage
        {
            get
            {
                return guideMessage;
            }
        }

        public event Action OnStepCompleted;

        /// <summary>
        /// ステップの案内を開始し、達成条件の監視を開始
        /// </summary>
        public abstract void EnterStep();

        /// <summary>
        /// ステップの案内を終了し、監視の後始末をする
        /// </summary>
        public abstract void ExitStep();

        /// <summary>
        /// 達成条件を満たした際に呼び出し、シーケンスへ完了を通知
        /// </summary>
        protected void CompleteStep()
        {
            OnStepCompleted?.Invoke();
        }
    }
}
