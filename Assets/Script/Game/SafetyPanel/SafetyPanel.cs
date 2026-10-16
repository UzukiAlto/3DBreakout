using UnityEngine;

namespace Game
{
    public class SafetyPanel : MonoBehaviour
    {
        // このパネルが存在する面と同じ面のGameOverRange
        [SerializeField] private GameObject gameOverRange;

        /// <summary>
        /// パネルを有効化し、同じ面のGameOverRangeを無効化する
        /// </summary>
        public void Activate()
        {
            gameObject.SetActive(true);
            if (gameOverRange != null)
            {
                gameOverRange.SetActive(false);
            }
        }

        /// <summary>
        /// パネルを無効化し、同じ面のGameOverRangeを有効化する
        /// </summary>
        public void Deactivate()
        {
            gameObject.SetActive(false);
            if (gameOverRange != null)
            {
                gameOverRange.SetActive(true);
            }
        }
    }
}