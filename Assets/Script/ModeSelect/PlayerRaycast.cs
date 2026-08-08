using UnityEngine;

namespace ModeSelect
{
    /// <summary>
    /// カメラからキューブに向けてモード選択用のレイを飛ばすクラス
    /// </summary>
    public class PlayerRaycast : MonoBehaviour
    {
        [SerializeField] private GameObject cameraObject;
        [SerializeField] private GameObject cubeObject;
        [SerializeField] private LayerMask modeTargetLayer;
        public GameObject GetRaycastHitGameObject()
        {
            GameObject result = null;
            Vector3 cameraPosition = cameraObject.transform.position;
            Vector3 cubePosition = cubeObject.transform.position;
            float rayLength = Vector3.Distance(cameraPosition, cubePosition);

            Ray ray = new Ray(cameraPosition, cubePosition - cameraPosition);
            Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);

            RaycastHit hit;


            if (Physics.Raycast(ray, out hit, rayLength, modeTargetLayer))
            {
                result = hit.collider.gameObject;
            }
            return result;

        }
    }
}