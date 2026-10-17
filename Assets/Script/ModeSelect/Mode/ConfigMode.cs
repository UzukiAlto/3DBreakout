using UnityEngine;
using MainSystem;

namespace ModeSelect
{
    public class ConfigMode : MonoBehaviour, IModeSelectionTarget
    {
        [SerializeField] private ConfigData configData;
        [SerializeField] private GameObject configObjects;
        [SerializeField] private GameObject configText;
        [SerializeField] private ScreenBase modeSelectScreen;
        [SerializeField] private PlayerCameraController playerCameraController;
        [SerializeField] private Transform configCameraTransform;

        public void SwitchMode()
        {
            configObjects.SetActive(true);
            configText.SetActive(false);
            modeSelectScreen.SetEnableOperation(false);
            playerCameraController.MoveToTargetPosition(configCameraTransform);
            Debug.Log("ConfigMode");
        }
        // ReturnButtonのonclickで呼ばれる
        public void ReturnToModeSelect()
        {
            // 設定画面を抜ける際に変更内容を永続化保存するため
            if (configData != null)
            {
                configData.Save();
            }

            configObjects.SetActive(false);
            configText.SetActive(true);
            modeSelectScreen.SetEnableOperation(true);
            playerCameraController.MoveToPreviousPosition();
        }

    }
}