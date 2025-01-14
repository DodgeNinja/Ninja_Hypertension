using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBlinkFeedBack : FeedBack
{

    private readonly int BlinkHash = Shader.PropertyToID("_AddColorFade");
    private readonly int ShakeHash = Shader.PropertyToID("_VibrateFade");

    [SerializeField] private GameObject root;
    
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        
        spriteRenderer = root == null ? GetComponent<SpriteRenderer>() : root.GetComponent<SpriteRenderer>();

    }

    public override void CreateFeedBack()
    {

        spriteRenderer.material.SetFloat(BlinkHash, 1f);
        spriteRenderer.material.SetFloat(ShakeHash, 1f);
        FAED.InvokeDelayReal(() =>
        {

            spriteRenderer.material.SetFloat(BlinkHash, 0f);
            spriteRenderer.material.SetFloat(ShakeHash, 0f);

        }, 0.3f);

    }

    public override void EndFeedBack()
    {
        
    }
}
