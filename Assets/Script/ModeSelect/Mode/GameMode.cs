using UnityEngine;
using MainSystem;

namespace ModeSelect
{
    public class GameMode : MonoBehaviour, IModeSelectionTarget
    {
        [SerializeField] private ScreenChanger screenChanger;
        
        public void SwitchMode()
        {
            Debug.Log("GameMode");
        }
        public void ReturnToModeSelect()
        {
            // 呼び出さない
        }
    }
}