using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class BlockManager : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private BlockGenerator blockGenerator;
        [SerializeField] private BlockRemover blockRemover;
        // 中身の確認のためにSerializeFieldで公開
        [SerializeField] private List<GameObject> currentBlockList = new List<GameObject>();

        private void OnEnable()
        {
            gameManager.OnGameReady += GenerateBlock;
        }

        private void OnDisable()
        {
            gameManager.OnGameReady -= GenerateBlock;
        }

        public void GenerateBlock()
        {
            GameObject nextBlock = blockGenerator.GenerateBlock();

            foreach (Transform childrenBlockTransform in nextBlock.transform)
            {
                currentBlockList.Add(childrenBlockTransform.gameObject);
            }
        }

        public void RemoveBlock(GameObject removeTargetBlock)
        {
            blockRemover.RemoveBlock(removeTargetBlock, currentBlockList);
        }
    }
}