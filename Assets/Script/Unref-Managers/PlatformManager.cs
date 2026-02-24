using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete("リファクタリング移行中")]
public class PlatformManager : MonoBehaviour
{
    public bool isSmartphone;
    void Start()
    {
        isSmartphone = UnityEngine.Application.isMobilePlatform;
        if(!isSmartphone)
        {
            gameObject.SetActive(false);
        }

        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
