using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumScript;
using UnityEngine.EventSystems;
using DG.Tweening;
public class VerticalMove : CombatMove
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
    Color entityTileColour;
    [SerializeField]
    List<TileAlignment> alignedTiles;

    private Vector2 currentGridPosition;

    private GameObject previousRaycastTile;
    //private TileClass previousRaycastTileClass;
    private GameObject currentRaycastTile;
    //private TileClass currentRaycastTileClass;
    private string tileTag = "Tile";
    private Ray ray;
    private RaycastHit hit;
    private Vector2 lastMove;

    private bool bufferedMove;
    private bool instantMove = true;

    EntityScript entity;
    EntityStatus status;

    public int MovementRange { get => movementRange; set => movementRange = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public float HeightAboveGround { get => heightAboveGround; set => heightAboveGround = value; }
    public List<TileAlignment> AlignedTiles { get => alignedTiles; set => alignedTiles = value; }
    public Color EntityTileColour { get => entityTileColour; set => entityTileColour = value; }
    public GameObject CurrentTile { get => currentTile; set => currentTile = value; }
    public GameObject PreviousTile { get => previousTile; set => previousTile = value; }
    public TileClass PreviousRaycastTileClass { get => previousRaycastTile.GetComponent<TileClass>();}
    public TileClass CurrentRaycastTileClass { get => currentRaycastTile.GetComponent<TileClass>(); }
    public Vector2 CurrentGridPosition { get => currentGridPosition; set => currentGridPosition = value; }

    EntityInputManager inputManager;

    Sequence moveSequence;

    private void Start()
    {
        entity = GetComponent<EntityScript>();
        movementSpeed = entity.movementSpeed;
        movementRange = entity.movementRange;


        inputManager = this.GetComponent<EntityInputManager>();
        status = GetComponent<EntityStatus>();

        bfs = BattlefieldScript.Instance;

        ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit, 10f))
        {
            Debug.DrawRay(transform.position, -transform.up);

            previousRaycastTile = hit.transform.gameObject;
            

            PreviousRaycastTileClass.SetColour(entity.EntityTileColour);
            currentTile = hit.transform.gameObject;

            entity.currentTile = currentTile;
        }


        currentTileClass = currentTile.GetComponent<TileClass>();

        CurrentGridPosition = new Vector2(currentTileClass.gridLocation.x, currentTileClass.gridLocation.y);
        entity.currentGridPosition = CurrentGridPosition;


        currentTileClass.occupied = true;

        previousRaycastTile = currentRaycastTile;



        transform.position = CombatCalculations.EntityPostition(currentTile, heightAboveGround);

        SetSortingOrder(-(int)currentTileClass.gridLocation.y + 5);
    }

    public override void Move(int x = 0, int y = 0)
    {
        Vector2 _movementInput = inputManager.movementVector;
        //Vector2 _movementInput = new Vector2(x,y);

        //if (x != 0 && y != 0)
        //{

            if (entity.IsMoving == true)
            {
                MoveEntity(_movementInput);
                //lastMove = _movementInput;
                //bufferedMove = true;
            }
            else
            {
                
                //MoveEntity(_movementInput);
            }

        //}
       
    }

    private void MoveEntity(Vector2 direction)
    {
        
        if (entity.IsMoving)
        {
            
            if (instantMove == true)
            {

                entity.IsMoving = true;
                ZeroOutTheDelays();
                UpdateBattlefield((int)direction.x * movementRange, (int)direction.y * movementRange);
                //MoveEvent?.Invoke(moveDir);
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
            entity.EntitySprite.sortingOrder = i;
            foreach (ParticleSystemRenderer p in entity.EntitySprite.transform.GetComponentsInChildren<ParticleSystemRenderer>())
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

    }

    void SetTileInfo(int x, int y)
    {
        currentTile = bfs.battleTilesGrid[(int)currentTileClass.gridLocation.x + x, (int)currentTileClass.gridLocation.y + y];
        currentTileClass = currentTile.GetComponent<TileClass>();
        entity.currentGridPosition = currentTileClass.gridLocation;
        //currentTileClass.DebugCurrentTile();
    }

    public override void UpdateEntity()
    { 
        TweenEntity();
    }

    void TweenEntity()
    {
        float _time = movementSpeed;

        Vector3 _previousTilePos = CombatCalculations.EntityPostition(previousTile, heightAboveGround);
        Vector3 _currentTilePos = CombatCalculations.EntityPostition(currentTile, heightAboveGround);

        moveSequence.Join(DOTween.To(() => transform.position, x => transform.position = x, _currentTilePos, _time)
            .SetEase(Ease.OutBack)
            .OnStart(() => entity.MoveStarted())
            .OnComplete(() => { entity.MoveEnded(); }));


    }

    private void OnDisable()
    {
        if (currentRaycastTile != null)
        {
            currentRaycastTile.GetComponent<TileClass>().UnOccupy(true);
            previousTile.GetComponent<TileClass>().UnOccupy(true);
        }
    }

}
