using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameOverRangeBallTarget : MonoBehaviour, IBallTarget
    {
        private Color hitGameOverRangeColor = new Color(0.69f, 0.40f, 0.91f, 1f);
        private GameManager gameManager;

        private void Awake()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        // Ballが衝突した際の反発の仕方を定義
        public void OnHitBallObject(Rigidbody rigidbody, float speed)
        {
            gameManager.FailGame();
        }

        public void OnHitBallRaycast(Material material, float alpha)
        {
            Color newBallColor = hitGameOverRangeColor;
            newBallColor.a = alpha;
            material.color = newBallColor;
        }
    }
}