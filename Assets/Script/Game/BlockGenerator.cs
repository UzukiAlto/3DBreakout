using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class BlockGenerator : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private GameObject cubes;
        [SerializeField] private GameObject spheres;
        [SerializeField] private GameObject pyramids;
        [SerializeField] private GameObject blockParent;
        [SerializeField] private Material blockMaterial;
        private List<GameObject> currentBlockList = new List<GameObject>();
        private List<GameObject> allBlockList = new List<GameObject>();

        private void Awake()
        {
            allBlockList = new List<GameObject>()
            {
                cubes, spheres, pyramids
            };
        }

        private void OnEnable()
        {
            gameManager.OnGameReady += GenerateBlock;
        }

        void OnDisable()
        {
            
            gameManager.OnGameReady -= GenerateBlock;
        }

        public void GenerateBlock()
        {
            // 出現させるブロックの種類、傾きをランダムにする
            GameObject nextBlockPrefab = allBlockList[Random.Range(0, allBlockList.Count)];
            
            Quaternion quaternion = Quaternion.Euler(Random.Range(-30, 30), Random.Range(-30, 30), 0);
            GameObject nextBlock = Instantiate(nextBlockPrefab, Vector3.zero, quaternion);
            nextBlock.transform.SetParent(blockParent.transform);
            currentBlockList = new List<GameObject>();
            foreach (Transform childrenBlockTransform in nextBlock.transform)
            {
                currentBlockList.Add(childrenBlockTransform.gameObject);
            }
        }
    }
}