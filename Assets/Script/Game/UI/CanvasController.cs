using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CanvasController : MonoBehaviour
    {
        [SerializeField] private GameObject mainCamera;
        private float distance = 10f;

        void Update()
        {
            transform.position = mainCamera.transform.position + mainCamera.transform.forward * distance;
            transform.rotation = mainCamera.transform.rotation;
        }
    }
}