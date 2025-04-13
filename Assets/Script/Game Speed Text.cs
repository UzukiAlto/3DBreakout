using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSpeedText : MonoBehaviour
{
    private TMP_Text isText;
    // Start is called before the first frame update
    void Start()
    {
        isText = gameObject.GetComponent<TMP_Text>();

        isText.text = "GameSpeed 1";
    }

    public void ChangeText()
    {
        isText.text = "GameSpeed " + 
                    (GM.gameSpeed - GM.defaultgameSpeed + 1);
    }
}
