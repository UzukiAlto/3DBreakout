using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModeSelect
{
    public class ModeText : MonoBehaviour
    {
        [SerializeField] private GameObject mainCameraObj;

        private void Update()
        {
            if (mainCameraObj == null) return;
            Vector3 rot = Vector3.zero;
            rot.z = mainCameraObj.transform.eulerAngles.z;
            transform.localEulerAngles = rot;
        }
    }
}