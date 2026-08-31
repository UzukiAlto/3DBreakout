using UnityEngine;

namespace Game
{
    public class SafetyPanelBallTarget : MonoBehaviour, IBallTarget
    {
        // 予測線に使う色（プレイヤーパネルと同じ色）
        private Color hitSafetyPanelColor = new Color(0.46f, 0.92f, 0.40f, 1f);

        /// <summary>
        /// ボールが当たったとき。SafetyPanelは静的な壁のため、
        /// 反射はPhysicsMaterialに任せ、追加処理は行わない。
        /// </summary>
        public void OnHitBallObject(Rigidbody rigidbody, float ballSpeed)
        {
            // 物理エンジンによる反射に任せる
        }

        /// <summary>
        /// 予測レイが当たったとき、予測線の色を変更する。
        /// </summary>
        public void OnHitBallRaycast(Material material, float alpha)
        {
            Color newBallColor = hitSafetyPanelColor;
            newBallColor.a = alpha;
            material.color = newBallColor;
        }
    }
}