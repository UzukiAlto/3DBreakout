using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class ConfigSlider : MonoBehaviour
{
    public RawImage selectedRawImage;
    public VideoPlayer selectedVideoPlayer;
    public Image unselectedImage;
    public TMP_Text myText;

    private Color defaultTextCol = new Color(0.05f, 0.05f, 0.13f, 1f);
    private Color selectedTextCol = new Color(0.56f, 0.53f, 0.3f, 1f);
    // Start is called before the first frame update
    void Start()
    {
        // selectedRawImage = GameObject.Find("Handle Slide Area/RawImage").GetComponent<RawImage>();
        // selectedVideoPlayer = GameObject.Find("Handle Slide Area/RawImage").GetComponent<VideoPlayer>();
        // unselectedImage = GameObject.Find("Handle Slide Area/UnselectedImage").GetComponent<Image>();
        // myText = GameObject.Find("Text Object/Text (TMP) (1)").GetComponent<TMP_Text>();

        selectedRawImage.enabled = false;
        selectedVideoPlayer.enabled = false;
        unselectedImage.enabled = true;
        myText.color = defaultTextCol;
    }

    public void PointerDown()
    {

        selectedRawImage.enabled = true;
        selectedVideoPlayer.enabled = true;
        unselectedImage.enabled = false;
        myText.color = selectedTextCol;

    }
    public void PointerUp()
    {

        selectedRawImage.enabled = false;
        selectedVideoPlayer.enabled = false;
        unselectedImage.enabled = true;
        myText.color = defaultTextCol;

    }
}
