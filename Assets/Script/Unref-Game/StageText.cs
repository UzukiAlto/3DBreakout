using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Obsolete("リファクタリング移行中")]
public class StageText : MonoBehaviour
{

    private TMP_Text myText;
    // Start is called before the first frame update
    void Start()
    {
        myText = gameObject.GetComponent<TMP_Text>();

        myText.text = "Stage " + GM.currentStage;
    }

    public void Update()
    {
        myText.text = "Stage " + GM.currentStage;
    }
}
