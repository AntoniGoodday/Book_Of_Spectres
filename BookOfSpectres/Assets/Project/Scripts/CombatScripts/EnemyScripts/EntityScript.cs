using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumScript;
using DG.Tweening;
public class EntityScript : MonoBehaviour
{
    ObjectPooler objectPooler;
    [SerializeReference]
    public CombatMove move;

    [SerializeField]
    public GameObject currentTile;
    [SerializeField]
    public GameObject previousTile;
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    Transform shotLocation;

    public float movementSpeed;
    public int movementRange;

    public TileClass currentTileClass;
    [SerializeField]
    BattlefieldScript bfs;
    [SerializeField]
    Color entityTileColour;
    [SerializeField]
    List<TileAlignment> alignedTiles;
    [SerializeField]
    public Animator anim;

    EntityStatus status;

    public Vector2 currentGridPosition;

    private GameObject previousRaycastTile;
    private GameObject currentRaycastTile;
    private TileClass previousRaycastTileClass;
    private string tileTag = "Tile";
    private Ray ray;
    private RaycastHit hit;
    SpriteRenderer entitySprite;
    bool coroutineIsOn = false;
    //bool isPaused = false;
    bool isRaycasting = true;

    float actionCooldown = 0.5f;
    bool isMoving = false;
    float interruptedAnimationTime = 0;
    [SerializeField]
    bool isInterrupted = false;
    [SerializeField]
    bool canBeCountered = false;
    [SerializeField]
    bool animationEnd = false;
    [SerializeField]
    SpriteRenderer visuals;

    public float ActionCooldown { get => actionCooldown; set => actionCooldown = value; }
    public bool IsMoving { get => isMoving; set => isMoving = value; }
    public float InterruptedAnimationTime { get => interruptedAnimationTime; set => interruptedAnimationTime = value; }
    public bool IsInterrupted { get => isInterrupted; set => isInterrupted = value; }
    public bool CanBeCountered { get => canBeCountered; set => canBeCountered = value; }
    public bool AnimationEnd { get => animationEnd; set => animationEnd = value; }
    public EntityStatus Status { get => status; set => status = value; }
    public SpriteRenderer EntitySprite { get => entitySprite; set => entitySprite = value; }
    public Color EntityTileColour { get => entityTileColour; set => entityTileColour = value; }
    public ObjectPooler ObjectPooler { get => objectPooler; set => objectPooler = value; }
    public TileClass PreviousTileClass { get => previousTile.GetComponent<TileClass>(); }
    public TileClass CurrentTileClass { get => currentTile.GetComponent<TileClass>(); }


    // Start is called before the first frame update
    public virtual void Awake()
    {
        EntitySprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        ObjectPooler = ObjectPooler.Instance;
        bfs = BattlefieldScript.Instance;
        Status = GetComponent<EntityStatus>();

        if (move == null)
        {
            move = GetComponent<CombatMove>();
        }
    }

