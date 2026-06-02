using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace MainSystem
{
    public class ScreenChanger : MonoBehaviour
    {
        [SerializeField] private ScreenBase modeSelectScreen;
        [SerializeField] private ScreenBase gameScreen;
        private ScreenBase previousScreen;
        private ScreenBase currentScreen;
        private float changeSecond = 1f;
        private Dictionary<ScreenEnum, ScreenBase> screenDict = new Dictionary<ScreenEnum, ScreenBase>();
        private void Start()
        {
            screenDict = new Dictionary<ScreenEnum, ScreenBase>(){
                {ScreenEnum.ModeSelect, modeSelectScreen},
                {ScreenEnum.Game, gameScreen},
            };

            // debug
            currentScreen = modeSelectScreen;
            currentScreen.Show();
        }

        // ただスクリーンを変更するだけのときはscreenEnumのみを渡す
        public void ChangeScreen(ScreenEnum screenEnum)
        {
            previousScreen = currentScreen;
            currentScreen = screenDict[screenEnum];

            previousScreen?.Hide();
            currentScreen.Show();
        }

        // スクリーン変更アニメーションを再生するときは変更後のscreenEnumと変更前のcubeObjを渡す
        public void ChangeScreen(ScreenEnum screenEnum, GameObject previousCubeObj)
        {
            if (currentScreen == screenDict[screenEnum]) return;
            if (previousCubeObj == null) return;

            previousScreen = currentScreen;
            currentScreen = screenDict[screenEnum];

            previousScreen.SetEnableOperation(false);
            currentScreen.Show();

            Debug.Log("MoveGamePos");
            Vector3 endPos = currentScreen.cameraObject.transform.position - previousCubeObj.transform.position;
            Vector3 startPos = previousScreen.cameraObject.transform.position - previousCubeObj.transform.position;

            Quaternion endRotate = currentScreen.cameraObject.transform.rotation;
            Quaternion startRotate = previousScreen.cameraObject.transform.rotation;
            float slerpPos = 0f;
            DOTween.To
            (
                () => slerpPos,
                x =>
                {
                    previousScreen.cameraObject.transform.position = Vector3.Slerp(startPos, endPos, x) + previousCubeObj.transform.position;
                    previousScreen.cameraObject.transform.rotation = Quaternion.Lerp(startRotate, endRotate, x);
                },
                1f,
                changeSecond
            )
            .OnComplete(() =>
            {
                Debug.Log("ChangeScreenComplete");
                previousScreen?.Hide();
                currentScreen.cameraObject.SetActive(true);
            })
            .SetEase(Ease.OutCubic);
        }

    }

    public enum ScreenEnum
    {
        ModeSelect,
        Game,
    }
}