using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyoScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite puyoSprite;
    public int color = -1; //0 = Red, 1 = Blue, 2 = Green, 3 = Yellow
    public int x = -1;
    public int y = -1;
    public bool isFalling = false;

    public bool leftConnected = false;
    public bool rightConnected = false;
    public bool upperConnected = false;
    public bool bottomConnected = false;

    public Sprite[] redPuyoSprites;
    public Sprite[] bluePuyoSprites;
    public Sprite[] greenPuyoSprites;
    public Sprite[] yellowPuyoSprites;

    private int downValue = 1;
    private int upValue = 2;
    private int rightValue = 4;
    private int leftValue = 8;
    private int spriteIndex = 0;

    private bool popped = false;
    private Animator puyoAnim;
    public AnimationClip popClip;
    public AnimationClip placedClip;

    public Sprite crossSprite;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        puyoAnim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        spriteRenderer.sprite = puyoSprite;
    }

    private void OnDisable()
    {
        color = -1;
        x = -1;
        y = -1;
        leftConnected = false;
        rightConnected = false;
        upperConnected = false;
        bottomConnected = false;
        popped = false;
    }

    public void UpdateSprite()
    {
        if(popped)
        {
            spriteIndex = 16;
        }
        else
        {
            spriteIndex = 0;

            if (leftConnected)
            {
                spriteIndex += leftValue;
            }
            if (rightConnected)
            {
                spriteIndex += rightValue;
            }
            if (upperConnected)
            {
                spriteIndex += upValue;
            }
            if (bottomConnected)
            {
                spriteIndex += downValue;
            }
        }

        switch (color)
        {
            case 0:
                spriteRenderer.sprite = redPuyoSprites[spriteIndex];
            break;

            case 1:
                spriteRenderer.sprite = bluePuyoSprites[spriteIndex];
            break;

            case 2:
                spriteRenderer.sprite = greenPuyoSprites[spriteIndex];
            break;

            case 3:
                spriteRenderer.sprite = yellowPuyoSprites[spriteIndex];
            break;
        }
    }

    public void Placed()
    {
        SoundManager.instance.PlayPlacedSound(1f);
        puyoAnim.Play(placedClip.name);
    }

    public IEnumerator Pop()
    {
        popped = true;
        UpdateSprite();
        puyoAnim.Play(popClip.name);
        yield return new WaitForSeconds(0.2f);
        VFXPool.instance.SpawnPopVFX(color, transform.position);
        gameObject.SetActive(false);
    }

    public void OnTop()
    {
        puyoAnim.Play(placedClip.name);
        spriteRenderer.sprite = crossSprite;
    }
}
