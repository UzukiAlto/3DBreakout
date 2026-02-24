using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete("リファクタリング移行中")]
public class BlockArea : MonoBehaviour
{
    public BlockManager blockManager;
    public Material blockAreaMaterial;
    private Color defaultBlockAreaCol = new Color(0.63f, 0.62f, 0.95f, 0.50f);
    // private float flashTime = 0f;
    // private float flashSpeed = 1f;
    public bool isBallInArea = false;
    private bool isHidden = false;
    private bool isGenerat;

    void Start()
    {
        blockManager = GameObject.Find("BlockManager").GetComponent<BlockManager>();
        isHidden = false;
        isGenerat = true;
        blockAreaMaterial.color = defaultBlockAreaCol;
        // if (isBallInArea)
        // {
        //     HideObject();
        // }else{

        //     gameObject.SetActive(false);
        // }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Ball"))
        {
            isBallInArea = true;
            Debug.Log("Ball is in area");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            isBallInArea = false;
            if(isHidden)
            {
                ApperObjct();
            }
        }
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
    }
    private void HideObject()
    {
        isHidden = true;
        foreach (GameObject block in blockManager.blockList)
        {
            block.SetActive(false);
        }
    }
    private void ApperObjct()
    {
        isHidden = false;
        foreach (GameObject block in blockManager.blockList)
        {
            block.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
