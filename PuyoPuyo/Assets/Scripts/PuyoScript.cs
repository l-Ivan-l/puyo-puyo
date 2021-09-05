using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyoScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite puyoSprite;
    public int puyoColor = 0; //0 = Red, 1 = Blue, 2 = Green, 3 = Yellow
    public bool isFalling = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        spriteRenderer.sprite = puyoSprite;
    }
}
