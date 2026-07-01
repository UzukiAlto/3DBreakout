using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class BlockBallTarget : MonoBehaviour, IBallTarget
    {
        private Color hitBlockColor = new Color(0.89f, 0.91f, 0.40f, 1f);

        // Ballが衝突した際の反発の仕方を定義
        public void OnHitBallObject(Rigidbody rigidbody, float speed)
        {
            
        }

        public void OnHitBallRaycast(Material material, float alpha)
        {
            Color newBallColor = hitBlockColor;
            newBallColor.a = alpha;
            material.color = newBallColor;
        }
    }
}