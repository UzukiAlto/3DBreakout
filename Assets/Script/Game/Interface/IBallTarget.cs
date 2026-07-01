using UnityEngine;

namespace Game
{
    public interface IBallTarget
    {
        public void OnHitBallObject(Rigidbody rigidbody, float ballSpeed);
        public void OnHitBallRaycast(Material material, float alpha);
    }
}