using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumScript;
public class BattlefieldScript : MonoBehaviour
{

    public static BattlefieldScript Instance;
    [SerializeField]
    GameObject bTile;
    [SerializeField]
    public List<GameObject> battleTiles;
    [SerializeField]
    public GameObject[,] battleTilesGrid;
    [SerializeField]//X length of the field
    public int xMax;
    [SerializeField]//Y length of the field
    public int yMax;
    [SerializeField]
    public float distanceModifier;
    [SerializeField]
    public Vector2 playerSpawn;
    [SerializeField]
    public Vector2 playerPosition;
    [SerializeField]
    TileColour t;
    [SerializeField]
    List<Color> standardColours;
    
    [Header("Tile Alignement")]
    public List<GameObject> playerTiles = new List<GameObject>();
    public List<GameObject> neutralTiles = new List<GameObject>();
    public List<GameObject> enemyTiles = new List<GameObject>();
    

    private int playerColumns;
    public int PlayerColumns { get => playerColumns; set => playerColumns = value; }
    private int enemyColumns;
    public int EnemyColumns { get => enemyColumns; set => enemyColumns = value; }


    /*private void OnValidate()
    {
        int _tileID = 0;
        for (int x = 0; x < xMax; x++)
        {
            for (int y = 0; y < yMax; y++)
            {
                TileColour(_tileID);
                _tileID++;
            }
        }
    }*/
    
    public void SpawnTiles()
    {
        if (battleTiles.Count <= 0)
        {
            InstanciateBattlefield();
        }
        else
        {
            ClearBattlefield();
            InstanciateBattlefield();
        }
    }

    private void Awake()
    {
        Instance = this;
        battleTilesGrid = new GameObject[xMax, yMax];
        int _tileID = 0;
        for (int x = 0; x < xMax; x++)
        {
            for (int y = 0; y < yMax; y++)
            {
                battleTilesGrid[x, y] = battleTiles[_tileID];
                
                _tileID++;
            }
        }
        AddNeighbours();
    }

    void InstanciateBattlefield()
    {
        
        int _tileID = 0;
        for (int x = 0; x < xMax; x++)
        {
            for (int y = 0; y < yMax; y++)
            {
                battleTiles.Add(Instantiate(bTile, new Vector3(x*distanceModifier, y*distanceModifier, 0), Quaternion.Euler(0,0,0), gameObject.transform));
                battleTiles[_tileID].name = "BattleTile #" + _tileID + ": " + x + "x || " + y + "y";
                battleTiles[_tileID].GetComponent<TileClass>().tileID = _tileID;
                battleTiles[_tileID].GetComponent<TileClass>().gridLocation = new Vector2(x, y);
                TileColour(_tileID);
                
                _tileID++;
            }
        }
        
    }
    public void ClearBattlefield()
    {
        foreach (GameObject bTile in battleTiles.ToArray())
        {
            battleTiles.Remove(bTile);
            DestroyImmediate(bTile);
        }
        playerTiles.Clear();
        neutralTiles.Clear();
        enemyTiles.Clear();
    }
    void TileColour(int _tileID = 0)
    {
        
        if (t == EnumScript.TileColour.Standard)
        {
            int _playerColumnID = (playerColumns + 1) * yMax;
            int _enemyColumnID = ((enemyColumns + 2) * yMax)-1;
            if (_tileID < _playerColumnID)
            {
                //battleTiles[currentTile].GetComponent<Renderer>().material.color = Color.cyan;
                SetTileAlignment(playerTiles, 0, TileAlignment.Friendly, _tileID);
                

            }
            else if (_tileID >= _playerColumnID && _tileID <= _enemyColumnID)
            {
                SetTileAlignment(neutralTiles, 1, TileAlignment.Neutral, _tileID);
                
            }
            else if (_tileID > _enemyColumnID)
            {
                //battleTiles[currentTile].GetComponent<Renderer>().material.color = Color.magenta;
                SetTileAlignment(enemyTiles, 2, TileAlignment.Enemy, _tileID);
                
            }
        }
    }
    void SetTileAlignment(List<GameObject> _alignmentList, int _standardColoursID, TileAlignment _tileAlignment, int _tileID )
    {
        battleTiles[_tileID].GetComponent<TileClass>().SetColour(standardColours[_standardColoursID], true);
        battleTiles[_tileID].GetComponent<TileClass>().tAlign = _tileAlignment;
        battleTiles[_tileID].GetComponent<TileClass>().SetVariables();
        _alignmentList.Add(battleTiles[_tileID]);
    }
    void AddNeighbours()
    {
        foreach(GameObject tile in battleTilesGrid)
        {
            TileClass _tileClass = tile.GetComponent<TileClass>(); 
            int _currentDirection = 1;
            for(int y = -1; y < 2; y++)
            {
                for(int x = -1; x < 2; x++)
                {
                    //if (x != 0 && y != 0)
                    //{
                        int _tempX = (int)_tileClass.gridLocation.x + x;
                        int _tempY = (int)_tileClass.gridLocation.y + y;
                        if (_tempX > -1 && _tempX < xMax && _tempY > -1 && _tempY < yMax)
                        {
                            switch (_currentDirection)
                            {
                                case 1:
                                    {
                                        tile.GetComponent<TileClass>().tileNeighbours.Add(TileNeighbourDirection.DownLeft, battleTilesGrid[_tempX, _tempY]);
                                        break;
                                    }
                                case 2:
                                    {
                                        tile.GetComponent<TileClass>().tileNeighbours.Add(TileNeighbourDirection.Down, battleTilesGrid[_tempX, _tempY]);
                                        break;
                                    }
                                case 3:
                                    {
                                        tile.GetComponent<TileClass>().tileNeighbours.Add(TileNeighbourDirection.DownRight, battleTilesGrid[_tempX, _tempY]);
                                        break;
                                    }
                                case 4:
                                    {
                                        tile.GetComponent<TileClass>().tileNeighbours.Add(TileNeighbourDirection.Left, battleTilesGrid[_tempX, _tempY]);
                                        break;
                                    }
                                case 5:
                                    {
                                        //Debug.Log("That shouldn't be possible, you can't be a neighbour of yourself!");
                                        break;
                                    }
                                case 6:
                                    {
                                        tile.GetComponent<TileClass>().tileNeighbours.Add(TileNeighbourDirection.Right, battleTilesGrid[_tempX, _tempY]);
                                        break;
                                    }
                                case 7:
                                    {
                                        tile.GetComponent<TileClass>().tileNeighbours.Add(TileNeighbourDirection.UpLeft, battleTilesGrid[_tempX, _tempY]);
                                        break;
                                    }
                                case 8:
                                    {
                                        tile.GetComponent<TileClass>().tileNeighbours.Add(TileNeighbourDirection.Up, battleTilesGrid[_tempX, _tempY]);
                                        break;
                                    }
                                case 9:
                                    {
                                        tile.GetComponent<TileClass>().tileNeighbours.Add(TileNeighbourDirection.UpRight, battleTilesGrid[_tempX, _tempY]);
                                        break;
                                    }
                            }
                            _currentDirection++;
                            
                        //}
                        //else
                        //{
                            //_currentDirection++;
                        //}
                    }
                }
            }
        }
    }
}
