using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game
{
    public class BlockManager : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private BlockGenerator blockGenerator;
        [SerializeField] private BlockRemover blockRemover;
        [SerializeField] private BallController ballController;
        // 中身の確認のためにSerializeFieldで公開
        [SerializeField] private List<GameObject> currentBlockList = new List<GameObject>();
        private GameObject blockParent;
        private int ballTargetLayer;
        private int blockTempLayer;

        private void Awake()
        {
            ballTargetLayer = LayerMask.NameToLayer("BallTarget");
            blockTempLayer = LayerMask.NameToLayer("BlockTemp");
        }

        private void OnEnable()
        {
            gameManager.OnGameReady += GenerateBlock;
            gameManager.OnAllBlocksRemoved += HandleAllBlocksRemoved;
        }

        private void OnDisable()
        {
            gameManager.OnGameReady -= GenerateBlock;
            gameManager.OnAllBlocksRemoved -= HandleAllBlocksRemoved;
            ballController.OnBallTouched -= EnableBlockColliders;
        }

        public void GenerateBlock()
        {
            blockParent = blockGenerator.GenerateBlock();

            foreach (Transform childrenBlockTransform in blockParent.transform)
            {
                currentBlockList.Add(childrenBlockTransform.gameObject);
            }
        }

        public void RemoveBlock(GameObject removeTargetBlock)
        {
            blockRemover.RemoveBlock(removeTargetBlock, currentBlockList);
            if (currentBlockList.Count == 0)
            {
                Destroy(blockParent);
                gameManager.RaiseAllBlocksRemoved();
            }
        }

        public void HandleAllBlocksRemoved()
        {
            GenerateBlock();
            
            // ブロックを再生成するとすぐにボールに当たるため、ボールが他の何かに当たるまでLayerを変更して当たり判定を無効化する
            foreach (GameObject block in currentBlockList)
            {
                block.layer = blockTempLayer;
            }
            ballController.OnBallTouched += EnableBlockColliders;
        }
        public void EnableBlockColliders()
        {
            ballController.OnBallTouched -= EnableBlockColliders;
            foreach (GameObject block in currentBlockList)
            {
                block.layer = ballTargetLayer;
            }
        }
    }
}