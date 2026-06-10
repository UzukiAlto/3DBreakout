using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainSystem;

namespace Game
{
    /// <summary>
    /// プレイヤーとブロックの間のRayを処理するクラス
    /// </summary>
    public class PlayerRaycastHandler : MonoBehaviour
    {
        [SerializeField] 
        private ScreenBase gameScreen;
        [SerializeField] 
        private GameObject centerObj;
        [SerializeField] 
        private LayerMask panelLayer;
        public GameObject hitObject { get; private set; }
        private GameObject screenCamera;
        private float rayLength;

        void Awake()
        {
            screenCamera = gameScreen.screenCamera.gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 subCamePos = screenCamera.transform.position;
            Vector3 centerObjPos = centerObj.transform.position;
            rayLength = Vector3.Distance(subCamePos, centerObjPos);

            Ray ray = new Ray(centerObjPos, subCamePos);

            Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.green);

            RaycastHit hit;


            if (Physics.Raycast(ray, out hit, rayLength, panelLayer))
            {
                hitObject = hit.collider.gameObject;
            }
            else
            {
                hitObject = null;
            }
        
        }
    }
}