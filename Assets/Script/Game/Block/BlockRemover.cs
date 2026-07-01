using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class BlockRemover : MonoBehaviour
    {
        public void RemoveBlock(GameObject removeTargetBlock, List<GameObject> blockList)
        {
            blockList.Remove(removeTargetBlock);
            Destroy(removeTargetBlock);
        }
        public void ClearAllBlocks(List<GameObject> blockList)
        {
            foreach (GameObject block in blockList)
            {
                Destroy(block);
            }
            blockList.Clear();
        }
    }
}