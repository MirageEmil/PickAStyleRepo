using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private SpriteRenderer playerSr;

    public Sprite idleSprite;
    public Sprite[] animSprites;

    private int animFrame;
    public float animTime;

    public bool loop = true;
    public bool idle = true;

    // Start is called before the first frame update
    private void Start()
    {
        playerSr = GetComponent<SpriteRenderer>();
        InvokeRepeating(nameof(NextFrame), animTime, animTime);

    }

    private void NextFrame()
    {
        animFrame++;

        if(loop && animFrame >= animSprites.Length)
        {
            animFrame = 0;

        }

        if (idle)
        {
            playerSr.sprite = idleSprite;

        }
        else if(animFrame >= 0 &&  animFrame < animSprites.Length)
        {
            playerSr.sprite = animSprites[animFrame];

        }

    }

    private void OnEnable()
    {
        

    }

    private void OnDisable()
    {
        

    }

}
