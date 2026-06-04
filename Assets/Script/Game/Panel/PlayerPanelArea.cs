using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerPanelArea : MonoBehaviour
    {
        public Vector3 normalVector = Vector3.forward; // 面の法線ベクトル
        // プレイヤーパネルが移動できる範囲を定義するためのBounds
        public Bounds moveableArea;
        void Awake()
        {
            moveableArea = transform.GetComponent<BoxCollider>().bounds;
        }
    }
}