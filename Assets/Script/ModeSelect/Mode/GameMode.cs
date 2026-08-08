using UnityEngine;
using MainSystem;

namespace ModeSelect
{
    public class GameMode : MonoBehaviour, IModeSelectionTarget
    {
        [SerializeField] private ScreenChanger screenChanger;
        [SerializeField] private GameObject gameScreenCubeObj;
        
        public void SwitchMode()
        {
            Debug.Log("GameMode");
            screenChanger.ChangeScreen(ScreenEnum.Game, gameScreenCubeObj);

        }
        public void ReturnToModeSelect()
        {
            // 呼び出さない
        }
    }
}