using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigManager : MonoBehaviour
{
    public GameObject configCanvas;
    public Slider bgmSlider;
    public float bgmValue;

    public Slider seSlider;
    public float seValue;

    public Slider sensitivitySlider;
    public float sensitivityValue;
    void Start()
    {
        bgmValue = bgmSlider.value;
        seValue = seSlider.value;
        sensitivityValue = sensitivitySlider.value;


    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetSliderValue()
    {

        bgmValue = bgmSlider.value;
        seValue = seSlider.value;
        sensitivityValue = sensitivitySlider.value;
    }
}
