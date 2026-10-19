using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using MainSystem;
using DG.Tweening;

namespace Game
{
    /// <summary>
    /// ゲーム画面におけるプレイヤーのカメラ制御クラス
    /// ポーズUI判定とゲーム進行時のカメラ位置復帰を管理
    /// </summary>
    public class PlayerCameraController : PlayerRotationControllerBase
    {
        [SerializeField] private GameManager gameManager;
        private Vector3 defaultCameraPosition;
        private Quaternion defaultCameraRotation;
        private float changeSecond = 0.4f;

        protected override void Awake()
        {
            base.Awake();
            defaultCameraPosition = playerCameraObj.transform.position;
            defaultCameraRotation = playerCameraObj.transform.rotation;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            gameManager.OnGameReady += MoveCameraToDefaultPosition;
            gameManager.OnGameFailed += MoveCameraToDefaultPosition;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (gameManager != null)
            {
                gameManager.OnGameReady -= MoveCameraToDefaultPosition;
                gameManager.OnGameFailed -= MoveCameraToDefaultPosition;
            }
        }

        protected override void OnCameraMoveStart(Vector2 pointerPos)
        {
            // ポーズボタン等の操作UIをクリックした際は、カーソルをロックせずUIクリックを成立させるため
            if (IsPointerOverPauseUI())
            {
                return;
            }

            base.OnCameraMoveStart(pointerPos);
        }

        private bool IsPointerOverPauseUI()
        {
            if (EventSystem.current == null)
            {
                return false;
            }

            Vector2 screenPos = Vector2.zero;
            if (Mouse.current != null)
            {
                screenPos = Mouse.current.position.ReadValue();
            }
            else
            {
                screenPos = (Vector2)Input.mousePosition;
            }

            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = screenPos
            };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject == null)
                {
                    continue;
                }

                // ポーズボタンのコンポーネント（MenuCubeやEventTrigger、Button等）を持つUIを検知
                if (result.gameObject.GetComponentInParent<MenuCube>() != null ||
                    result.gameObject.GetComponentInParent<EventTrigger>() != null ||
                    result.gameObject.GetComponentInParent<Button>() != null)
                {
                    return true;
                }
            }

            return false;
        }

        // ゲーム失敗時や準備完了時にカメラを初期位置へ補間復帰
        private void MoveCameraToDefaultPosition()
        {
            Vector3 endPos = defaultCameraPosition - rotateCenterObj.transform.position;
            Vector3 startPos = playerCameraObj.transform.position - rotateCenterObj.transform.position;

            Quaternion endRotate = defaultCameraRotation;
            Quaternion startRotate = playerCameraObj.transform.rotation;
            float slerpPos = 0f;
            DOTween.To
            (
                () => slerpPos,
                x =>
                {
                    playerCameraObj.transform.position = Vector3.Slerp(startPos, endPos, x) + rotateCenterObj.transform.position;
                    playerCameraObj.transform.rotation = Quaternion.Lerp(startRotate, endRotate, x);
                },
                1f,
                changeSecond
            )
            .SetEase(Ease.OutCubic);
        }
    }
}