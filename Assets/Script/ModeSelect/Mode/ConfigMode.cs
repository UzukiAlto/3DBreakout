using UnityEngine;
using MainSystem;

namespace ModeSelect
{
    public class ConfigMode : MonoBehaviour, IModeSelectionTarget
    {
        [SerializeField] private GameObject configObjects;
        [SerializeField] private GameObject configText;
        [SerializeField] private ScreenBase modeSelectScreen;
        [SerializeField] private PlayerCameraController playerCameraController;

        public void SwitchMode()
        {
            configObjects.SetActive(true);
            configText.SetActive(false);
            modeSelectScreen.SetEnableOperation(false);
            playerCameraController.MoveToConfigPosition();
            Debug.Log("ConfigMode");
        }
        // ReturnButtonのonclickで呼ばれる
        public void ReturnToModeSelect()
        {
            configObjects.SetActive(false);
            configText.SetActive(true);
            modeSelectScreen.SetEnableOperation(true);
            playerCameraController.MoveToPreviousPosition();
        }

    }
}