using UnityEngine;

namespace ModeSelect
{
    public interface IModeSelectionTarget
    {
        public void SwitchMode();
        public void ReturnToModeSelect();
    }
}