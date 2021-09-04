using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyoScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite puyoSprite;
    public int puyoColor = 0; //1 = Red, 2 = Blue, 3 = Green, 4 = Yellow

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = puyoSprite;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
