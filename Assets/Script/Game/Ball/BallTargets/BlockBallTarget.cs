using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class BlockBallTarget : MonoBehaviour, IBallTarget
    {
        private BlockManager blockManager;
        private Color hitBlockColor = new Color(0.89f, 0.91f, 0.40f, 1f);
        private void Awake()
        {
            // BlockはPrefabでありInspectorで設定できないため、GameObject.FindでBlockManagerを取得する
            blockManager = GameObject.Find("BlockManager").GetComponent<BlockManager>();
        }

        // Ballが衝突した際の反発の仕方を定義
        public void OnHitBallObject(Rigidbody rigidbody, float speed)
        {
            blockManager.RemoveBlock(this.gameObject);
        }

        public void OnHitBallRaycast(Material material, float alpha)
        {
            Color newBallColor = hitBlockColor;
            newBallColor.a = alpha;
            material.color = newBallColor;
        }
    }
}