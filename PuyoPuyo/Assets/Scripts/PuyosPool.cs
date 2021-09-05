using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyosPool : MonoBehaviour
{
    public static PuyosPool instance;

    [SerializeField] private GameObject puyoPrefab;
    private int puyosPoolLenght = 72; //Size of the grid
    private List<GameObject> puyosPool = new List<GameObject>();

    public Sprite[] puyoSprites;
    private int puyoColor;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreatePuyosPool();
    }

    void CreatePuyosPool()
    {
        for (int i = 0; i < puyosPoolLenght; i++)
        {
            GameObject puyo = Instantiate(puyoPrefab, Vector3.zero, Quaternion.identity, this.transform);
            puyo.SetActive(false);
            puyosPool.Add(puyo);
        }
    }

    public GameObject SpawnPuyo(Vector3 _position)
    {
        for (int i = 0; i < puyosPool.Count; i++)
        {
            if (!puyosPool[i].activeInHierarchy)
            {
                puyosPool[i].transform.position = _position;
                puyoColor = Random.Range(0, 4);
                puyosPool[i].GetComponent<PuyoScript>().puyoColor = puyoColor;
                puyosPool[i].GetComponent<PuyoScript>().puyoSprite = puyoSprites[puyoColor];
                puyosPool[i].GetComponent<PuyoScript>().isFalling = true;
                puyosPool[i].SetActive(true);
                return puyosPool[i];
            }
        }
        return null;
    }
}
