using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyosController : MonoBehaviour
{
    private InputMaster inputMaster;

    public GameObject[] puyosSpawners; 
    [SerializeField]
    private GameObject[] currentPuyos = new GameObject[2]; //Current falling puyos

    private Transform[,] grid = new Transform[6, 12];

    private bool puyosPairExist;
    private float gridLimitY = 11;
    private float gridLimitX = 5;
    private float fallingRate = 0.6f;
    private float fallTimer = 0.0f;

    private int currentRotation = 0;
    private Transform rotatingPuyo;
    private Transform pivotPuyo;

    private bool puyoOnGrid = false;

    private void Awake()
    {
        SetUpInputs();
    }

    private void OnEnable()
    {
        inputMaster.Enable();
    }

    private void OnDisable()
    {
        inputMaster.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnPuyos", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        PuyosFalling();
    }

    void SetUpInputs()
    {
        inputMaster = new InputMaster();
        inputMaster.GameplayActions.MoveLeft.performed += _ => CurrentPuyosMovement(-1);
        inputMaster.GameplayActions.MoveRight.performed += _ => CurrentPuyosMovement(1);
        inputMaster.GameplayActions.MoveDown.performed += _ => FasterFall();
        inputMaster.GameplayActions.MoveDown.canceled += _ => NormalFall();
        inputMaster.GameplayActions.Rotate.performed += _ => RotatePuyos();
    }

    void SpawnPuyos()
    {
        NormalFall(); //Restore to normal fall for the next pair
        for (int i = 0; i < 2; i++)
        {
            currentPuyos[i] = PuyosPool.instance.SpawnPuyo(puyosSpawners[i].transform.position);
        }
        rotatingPuyo = currentPuyos[0].transform;
        pivotPuyo = currentPuyos[1].transform;
        puyosPairExist = true;
        puyoOnGrid = false;
    }

    void CurrentPuyosMovement(int _direction)
    {
        if(puyosPairExist && !puyoOnGrid)
        {
            foreach (GameObject puyo in currentPuyos)
            {
                Vector3 newPos = puyo.transform.position;
                newPos.x += _direction;
                puyo.transform.position = newPos;
            }
            if(BoundsOrCollision(currentPuyos[0].transform.position) || BoundsOrCollision(currentPuyos[1].transform.position))
            {
                foreach (GameObject puyo in currentPuyos)
                {
                    Vector3 newPos = puyo.transform.position;
                    newPos.x -= _direction;
                    puyo.transform.position = newPos;
                }
            }
        }
    }
    
    void RotatePuyos()
    {
        if (puyosPairExist && !puyoOnGrid)
        {
            SwapPuyos();
            currentRotation++;
            Vector3 oldPos = rotatingPuyo.position;
            Vector3 newPos = pivotPuyo.position;
            switch (currentRotation)
            {
                case 1:
                    newPos.y -= 1;
                    break;

                case 2:
                    newPos.x += 1;
                    break;

                case 3:
                    newPos.y += 1;
                    break;

                case 4:
                    newPos.x -= 1;
                    currentRotation = 0;
                    break;
            }
            rotatingPuyo.position = newPos;

            if (BoundsOrCollision(rotatingPuyo.position))
            {
                RotatePuyos();
            }
            else
            {
                SwapPuyos();
            }
        }
    }

    void SwapPuyos() //Used for keeping the upper puyo at the end of the array
    {
        if (currentRotation == 3)
        {
            GameObject swapAux = currentPuyos[1];
            currentPuyos[1] = currentPuyos[0];
            currentPuyos[0] = swapAux;
        }
    }

    void PuyosFalling()
    {
        if(puyosPairExist)
        {
            fallTimer += Time.deltaTime;

            if (fallTimer > fallingRate)
            {
                foreach (GameObject puyo in currentPuyos)
                {
                    Vector3 newPos = puyo.transform.position;
                    newPos.y -= 1;
                    puyo.transform.position = newPos;

                    if (BoundsOrCollision(puyo.transform.position))
                    {
                        newPos = puyo.transform.position;
                        newPos.y += 1;
                        puyo.transform.position = newPos;

                        AddPuyoToGrid(puyo.transform);
                    }
                }
                
                CheckIfPuyosFalling(); //Check if both puyos stopped falling
                fallTimer = 0.0f;
            }
        }
    }

    void FasterFall()
    {
        fallingRate = 0.05f;
    }

    void NormalFall()
    {
        fallingRate = 0.6f;
    }

    bool BoundsOrCollision(Vector3 _position)
    {
        int x = (int)_position.x;
        int y = (int)_position.y;

        if(x < 0 || x > gridLimitX || y < 0) //Outside grid bounds
        {
            return true;
        }

        if(y <= gridLimitY) //Avoid out of index errors
        {
            if (grid[x, y] != null) //Colliding with an existing puyo
            {
                return true;
            }
        }

        return false;
    }

    void AddPuyoToGrid(Transform _puyo)
    {
        int x = (int)_puyo.position.x;
        int y = (int)_puyo.position.y;
        grid[x, y] = _puyo;

        _puyo.GetComponent<PuyoScript>().isFalling = false;
        puyoOnGrid = true; //At least one puyo on grid
        FasterFall(); //Faster fall for the remaining puyo
    }

    void CheckIfPuyosFalling()
    {
        int notFalling = 0;
        foreach(GameObject puyo in currentPuyos)
        {
            if(!puyo.GetComponent<PuyoScript>().isFalling)
            {
                notFalling++;
            }
        }
        if(notFalling == 2)
        {
            puyosPairExist = false;
            currentRotation = 0;
            Invoke("SpawnPuyos", 0.5f);
        }
    }
}
