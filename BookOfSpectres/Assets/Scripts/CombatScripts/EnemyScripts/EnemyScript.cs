using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumScript;
public class EnemyScript : MonoBehaviour
{
    ObjectPooler objectPooler;
    [SerializeField]
    public GameObject currentTile;
    [SerializeField]
    public GameObject previousTile;
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    Transform shotLocation;

    public float movmentSpeed;

    TileClass currentTileClass;
    [SerializeField]
    BattlefieldScript bfs;
    [SerializeField]
    Color enemyTileColour;
    [SerializeField]
    List<TileAlignment> alignedTiles;
    [SerializeField]
    Animator anim;

    EntityStatus status;

    public Vector2 currentGridPosition;

    private GameObject previousRaycastTile;
    private TileClass previousRaycastTileClass;
    private string tileTag = "Tile";
    private Ray ray;
    private RaycastHit hit;
    SpriteRenderer enemySprite;
    bool coroutineIsOn = false;
    bool isPaused = false;
    bool isRaycasting = true;
    // Start is called before the first frame update
    void Start()
    {
        enemySprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        objectPooler = ObjectPooler.Instance;
        bfs = BattlefieldScript.Instance;
        status = GetComponent<EntityStatus>();
        ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit, 10f))
        {
            Debug.DrawRay(transform.position, -transform.up);

            previousRaycastTile = hit.transform.gameObject;
            previousRaycastTileClass = previousRaycastTile.GetComponent<TileClass>();
            previousRaycastTileClass.SetColour(enemyTileColour);
            currentTile = hit.transform.gameObject;
        }

        
        currentTileClass = currentTile.GetComponent<TileClass>();

        currentGridPosition = new Vector2(currentTileClass.gridLocation.x, currentTileClass.gridLocation.y);

        enemySprite.sortingOrder = -(int)currentTileClass.gridLocation.y + 5;

        currentTileClass.occupied = true;

    }

    // Update is called once per frame
    void Update()
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
                    previousRaycastTileClass = previousRaycastTile.GetComponent<TileClass>();
                    previousRaycastTileClass.SetColour(enemyTileColour);
                }
                else
                {
                    previousRaycastTileClass.SetColour(previousRaycastTileClass.initialMaterialColour);
                    previousRaycastTile = hit.transform.gameObject;
                    previousRaycastTileClass = previousRaycastTile.GetComponent<TileClass>();
                    previousRaycastTileClass.SetColour(enemyTileColour);
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
        previousTile = currentTile;

        currentTile = bfs.battleTilesGrid[(int)currentTileClass.gridLocation.x + x, (int)currentTileClass.gridLocation.y + y];
        currentTileClass = currentTile.GetComponent<TileClass>();
        currentGridPosition = currentTileClass.gridLocation;

    }

    public IEnumerator LerpMovement(float time)
    {
        bool _occupied = false;
        if (coroutineIsOn == false)
        {
            coroutineIsOn = true;
            float _elapsedTime = 0f;

            //Set which tile the entity is on before it moves, so that it won't clip into another entity
            previousTile.GetComponent<TileClass>().occupied = false;
            currentTileClass.occupied = true;

            Vector3 pT = new Vector3(previousTile.transform.position.x, previousTile.transform.position.y, -1.4f);
            Vector3 cT = new Vector3(currentTile.transform.position.x, currentTile.transform.position.y, -1.4f);

            while (_elapsedTime <= time)
            {
                if (isPaused == false)
                {
                    transform.position = Vector3.Lerp(pT, cT, (_elapsedTime / time));
                    _elapsedTime += Time.deltaTime;
                }
                yield return new WaitForEndOfFrame();
            }
            transform.position = cT;
            coroutineIsOn = false;
        }

        
        //For continuous movement, resetting the delay
        anim.SetBool("isMoving", false);
        enemySprite.sortingOrder = -(int)currentTileClass.gridLocation.y + 5;

        yield return new WaitForSeconds(0);
    }
    public void Shoot()
    {
        ProjectileScript _pScript = objectPooler.SpawnFromPool(projectile.name, shotLocation.position, Quaternion.Euler(0, 0, 90), gameObject.transform).GetComponent<ProjectileScript>();
        if (status.directionFacing == Facing.Left)
        {
            _pScript.speed = System.Math.Abs(_pScript.speed) * -1;
        }
        else if(status.directionFacing == Facing.Right)
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

    public void Paused()
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
    }

    public bool TileCheck(int movementRangeX = 0, int movementRangeY = 0)
    {
        foreach (TileAlignment aligned in alignedTiles)
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
}
