using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace MainSystem
{
    public class ScreenChanger : MonoBehaviour
    {
        [SerializeField] private GameObject screenChangeCamera;
        [SerializeField] private ScreenBase modeSelectScreen;
        [SerializeField] private ScreenBase gameScreen;
        private ScreenBase previousScreen;
        // CurrentScreen.currentを短く書くためにラッパープロパティを作成
        private ScreenBase currentScreen {
            get => CurrentScreen.current;
            set => CurrentScreen.current = value;
        }
        private float changeSecond = 1f;
        private Dictionary<ScreenEnum, ScreenBase> screenDict = new Dictionary<ScreenEnum, ScreenBase>();
        private void Awake()
        {
            screenChangeCamera.SetActive(false);
        }
        private void Start()
        {
            screenDict = new Dictionary<ScreenEnum, ScreenBase>(){
                {ScreenEnum.ModeSelect, modeSelectScreen},
                {ScreenEnum.Game, gameScreen},
            };

            foreach (ScreenBase screen in screenDict.Values)
            {
                screen.Hide();
            }

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
            if (currentScreen == screenDict[screenEnum])
            {
                Debug.Log("すでに " + screenEnum + "です");
                return;
            }
            if (previousCubeObj == null)
            {
                Debug.LogError("previousCubeObjがnullです");
                return;
            }

            previousScreen = currentScreen;
            currentScreen = screenDict[screenEnum];

            previousScreen.SetEnableOperation(false);
            Debug.Log("ChangeScreen Start: " + previousScreen.name + " -> " + currentScreen.name);
            
            currentScreen.cameraObject.SetActive(false);
            currentScreen.Show();

            Transform currentCameraTransform = currentScreen.cameraObject.transform;
            Transform previousCameraTransform = previousScreen.cameraObject.transform;

            // previousCubeObjを中心にSlerpで補間するため、previousCubeObjの座標を引く
            Vector3 endPos = currentCameraTransform.position - previousCubeObj.transform.position;
            Vector3 startPos = previousCameraTransform.position - previousCubeObj.transform.position;

            Quaternion endRotate = currentCameraTransform.rotation;
            Quaternion startRotate = previousCameraTransform.rotation;

            // 画面遷移時にはUI等が追従しないように専用のカメラを動かす
            screenChangeCamera.transform.SetPositionAndRotation(previousCameraTransform.position, previousCameraTransform.rotation);
            previousScreen.cameraObject.SetActive(false);
            screenChangeCamera.SetActive(true);

            float slerpPos = 0f;
            DOTween.To
            (
                () => slerpPos,
                x =>
                {
                    screenChangeCamera.transform.position = Vector3.Slerp(startPos, endPos, x) + previousCubeObj.transform.position;
                    screenChangeCamera.transform.rotation = Quaternion.Slerp(startRotate, endRotate, x);
                },
                1f,
                changeSecond
            )
            .OnComplete(() =>
            {
                Debug.Log("ChangeScreenComplete");
                previousScreen?.Hide();
                screenChangeCamera.SetActive(false);
                currentScreen.cameraObject.SetActive(true);
            })
            .SetEase(Ease.OutCubic);
        }

    }

    /// <summary>
    /// ChangeScreen()の引数として次のスクリーンを指定するenum
    /// </summary>
    public enum ScreenEnum
    {
        ModeSelect,
        Game,
    }
}