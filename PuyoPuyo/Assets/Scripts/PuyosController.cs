using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyosController : MonoBehaviour
{
    private InputMaster inputMaster;

    public GameController gameController;

    public GameObject[] puyosSpawners; 
    private GameObject[] currentPuyos = new GameObject[2]; //Current falling puyos

    private Transform[,] grid = new Transform[6, 12];
    private int[,] gridColors = new int[6, 12];
    private bool[,] checkedGrid = new bool[6, 12];

    private bool puyosPairExist;
    private float gridLimitY = 11;
    private float gridLimitX = 5;
    private float fallingRate = 0.6f;
    private float fallTimer = 0.0f;
    private float fallingBlocksRate = 0.05f;

    private int currentRotation = 0;
    private Transform rotatingPuyo;
    private Transform pivotPuyo;

    private bool puyoOnGrid = false;

    private int comboCount = 0;

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
        InitGridColors();
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

    void InitGridColors()
    {
        for (int i = 0; i < gridColors.GetLength(0); i++)
        {
            for (int j = 0; j < gridColors.GetLength(1); j++)
            {
                gridColors[i, j] = -1;
            }
        }
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
        comboCount = 0;
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
        gridColors[x, y] = _puyo.GetComponent<PuyoScript>().color;

        PuyoScript puyoData = _puyo.GetComponent<PuyoScript>();
        puyoData.isFalling = false;
        puyoData.x = x;
        puyoData.y = y;

        puyoOnGrid = true; //At least one puyo on grid
        FasterFall(); //Faster fall for the remaining puyo
    }

    void RemovePuyoFromGrid(Transform _puyo)
    {
        int x = (int)_puyo.position.x;
        int y = (int)_puyo.position.y;
        grid[x, y] = null;
        gridColors[x, y] = -1;
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
            SearchForConnections();
            puyosPairExist = false;
            currentRotation = 0;
        }
    }

    void SearchForConnections()
    {
        Debug.Log("<color=blue>CHECK GRID</color>");

        ResetCheckedGrid();
        for(int i = 0; i < gridColors.GetLength(0); i++)
        {
            for(int j = 0; j < gridColors.GetLength(1); j++)
            {
                //Debug.Log("Color in: (" + i + ", " + j + ") = " + gridColors[i, j]);
                if(gridColors[i, j] > -1 && !checkedGrid[i, j]) //There is a puyo in that cell that hasn't been checked
                {
                    InitPuyosSearch(grid[i, j].GetComponent<PuyoScript>(), i, j);
                }
            }
        }

        StartCoroutine(AdjustFloatingPuyos());
    }

    void ResetCheckedGrid()
    {
        for (int i = 0; i < checkedGrid.GetLength(0); i++)
        {
            for (int j = 0; j < checkedGrid.GetLength(1); j++)
            {
                checkedGrid[i, j] = false;
            }
        }
    }

    void InitPuyosSearch(PuyoScript _initPuyo, int _x, int _y)
    {
        List<PuyoScript> totalConnectedPuyos = new List<PuyoScript>();
        bool connectionsFinished = false;
        var puyosGroup = GetPuyosGroup(_initPuyo, _x, _y);
        totalConnectedPuyos.AddRange(puyosGroup);

        while (!connectionsFinished)
        {
            var puyosToAdd = new List<PuyoScript>();

            foreach (PuyoScript puyo in totalConnectedPuyos)
            {
                var connectedPuyos = GetPuyosGroup(puyo, puyo.x, puyo.y);
                if (connectedPuyos.Count > 0)
                {
                    puyosToAdd.AddRange(connectedPuyos);
                } 
            }

            if(puyosToAdd.Count > 0)
            {
                totalConnectedPuyos.AddRange(puyosToAdd);
            }
            else
            {
                connectionsFinished = true;
            }
        }
        //Debug.Log("Connected puyos: " + totalConnectedPuyos.Count);
        foreach (PuyoScript puyo in totalConnectedPuyos)
        {
            puyo.UpdateSprite();
        }
        

        if (totalConnectedPuyos.Count > 3)
        {
            StartCoroutine(ChainCompleted(totalConnectedPuyos));
        }
    }

    List<PuyoScript> GetPuyosGroup(PuyoScript _puyo, int _x, int _y)
    {
        List<PuyoScript> connectedPuyos = new List<PuyoScript>();
        if(!checkedGrid[_x, _y])
        {
            connectedPuyos.Add(_puyo);
            checkedGrid[_x, _y] = true;
        }

        if(_x < gridLimitX)
        {
            _puyo.rightConnected = CheckNeighbor(connectedPuyos, _puyo.color, _x + 1, _y);
        }
        if(_x > 0)
        {
            _puyo.leftConnected = CheckNeighbor(connectedPuyos, _puyo.color, _x - 1, _y);
        }
        if(_y < gridLimitY)
        {
            _puyo.upperConnected = CheckNeighbor(connectedPuyos, _puyo.color, _x, _y + 1);
        }
        if(_y > 0)
        {
            _puyo.bottomConnected = CheckNeighbor(connectedPuyos, _puyo.color, _x, _y - 1);
        }
        
        return connectedPuyos;
    }

    bool CheckNeighbor(List<PuyoScript> _connectedPuyos , int _color, int _x, int _y)
    {
        if(!checkedGrid[_x, _y] && gridColors[_x, _y] == _color) //For actual grid logic
        {
            _connectedPuyos.Add(grid[_x, _y].GetComponent<PuyoScript>());
            checkedGrid[_x, _y] = true;
        }

        if(gridColors[_x, _y] == _color) //For UpdateSprite functionality
        {
            return true;
        }
        return false;
    }

    IEnumerator ChainCompleted(List<PuyoScript> _chain)
    {
        yield return new WaitForSeconds(0.1f);
        foreach(PuyoScript puyo in _chain)
        {
            grid[puyo.x, puyo.y] = null;
            gridColors[puyo.x, puyo.y] = -1;
            checkedGrid[puyo.x, puyo.y] = false;

            StartCoroutine(puyo.Pop());
        }
        comboCount++;
        Debug.Log("Combo count: " + comboCount);
        gameController.AddScore(_chain.Count);
        if(comboCount > 1)
        {
            gameController.ShowCurrentCombo(comboCount);
        }
    }

    IEnumerator AdjustFloatingPuyos()
    {
        yield return new WaitForSeconds(0.5f); //Wait for chain reaction
        var floatingPuyos = new List<Transform>();
        var puyosToRemove = new List<Transform>();

        bool puyosFloating = false;
        bool puyosFalled = false;

        do
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 1; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] != null && grid[i, j - 1] == null) //There is a void in there
                    {
                        puyosFloating = true;
                        puyosFalled = true;
                        //Get entire column to drop
                        for (int z = 1; z < grid.GetLength(1); z++)
                        {
                            if (grid[i, z] != null)
                            {
                                grid[i, z].GetComponent<PuyoScript>().isFalling = true;
                                floatingPuyos.Add(grid[i, z]);
                                RemovePuyoFromGrid(grid[i, z]);
                            }
                        }
                    }
                }
            }

            //Debug.Log("Floating puyos: " + floatingPuyos.Count);
            foreach (Transform puyo in floatingPuyos)
            {
                Vector3 newPos = puyo.position;
                newPos.y -= 1;
                puyo.transform.position = newPos;

                if (BoundsOrCollision(puyo.transform.position))
                {
                    newPos = puyo.position;
                    newPos.y += 1;
                    puyo.position = newPos;

                    AddPuyoToGrid(puyo);
                    puyosToRemove.Add(puyo);
                }
            }
            for(int i = 0; i < puyosToRemove.Count; i++)
            {
                floatingPuyos.Remove(puyosToRemove[i]);
            }
            //Debug.Log("Floating puyos: " + floatingPuyos.Count + ", Puyos not floating: " + puyosToRemove.Count);
            if (floatingPuyos.Count == 0)
            {
                puyosFloating = false;
            }
            yield return new WaitForSeconds(fallingBlocksRate);
        } while (puyosFloating);

        if(puyosFalled)
        {
            SearchForConnections(); //Search if the adjustment caused any more chains
        }
        else
        {
            if (comboCount > 1)
            {
                StartCoroutine(gameController.AddChainComboScore(comboCount));
            }
            if(!CheckForGameOver())
            {
                Invoke("SpawnPuyos", 0.2f);
            }
        }
    }

    bool CheckForGameOver()
    {
        for (int i = 0; i < checkedGrid.GetLength(0); i++)
        {
            if(grid[i, 11] != null) //There is a puyo on top of the board
            {
                gameController.GameOver();
                return true;
            }
        }
        return false;
    }
}
