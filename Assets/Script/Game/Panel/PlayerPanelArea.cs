using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerPanelArea : MonoBehaviour
    {
        [SerializeField] private GameObject moveableAreaObject;
        public Vector3 normalVector;
        // プレイヤーパネルが移動できる範囲を定義するためのBounds
        public Bounds moveableAreaBounds;
        void Awake()
        {
            normalVector = transform.forward;
            moveableAreaBounds = moveableAreaObject.GetComponent<BoxCollider>().bounds;
        }
    }
}