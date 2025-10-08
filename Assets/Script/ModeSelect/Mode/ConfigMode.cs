using UnityEngine;
using MainSystem;

namespace ModeSelect
{
    public class ConfigMode : MonoBehaviour, IModeSelectionTarget
    {
        [SerializeField] private GameObject configObjects;
        [SerializeField] private GameObject configText;
        [SerializeField] private ScreenBase modeSelectScreen;
        
        public void SwitchMode()
        {
            configObjects.SetActive(true);
            configText.SetActive(false);
            modeSelectScreen.DisableOperation();
            Debug.Log("ConfigMode");
        }
        public void ReturnToModeSelect()
        {
            configObjects.SetActive(false);
            configText.SetActive(true);
        }
    }
}