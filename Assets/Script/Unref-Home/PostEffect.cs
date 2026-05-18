using UnityEngine;
using System.Collections;

public class PostEffect : MonoBehaviour
{

    public Material outline;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, outline);
    }
}