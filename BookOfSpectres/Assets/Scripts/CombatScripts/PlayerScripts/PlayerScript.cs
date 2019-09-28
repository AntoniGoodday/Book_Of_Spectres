using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumScript;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PlayerScript : MonoBehaviour
{
    public static PlayerScript Instance;
    ObjectPooler objectPooler;
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
    int movementRange;
    [SerializeField]
    List<TileAlignment> alignedTiles;
    [SerializeField]
    float movementSpeed;
    [SerializeField]
    float maxShotChargeTime = 2;
    [SerializeField]
    float shotChargeAmount = 0;
    [SerializeField]
    bool shotFullyCharged = false;
    [SerializeField]
    List<ParticleSystem> chargeParticles;
    [SerializeField]
    float heightAboveGround = -1.25f;

    bool isLerping = false;
    public bool isPaused = false;
    [SerializeField]
    float contMovementDelay = 0.5f;
    float tempMovementDelayUp = 0;
    float tempMovementDelayDown = 0;
    float tempMovementDelayLeft = 0;
    float tempMovementDelayRight = 0;

    [SerializeField]
    StandardPlayerShot standardShot;
    [SerializeField]
    ChargedPlayerShot chargedShot;
    /*Spell origin:
    0 - Regular Slot
    */
    [SerializeField]
    List<GameObject> spellOrigin;
    [SerializeField]
    Animator anim;
    public Animator emotionAnim;
    public Animator canvasAnim;
    public Canvas playerCanvas;
    GameObject firstButton;
    TurnBarScript turnBarScript;
    SpriteRenderer playerSprite;
    
    EntityStatus status;
    private GameObject previousRaycastTile;
    private string tileTag = "Tile";
    private Ray ray;
    private RaycastHit hit;
    
    
    
    // Start is called before the first frame update
    void Awake()
    {
        //objectPooler = ObjectPooler.Instance;
        //bfs = GameObject.Find("BattlefieldMaster").GetComponent<BattlefieldScript>();
        //bfs.playerPosition = new Vector2(transform.position.x, transform.position.y);

        anim = GetComponent<Animator>();
        status = GetComponent<EntityStatus>();
        emotionAnim = GameObject.Find("PlayerEmotionSprite").GetComponent<Animator>();
        canvasAnim = GameObject.Find("Canvas").GetComponent<Animator>();
        firstButton = GameObject.Find("Slot1");
        turnBarScript = GameObject.Find("TurnBar").GetComponent<TurnBarScript>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        Instance = this;
      
    }
    private void Start()
    {
        
        objectPooler = ObjectPooler.Instance;
        bfs = BattlefieldScript.Instance;
        //objectPooler.allPooledObjects.Add(gameObject);
        currentTile = bfs.battleTilesGrid[(int)bfs.playerSpawn.x, (int)bfs.playerSpawn.y];
        currentTileClass = currentTile.GetComponent<TileClass>();

        currentTileClass.SetColour(playerTileColour);
        transform.position = new Vector3 (currentTile.transform.position.x, currentTile.transform.position.y, heightAboveGround);
        bfs.playerPosition = new Vector2((int)currentTileClass.gridLocation.x, (int)currentTileClass.gridLocation.y);
        previousTile = currentTile;

        standardShot = GetComponent<StandardPlayerShot>();
        chargedShot = GetComponent<ChargedPlayerShot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused == false)
        {
            if (isLerping == false)
            {
                if (Input.GetButtonDown("MoveUp"))
                {
                    if ((int)currentTileClass.gridLocation.y + movementRange < bfs.yMax && TileCheck(0, movementRange))
                    {
                        isLerping = true;
                        ZeroOutTheDelays();
                        UpdateBattlefield(0, movementRange);
                        UpdatePlayer();
                    }

                }
                else if (Input.GetButtonDown("MoveDown"))
                {
                    if ((int)currentTileClass.gridLocation.y - movementRange >= 0 && TileCheck(0, -movementRange))
                    {
                        isLerping = true;
                        ZeroOutTheDelays();
                        UpdateBattlefield(0, -movementRange);
                        UpdatePlayer();
                    }

                }
                else if (Input.GetButtonDown("MoveLeft"))
                {
                    if ((int)currentTileClass.gridLocation.x - 1 >= 0 && TileCheck(-movementRange, 0))
                    {
                        isLerping = true;
                        ZeroOutTheDelays();
                        UpdateBattlefield(-movementRange, 0);
                        UpdatePlayer();
                    }

                }
                else if (Input.GetButtonDown("MoveRight"))
                {
                    if ((int)currentTileClass.gridLocation.x + 1 < bfs.xMax && TileCheck(movementRange, 0))
                    {
                        isLerping = true;
                        ZeroOutTheDelays();
                        UpdateBattlefield(movementRange, 0);
                        UpdatePlayer();
                    }

                }

            }
            ContinuousMovement();
            if (Input.GetButton("Shoot"))
            {
                shotChargeAmount += Time.deltaTime;
                if (shotChargeAmount < maxShotChargeTime && shotChargeAmount > 0.05f)
                {
                    if (chargeParticles[0].isPlaying == false)
                    {
                        chargeParticles[0].Play();
                    }
                }
                else if (shotChargeAmount >= maxShotChargeTime && shotFullyCharged == false)
                {

                    chargeParticles[0].Stop();
                    chargeParticles[1].Play();
                    shotFullyCharged = true;

                }
            }
            if (Input.GetButtonUp("Shoot"))
            {
                foreach (ParticleSystem ps in chargeParticles)
                {
                    if (ps.isPlaying == true)
                    {
                        ps.Stop();
                        ps.Clear();
                    }
                }
                anim.Play("Attack");
                if (shotFullyCharged == false)
                {

                    standardShot.Shoot(spellOrigin[0].transform, gameObject);
                    //objectPooler.SpawnFromPool("PlayerBullet", spellOrigin.transform.position, Quaternion.Euler(0, 0, 90), gameObject.transform);
                }
                else
                {
                    //objectPooler.SpawnFromPool("ChargedPlayerBullet", spellOrigin.transform.position, Quaternion.Euler(0, 0, 90), gameObject.transform);
                    chargedShot.ShootCharged(spellOrigin[0].transform, gameObject);
                    shotFullyCharged = false;
                }

                shotChargeAmount = 0;
            }
        }
        ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit, 10f))
        {
            Debug.DrawRay(transform.position, -transform.up);
            if (previousRaycastTile == null)
            {
                previousRaycastTile = hit.transform.gameObject;
                previousRaycastTile.GetComponent<TileClass>().SetColour(playerTileColour);
            }
            else
            {
                previousRaycastTile.GetComponent<TileClass>().SetColour(previousRaycastTile.GetComponent<TileClass>().initialMaterialColour);
                previousRaycastTile = hit.transform.gameObject;
                previousRaycastTile.GetComponent<TileClass>().SetColour(playerTileColour);
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

        if(Input.GetButtonUp("StartTurn") && turnBarScript.currentTurnTime >= turnBarScript.maxTurnTime)
        {
            if (isPaused == false)
            {
                //objectPooler.PauseAll();
                EventSystem.current.SetSelectedGameObject(firstButton);
               
                canvasAnim.Play("MenuSlideIn");
            }
            else
            {
                canvasAnim.Play("MenuSlideOut");
                EventSystem.current.SetSelectedGameObject(null);
                //objectPooler.UnPauseAll();
            }
        }
    }

    void ContinuousMovement()
    {
        if (isLerping == false)
        {
            if (Input.GetButton("MoveUp"))
            {
                if ((int)currentTileClass.gridLocation.y + movementRange < bfs.yMax && TileCheck(0, movementRange))
                {
                    tempMovementDelayUp += Time.deltaTime;
                    if (tempMovementDelayUp > contMovementDelay)
                    {
                        ZeroOutTheDelays();
                        UpdateBattlefield(0, movementRange);
                        UpdatePlayer();
                        
                    }
                }

            }
            if (Input.GetButton("MoveDown"))
            {
                if ((int)currentTileClass.gridLocation.y - movementRange >= 0 && TileCheck(0, -movementRange))
                {
                    tempMovementDelayDown += Time.deltaTime;
                    if (tempMovementDelayDown > contMovementDelay)
                    {
                        ZeroOutTheDelays();
                        UpdateBattlefield(0, -movementRange);
                        UpdatePlayer();
                    }
                }

            }
            if (Input.GetButton("MoveLeft"))
            {
                if ((int)currentTileClass.gridLocation.x - 1 >= 0 && TileCheck(-movementRange, 0))
                {
                    tempMovementDelayLeft += Time.deltaTime;
                    if (tempMovementDelayLeft > contMovementDelay)
                    {
                        ZeroOutTheDelays();
                        UpdateBattlefield(-movementRange, 0);
                        UpdatePlayer();
                    }
                }

            }
            if (Input.GetButton("MoveRight"))
            {
                if ((int)currentTileClass.gridLocation.x + 1 < bfs.xMax && TileCheck(movementRange, 0))
                {
                    tempMovementDelayRight += Time.deltaTime;
                    if (tempMovementDelayRight > contMovementDelay)
                    {
                        ZeroOutTheDelays();
                        UpdateBattlefield(movementRange, 0);
                        UpdatePlayer();
                    }
                }

            }

        }
        if(Input.GetButtonUp("MoveUp"))
        {
            tempMovementDelayUp = 0;
        }
        if(Input.GetButtonUp("MoveDown"))
        {
            tempMovementDelayDown = 0;
        }
        if (Input.GetButtonUp("MoveLeft"))
        {
            tempMovementDelayLeft = 0;
        }
        if (Input.GetButtonUp("MoveRight"))
        {
            tempMovementDelayRight = 0;
        }
    }

    void ZeroOutTheDelays()
    {
        tempMovementDelayUp = 0;
        tempMovementDelayDown = 0;
        tempMovementDelayLeft = 0;
        tempMovementDelayRight = 0;
    }

    public void UpdateBattlefield(int x = 0, int y = 0)
    {
        
        //currentTileClass.SetColour(currentTileClass.initialMaterialColour);
        previousTile = currentTile;
        SetTileInfo(x,y);
        bfs.playerPosition = new Vector2((int)currentTileClass.gridLocation.x, (int)currentTileClass.gridLocation.y);
    }

    public void UpdatePlayer()
    {
        
        //Teleport player to new position
        //transform.position = new Vector3(currentTile.transform.position.x, currentTile.transform.position.y, -1.4f);
        StartCoroutine(LerpPlayer(movementSpeed));
        //currentTileClass.SetColour(playerTileColour);
        //transform.position = new Vector3(bfs.battleTilesGrid[(int)bfs.playerPosition.x,(int)bfs.playerPosition.y].transform.position.x, bfs.battleTilesGrid[(int)bfs.playerPosition.x, (int)bfs.playerPosition.y].transform.position.y,-1.4f);

        //bfs.battleTilesGrid[(int)transform.position.x, (int)transform.position.y].GetComponent<TileClass>().SetColour(playerTileColour);
        //tileColour = bfs.battleTiles[bfs.currentTile].GetComponent<Renderer>().material.color;
        //bfs.battleTiles[bfs.currentTile].GetComponent<Renderer>().material.color = playerTileColour;
        //bfs.battleTiles[bfs.currentTile].transform.localPosition += new Vector3(0, Mathf.Lerp(0, -0.1f, 60), 0);
        //bfs.battleTiles[bfs.currentTile].GetComponent<TileClass>().occupied = true;
    }
    bool TileCheck(int movementRangeX = 0, int movementRangeY = 0)
    {
        foreach (TileAlignment aligned in alignedTiles)
        {
            if (bfs.battleTilesGrid[(int)currentTileClass.gridLocation.x + movementRangeX, (int)currentTileClass.gridLocation.y + movementRangeY].GetComponent<TileClass>().tAlign == aligned)
            { 
               //if (battleTiles[currentTile + movementRange].GetComponent<TileClass>().occupied == false)
               //{

               return true;
               //}
            }
            
        }
        return false;
    }
    void SetTileInfo(int x, int y)
    {
        currentTile = bfs.battleTilesGrid[(int)currentTileClass.gridLocation.x + x, (int)currentTileClass.gridLocation.y + y];
        currentTileClass = currentTile.GetComponent<TileClass>();
        //currentTileClass.DebugCurrentTile();
    }

    IEnumerator LerpPlayer(float time)
    {
        float _elapsedTime = 0f;
        isLerping = true;
        Vector3 pT = new Vector3(previousTile.transform.position.x, previousTile.transform.position.y, heightAboveGround);
        Vector3 cT = new Vector3(currentTile.transform.position.x, currentTile.transform.position.y, heightAboveGround);

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
        playerSprite.sortingOrder = -(int)currentTileClass.gridLocation.y;
        //For continuous movement, resetting the delay
        isLerping = false;
        

        yield return new WaitForSeconds(0);
    }

    #region Pausing/Unpausing

    public void Paused()
    {
        anim.enabled = false;
        emotionAnim.enabled = false;
        ParticleSystem _pSys = status.hitParticles;
        if(_pSys.isPlaying)
        {
            _pSys.Pause();
        }
        isPaused = true;
        
    }

    public void UnPaused()
    {
        ParticleSystem _pSys = status.hitParticles;
        if (_pSys.isPaused)
        {
            _pSys.Play();
        }
        anim.enabled = true;
        emotionAnim.enabled = true;
        
        isPaused = false;
        
    }
    #endregion

}
