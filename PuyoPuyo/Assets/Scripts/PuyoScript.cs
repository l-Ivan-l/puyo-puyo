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

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
    }
}
