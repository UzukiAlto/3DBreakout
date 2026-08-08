using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerBallTarget : MonoBehaviour, IBallTarget
    {
        [SerializeField] private GameManager gameManager;
        private Color hitPlayerColor = new Color(0.46f, 0.92f, 0.40f, 1f);

        // Ballが衝突した際の反発の仕方を定義
        public void OnHitBallObject(Rigidbody rigidbody, float speed)
        {
            
            Vector3 reflectVec = rigidbody.transform.position - transform.position;
            reflectVec.Normalize();            
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(speed * reflectVec * GameState.gameSpeed);
        }

        public void OnHitBallRaycast(Material material, float alpha)
        {
            Color newBallColor = hitPlayerColor;
            newBallColor.a = alpha;
            material.color = newBallColor;
        }

    }
}