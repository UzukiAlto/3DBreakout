using System.Runtime.InteropServices;
using UnityEngine;

namespace MainSystem
{
    /// <summary>
    /// WebGLビルド時とエディタ/スタンドアロン環境でポインタロック処理を統一的に扱うブリッジクラス
    /// </summary>
    public static class WebGLPointerLockBridge
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void WebGL_RequestPointerLock();

        [DllImport("__Internal")]
        private static extern void WebGL_ExitPointerLock();

        [DllImport("__Internal")]
        private static extern bool WebGL_IsPointerLocked();
#endif

        /// <summary>
        /// マウスカーソルをロックし、ドラッグ時の無限移動を可能にする
        /// </summary>
        public static void RequestLock()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            // WebGLではブラウザのPointer Lock APIを直接同期呼び出しして遅延や拒否を防ぐ
            WebGL_RequestPointerLock();
#else
            // エディタやデスクトップビルドではUnity標準のCursor APIを使用
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
#endif
        }

        /// <summary>
        /// マウスカーソルのロックを解除し、通常のカーソル表示に戻す
        /// </summary>
        public static void ExitLock()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            // WebGL環境でブラウザのPointer Lockを解除
            WebGL_ExitPointerLock();
#else
            // エディタやデスクトップビルドでカーソルを通常状態へ復帰
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
#endif
        }

        /// <summary>
        /// 現在ポインタがロックされているかどうかを取得
        /// </summary>
        public static bool IsLocked()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            return WebGL_IsPointerLocked();
#else
            return Cursor.lockState == CursorLockMode.Locked;
#endif
        }
    }
}
