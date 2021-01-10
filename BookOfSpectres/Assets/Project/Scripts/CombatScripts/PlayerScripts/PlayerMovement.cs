using System.Collections;
using System.Collections.Generic;
using EnumScript;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class PlayerMovement : CombatMove
{
    public delegate void MoveDelegate(MoveDirection direction);
    public event MoveDelegate MoveEvent;

    [SerializeField]
    int movementRange;
    [SerializeField]
    float movementSpeed;
    [SerializeField]
    float heightAboveGround = -1.25f;

    [SerializeField]
    GameObject currentTile;
    [SerializeField]
    GameObject previousTile;

    TileClass currentTileClass;
    [SerializeField]
    BattlefieldScript bfs;

    [SerializeField]
    Color playerTileColour;
    [SerializeField]
    List<TileAlignment> alignedTiles;

    private GameObject previousRaycastTile;
    private GameObject currentRaycastTile;
    private string tileTag = "Tile";
    private Ray ray;
    private RaycastHit hit;
    private Vector2 lastMove;

    private bool bufferedMove;
    private bool instantMove = true;

    PlayerScript playerScript;
    PlayerStatus status;

    public int MovementRange { get => movementRange; set => movementRange = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public float HeightAboveGround { get => heightAboveGround; set => heightAboveGround = value; }
    public List<TileAlignment> AlignedTiles { get => alignedTiles; set => alignedTiles = value; }
    public Color PlayerTileColour { get => playerTileColour; set => playerTileColour = value; }
    public GameObject CurrentTile { get => currentTile; set => currentTile = value; }
    public GameObject PreviousTile { get => previousTile; set => previousTile = value; }

    EntityInputManager inputManager;

    Sequence moveSequence;

    private void Start()
    {
        playerScript = GetComponent<PlayerScript>();
        inputManager = this.GetComponent<EntityInputManager>();
        status = GetComponent<PlayerStatus>();

        bfs = BattlefieldScript.Instance;

        currentTile = bfs.battleTilesGrid[(int)bfs.playerSpawn.x, (int)bfs.playerSpawn.y];
        currentTileClass = currentTile.GetComponent<TileClass>();

        transform.position = CombatCalculations.EntityPostition(currentTile,heightAboveGround);
        bfs.playerPosition = new Vector2Int((int)currentTileClass.gridLocation.x, (int)currentTileClass.gridLocation.y);
        previousTile = currentTile;

        currentTileClass.occupied = true;

        SetSortingOrder(-(int)currentTileClass.gridLocation.y + 5);
    }

    private void FixedUpdate()
    {
        //Raycast downwards
        ray = new Ray(transform.position, transform.forward);

        RayCheck(ray);
    }

   
    public override void Move(int moveX = 0, int moveY = 0)
    {
        Vector2 _movementInput = inputManager.movementVector;

        if (_movementInput.x != 0 || _movementInput.y != 0)
        {
            if (playerScript.IsLerping == true)
            {
                //lastMove = _movementInput;
                //bufferedMove = true;
            }
            else
            {
                MovePlayer(_movementInput);
            }

        }
    }

    private void MovePlayer(Vector2 direction)
    {
        if (!playerScript.IsLerping)
        {
            if (instantMove == true)
            {
                int _x = 0;
                int _y = 0;

                MoveDirection moveDir = MoveDirection.None;

                if (direction.x > 0)
                {
                    if ((int)currentTileClass.gridLocation.x + 1 < bfs.xMax && TileCheck(movementRange, 0))
                    {
                        moveDir = MoveDirection.Right;
                    }
                    else
                    {
                        return;
                    }
                }
                else if (direction.x < 0)
                {
                    if ((int)currentTileClass.gridLocation.x - 1 >= 0 && TileCheck(-movementRange, 0))
                    {
                        moveDir = MoveDirection.Left;
                    }
                    else
                    {
                        return;
                    }
                }
                if (direction.y > 0)
                {
                    if ((int)currentTileClass.gridLocation.y + movementRange < bfs.yMax && TileCheck(0, movementRange))
                    {
                        moveDir = MoveDirection.Up;
                    }
                    else
                    {
                        return;
                    }
                }
                else if (direction.y < 0)
                {
                    if ((int)currentTileClass.gridLocation.y - movementRange >= 0 && TileCheck(0, -movementRange))
                    {
                        moveDir = MoveDirection.Down;
                    }
                    else
                    {
                        return;
                    }
                }

                switch (moveDir)
                {
                    case (MoveDirection.Up):
                        {
                            //vertAxisInUse = true;
                            _x = 0;
                            _y = 1;
                            break;
                        }
                    case (MoveDirection.Down):
                        {
                            //vertAxisInUse = true;
                            _x = 0;
                            _y = -1;
                            break;
                        }
                    case (MoveDirection.Left):
                        {
                            //horizontalAxisInUse = true;
                            _x = -1;
                            _y = 0;
                            break;
                        }
                    case (MoveDirection.Right):
                        {
                            //horizontalAxisInUse = true;
                            _x = 1;
                            _y = 0;
                            break;
                        }
                    default:
                        {
                            return;
                        }
                }



                playerScript.IsLerping = true;
                ZeroOutTheDelays();
                UpdateBattlefield(_x * movementRange, _y * movementRange);
                MoveEvent?.Invoke(moveDir);
                UpdateEntity();
            }
        }

        if (instantMove == true)
        {
            //instantMove = false;
        }
    }

    bool TileCheck(int movementRangeX = 0, int movementRangeY = 0)
    {
        foreach (TileAlignment aligned in AlignedTiles)
        {
            TileClass _tileClass = bfs.battleTilesGrid[(int)currentTileClass.gridLocation.x + movementRangeX, (int)currentTileClass.gridLocation.y + movementRangeY].GetComponent<TileClass>();

            if (_tileClass.tileAlignment == aligned)
            {
                if (_tileClass.occupied == false)
                {
                    if (_tileClass.tileEffect != TileEffect.Broken)
                    {
                        return true;
                    }

                }
            }

        }
        return false;
    }

    public override void SetSortingOrder(int i)
    {
        if (status.IsDying == false)
        {
            playerScript.playerSprite.sortingOrder = i;
            foreach(ParticleSystemRenderer p in playerScript.playerSprite.transform.GetComponentsInChildren<ParticleSystemRenderer>())
            {
                p.sortingOrder = i + 1;
            }
        }
    }

    void ZeroOutTheDelays()
    {
        //tempMovementDelayUp = 0;
        //tempMovementDelayDown = 0;
        //tempMovementDelayLeft = 0;
        //tempMovementDelayRight = 0;
    }

    public override void UpdateBattlefield(int x = 0, int y = 0)
    {

        //currentTileClass.SetColour(currentTileClass.initialMaterialColour);
        previousTile = currentTile;
        SetTileInfo(x, y);
        bfs.playerPosition = new Vector2Int((int)currentTileClass.gridLocation.x, (int)currentTileClass.gridLocation.y);
    }

    void SetTileInfo(int x, int y)
    {
        currentTile = bfs.battleTilesGrid[(int)currentTileClass.gridLocation.x + x, (int)currentTileClass.gridLocation.y + y];
        currentTileClass = currentTile.GetComponent<TileClass>();
        //currentTileClass.DebugCurrentTile();
    }

    public override void UpdateEntity() => TweenPlayer();


    IEnumerator LerpPlayer(float time)
    {
        playerScript.IsLerping = true;

        float _elapsedTime = 0f;

        //Set which tile the entity is on before it moves, so that it won't clip into another entity
        previousTile.GetComponent<TileClass>().occupied = false;


        currentTileClass.occupied = true;


        Vector3 _previousTilePos = CombatCalculations.EntityPostition(previousTile, heightAboveGround);
        Vector3 _currentTilePos = CombatCalculations.EntityPostition(currentTile, heightAboveGround);

        while (_elapsedTime <= time)
        {
            if (status.IsPaused == false)
            {
                transform.position = Vector3.Lerp(_previousTilePos, _currentTilePos, (_elapsedTime / time));
                _elapsedTime += Time.deltaTime;

            }
            yield return new WaitForEndOfFrame();
        }
        transform.position = _currentTilePos;

        SetSortingOrder(-(int)currentTileClass.gridLocation.y + 5);


        //For continuous movement, resetting the delay
        playerScript.IsLerping = false;

        ///BUFFER MOVE
        if (bufferedMove == true)
        {
            MovePlayer(lastMove);
            bufferedMove = false;
        }


        yield return new WaitForSeconds(0);
    }

    

    void TweenPlayer()
    {
        float _time = movementSpeed;

        Vector3 _previousTilePos = CombatCalculations.EntityPostition(previousTile, heightAboveGround);
        Vector3 _currentTilePos = CombatCalculations.EntityPostition(currentTile, heightAboveGround);

        moveSequence.Join(DOTween.To(() => transform.position, x => transform.position = x, _currentTilePos, _time)
            .SetEase(Ease.OutBack)
            .OnStart(() => playerScript.IsLerping = true)
            .OnComplete(() => { playerScript.IsLerping = false; playerScript.EndMove(); }));
   
        
    }

    public void RayCheck(Ray ray)
    {
        if (Physics.Raycast(ray, out hit, 10f))
        {
            Debug.DrawRay(transform.position, -transform.up);
            if (previousRaycastTile == null)
            {
                previousRaycastTile = hit.transform.gameObject;
                currentRaycastTile = hit.transform.gameObject;

                TileClass _previousRaycastTileClass = previousRaycastTile.GetComponent<TileClass>();
                _previousRaycastTileClass.SetColour(PlayerTileColour);
            }
            else
            {
                if (hit.transform.gameObject != currentRaycastTile | currentRaycastTile.GetComponent<TileClass>().currentColour != PlayerTileColour)
                {

                    previousRaycastTile.GetComponent<TileClass>().SetColour(previousRaycastTile.GetComponent<TileClass>().initialMaterialColour, false, false, true);

                    currentRaycastTile = hit.transform.gameObject;
                    currentRaycastTile.GetComponent<TileClass>().SetColour(PlayerTileColour);
                    previousRaycastTile = currentRaycastTile;

                    SetSortingOrder(-(int)currentTileClass.gridLocation.y + 5);
                }
            }

        }
        else
        {
            if (previousRaycastTile != null)
            {
                previousRaycastTile.GetComponent<TileClass>().SetColour(previousRaycastTile.GetComponent<TileClass>().initialMaterialColour);
                previousRaycastTile = null;
            }
        }
    }

    
}
