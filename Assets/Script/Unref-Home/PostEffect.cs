using UnityEngine;
using System.Collections;

[System.Obsolete("リファクタリング移行中")]
public class PostEffect : MonoBehaviour
{

    public Material outline;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, outline);
    }
}