    void Start()
    {
        
        /*ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit, 10f))
        {
            Debug.DrawRay(transform.position, -transform.up);

            previousRaycastTile = hit.transform.gameObject;
            previousRaycastTileClass = previousRaycastTile.GetComponent<TileClass>();
            previousRaycastTileClass.SetColour(EntityTileColour);
            currentTile = hit.transform.gameObject;
        }

        
        currentTileClass = currentTile.GetComponent<TileClass>();

        currentGridPosition = new Vector2(currentTileClass.gridLocation.x, currentTileClass.gridLocation.y);

        EntitySprite.sortingOrder = -(int)currentTileClass.gridLocation.y + 5;

        currentTileClass.occupied = true;

        previousRaycastTile = currentRaycastTile;*/

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isRaycasting == true)
        {
            ray = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(ray, out hit, 10f))
            {
                Debug.DrawRay(transform.position, -transform.up);
                if (previousRaycastTile == null)
                {
                    previousRaycastTile = hit.transform.gameObject;
                    currentRaycastTile = previousRaycastTile;
                    previousRaycastTileClass = previousRaycastTile.GetComponent<TileClass>();
                    previousRaycastTileClass.SetColour(EntityTileColour);
                }
                else
                {
                    if (hit.transform.gameObject != currentRaycastTile || currentRaycastTile.GetComponent<TileClass>().currentColour != EntityTileColour)
                    {
                        
                        previousRaycastTileClass.SetColour(previousRaycastTileClass.initialMaterialColour, false, false, true);
                        
                        

                        currentRaycastTile = hit.transform.gameObject;
                        currentRaycastTile.GetComponent<TileClass>().SetColour(EntityTileColour);
                        currentGridPosition = currentRaycastTile.GetComponent<TileClass>().gridLocation;


                        previousRaycastTile = currentRaycastTile;
                        previousRaycastTileClass = previousRaycastTile.GetComponent<TileClass>();
                    }

                    
                }

            }
            else
            {
                if (previousRaycastTile != null)
                {
                    previousRaycastTileClass.SetColour(previousRaycastTileClass.initialMaterialColour);
                    previousRaycastTile = null;
                }
            }
        }
    }

    public void SetTileInfo(int x, int y)
    {
        /*previousTile = currentTile;

        currentTile = bfs.battleTilesGrid[(int)currentTileClass.gridLocation.x + x, (int)currentTileClass.gridLocation.y + y];
        currentTileClass = currentTile.GetComponent<TileClass>();
        currentGridPosition = currentTileClass.gridLocation;*/

    }

    public void Shoot()
    {
        

        ProjectileScript _pScript = ObjectPooler.SpawnFromPool(projectile.name, shotLocation.position, Quaternion.Euler(0, 0, 90), gameObject.transform).GetComponent<ProjectileScript>();
        if (Status.directionFacing == Facing.Left)
        {
            _pScript.speed = System.Math.Abs(_pScript.speed) * -1;
        }
        else if(Status.directionFacing == Facing.Right)
        {
            _pScript.speed = System.Math.Abs(_pScript.speed);
        }
        
    }

    public void SetShooting()
    {
        anim.SetBool("isShooting", false);
    }

    public void ClearTileColour()
    {
        isRaycasting = false;
        previousRaycastTileClass.SetColour(previousRaycastTileClass.initialMaterialColour);
    }

    /*public void Paused()
    {
        anim.enabled = false;
        ParticleSystem _pSys = status.hitParticles;
        if (_pSys.isPlaying)
        {
            _pSys.Pause();
        }
        isPaused = true;
    }
    public void UnPaused()
    {
        
        anim.enabled = true;
        ParticleSystem _pSys = status.hitParticles;
        if (_pSys.isPaused)
        {
            _pSys.Play();
        }
        isPaused = false;
    }*/

    public bool TileCheck(int movementRangeX = 0, int movementRangeY = 0)
    {
        foreach (TileAlignment aligned in alignedTiles)
        {
            int _tileX = (int)CurrentTileClass.gridLocation.x + movementRangeX;
            int _tileY = (int)CurrentTileClass.gridLocation.y + movementRangeY;

            if(!bfs)
            {
                bfs = BattlefieldScript.Instance;
            }

            TileClass _tileClass = bfs.battleTilesGrid[_tileX, _tileY].GetComponent<TileClass>();

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

    private void OnDisable()
    {
        if (currentRaycastTile != null)
        {
            currentRaycastTile.GetComponent<TileClass>().UnOccupy(true);
            previousTile.GetComponent<TileClass>().UnOccupy(true);
        }
    }

    public void Counterable()
    {
        canBeCountered = true;
        anim.Play("Counterable",1);
    }

    public void EndCounter()
    {
        anim.Play("DefaultState", 1);
        canBeCountered = false;
    }

    public void EndAnimation()
    {
        animationEnd = true;
    }

    public void CounterFlashes()
    {
        var tweener = DOTween.To(() => transform.position, x => transform.position = x, transform.position, 3)
            .OnStart(() => { anim.SetFloat("AttackSpeed", 0f); anim.Play("CounterState", 1); })
            .OnComplete(() => { anim.SetFloat("AttackSpeed", 1f); GetComponent<EnemyAI>().isInCounterState = false; IsInterrupted = false; anim.Play("DefaultState", 1); });
    }

    public void MoveStarted()
    {
        isMoving = true;
    }

    public void MoveEnded()
    {
        isMoving = false;
        
    }

}
