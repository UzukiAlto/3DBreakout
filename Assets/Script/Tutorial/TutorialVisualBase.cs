using UnityEngine;

namespace Tutorial
{
    /// <summary>
    /// チュートリアルで表示する操作説明アニメーションの基底クラス
    /// 達成判定（Step）と見せ方（Visual）を分離し、見た目だけを差し替えられるようにする
    /// </summary>
    /// <remarks>
    /// ルートのTransformはTutorialUIが達成演出（拡大）に使用するため、
    /// 派生クラスのアニメーションは子オブジェクトに対して行うこと
    /// </remarks>
    public abstract class TutorialVisualBase : MonoBehaviour
    {
        /// <summary>
        /// ループアニメーションを開始する
        /// </summary>
        public abstract void Play();

        /// <summary>
        /// アニメーションを停止し、初期状態に戻す
        /// Playより前に呼ばれても安全であること
        /// </summary>
        public abstract void Stop();

        protected virtual void OnDisable()
        {
            // 外部から直接非アクティブにされた場合でも、ループTweenが残り続けないようにするため
            Stop();
        }
    }
}
