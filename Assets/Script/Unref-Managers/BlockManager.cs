using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Obsolete("リファクタリング移行中")]
public class BlockManager : MonoBehaviour
{
    public List<GameObject> blockList;
    public GameObject blocksParent;
    private GameObject blocksChildren;
    public GameObject cubes;
    public GameObject spheres;
    public GameObject pyramids;
    public Material blockMaterial;
    private int defaultLayer;
    private int blockTempLayer;
    public GM gm;
    private List<GameObject> allBlocks = new List<GameObject>();
    private bool isTempBlock = false;


    void Start()
    {
        defaultLayer = LayerMask.NameToLayer("Default");
        blockTempLayer = LayerMask.NameToLayer("BlockTemp");
        allBlocks = new List<GameObject>(){
            cubes, spheres, pyramids
        };
        InitializeBloack();


    }

    private int newBlockType;
    public void GenerateBlocks()
    {
        if(blocksChildren != null){
            Destroy(blocksChildren);
        }
        while(true)
        {
            newBlockType = (int)Random.Range(0, allBlocks.Count);
            if(GM.currentBlockType != newBlockType)
            {
                break;
            }
        }
        GM.currentBlockType = newBlockType;
        
        GameObject nextBlock = allBlocks[GM.currentBlockType];
        Quaternion quaternion = Quaternion.Euler(Random.Range(-30, 30), Random.Range(-30, 30), 0);
        blocksChildren = Instantiate(nextBlock, Vector3.zero, quaternion);
        blocksChildren.transform.SetParent(blocksParent.transform);
    }

    private void GetBlocks()
    {
        blockList = new List<GameObject>();
        Transform blocks = blocksChildren.GetComponentInChildren<Transform>();
        if (blocks.childCount == 0)
        {
            return;
        }else{
            
            foreach (Transform blockObj in blocks)
            {
                if(blockObj.CompareTag("Block"))
                {
                    blockList.Add(blockObj.gameObject);
                    blockObj.gameObject.SetActive(true);
                }
            } 
        }
    }


    public void DestroyBlock(GameObject target)
    {
        blockList.Remove(target);
        // Destroy(target);
        target.SetActive(false);
        // Debug.Log(blockList.Count + " blocks remain");

        // 次のゲームに移行
        if(blockList.Count <= 0)
        {

            GoNextStage();
        }
    }

    public void InitializeBloack()
    {
        GenerateBlocks();
        GetBlocks();
    }

    public void GoNextStage()
    {
        InitializeBloack();
        gm.NextStage();
        // List<GameObject> tempBlockList = new List<GameObject>(blockList);
        foreach (GameObject obj in blockList)
        {
            obj.layer = blockTempLayer;
        }
        isTempBlock = true;

        Color color = blockMaterial.color;
        color.a = 0;
        blockMaterial.color = color;
        StartCoroutine("fadeout");
    }

    private float defaultAlpha = 0.4f;
    public float fadeSpeed;
    IEnumerator fadeout()
    {
        Color color = blockMaterial.color;
        while (true)
        {
            color.a += 0.01f * fadeSpeed;
            if(color.a < defaultAlpha)
            {
                blockMaterial.color = color;
                yield return null;
            }else{
                color.a = defaultAlpha;
                blockMaterial.color = color;
                yield break;
            }
        }
    }

    public void ChangeDefaultLayer()
    {
        if (isTempBlock)
        {
            foreach (GameObject obj in blockList)
            {
                obj.layer = defaultLayer;
            }
            isTempBlock = false;
        }
    }
}
