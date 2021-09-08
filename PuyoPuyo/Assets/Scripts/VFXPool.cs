using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXPool : MonoBehaviour
{
    public static VFXPool instance;

    [SerializeField] private GameObject redVFXPrefab;
    [SerializeField] private GameObject blueVFXPrefab;
    [SerializeField] private GameObject greenVFXPrefab;
    [SerializeField] private GameObject yellowVFXPrefab;
    private int vfxPoolLenght = 15; //Size of the each color VFX Pool
    private Transform redVFXContainer;
    private Transform blueVFXContainer;
    private Transform greenVFXContainer;
    private Transform yellowVFXContainer;
    private List<ParticleSystem> redVFXPool = new List<ParticleSystem>();
    private List<ParticleSystem> blueVFXPool = new List<ParticleSystem>();
    private List<ParticleSystem> greenVFXPool = new List<ParticleSystem>();
    private List<ParticleSystem> yellowVFXPool = new List<ParticleSystem>();

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
        redVFXContainer = transform.GetChild(0);
        blueVFXContainer = transform.GetChild(1);
        greenVFXContainer = transform.GetChild(2);
        yellowVFXContainer = transform.GetChild(3);
        CreateVFXPools();
    }

    void CreateVFXPools()
    {
        for (int i = 0; i < vfxPoolLenght; i++)
        {
            ParticleSystem redVFX = Instantiate(redVFXPrefab, Vector3.zero, Quaternion.identity, redVFXContainer).GetComponent<ParticleSystem>();
            redVFXPool.Add(redVFX);
            ParticleSystem blueVFX = Instantiate(blueVFXPrefab, Vector3.zero, Quaternion.identity, blueVFXContainer).GetComponent<ParticleSystem>();
            blueVFXPool.Add(blueVFX);
            ParticleSystem greenVFX = Instantiate(greenVFXPrefab, Vector3.zero, Quaternion.identity, greenVFXContainer).GetComponent<ParticleSystem>();
            greenVFXPool.Add(greenVFX);
            ParticleSystem yellowVFX = Instantiate(yellowVFXPrefab, Vector3.zero, Quaternion.identity, yellowVFXContainer).GetComponent<ParticleSystem>();
            yellowVFXPool.Add(yellowVFX);
        }
    }

    public void SpawnPopVFX(int _color, Vector3 _position)
    {
        List<ParticleSystem> correctColorVFXPool = new List<ParticleSystem>();
        switch (_color)
        {
            case 0:
                correctColorVFXPool = redVFXPool;
                break;

            case 1:
                correctColorVFXPool = blueVFXPool;
                break;

            case 2:
                correctColorVFXPool = greenVFXPool;
                break;

            case 3:
                correctColorVFXPool = yellowVFXPool;
                break;
        }

        for (int i = 0; i < vfxPoolLenght; i++)
        {
            if (!correctColorVFXPool[i].isPlaying)
            {
                correctColorVFXPool[i].transform.position = _position;
                correctColorVFXPool[i].Play();
                break;
            }
        }
    }
}
