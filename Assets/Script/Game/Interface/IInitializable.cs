using UnityEngine;

namespace Game
{
    /// <summary>
    /// GameScreenから呼び出される初期化処理をかくインターフェース
    /// </summary>
    public interface IInitializable
    {
        void Initialize();
    }
}