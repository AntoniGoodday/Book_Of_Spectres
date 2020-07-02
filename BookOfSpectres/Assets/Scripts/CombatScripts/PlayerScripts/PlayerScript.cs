using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumScript;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using PlayerControlNamespace;
public class PlayerScript : MonoBehaviour
{
    #region delegates
    public delegate void ShootDelegate();
    public event ShootDelegate ShootEvent;
    public delegate void ChargedShootDelegate();
    public event ShootDelegate ChargedShootEvent;


    public delegate void MoveDelegate(MoveDirection direction);
    public event MoveDelegate MoveEvent;
    #endregion

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
    ParticleSystem spellParticles;
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

    bool canShoot = true;
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
    [SerializeField]
    public SpriteRenderer playerSprite;
    CardHolder cardHolder;

    public EntityStatus status;
    private GameObject previousRaycastTile;
    private GameObject currentRaycastTile;
    private string tileTag = "Tile";
    private Ray ray;
    private RaycastHit hit;

    private bool vertAxisInUse = false;
    private bool horizontalAxisInUse = false;

    PlayerControl playerControl;

    bool dying = false;

    bool shootIsHeld = false;
    bool instantMove = true;
    Vector2 moveVector = new Vector2();
    bool bufferedMove = false;
    Vector2 lastMove = new Vector2();

    public bool Dying { get => dying; set => dying = value; }

    // Start is called before the first frame update
    void Awake()
    {
        /*playerControl = new PlayerControl();

        //playerControl.DefaultControls.Move.performed += context => MovePlayer(context.ReadValue<Vector2>());

        playerControl.DefaultControls.Move.performed += context => { if (!isPaused) { moveVector = context.ReadValue<Vector2>(); } };

        playerControl.DefaultControls.Shoot.started += context => {if (!isPaused) { shootIsHeld = true; } };
        playerControl.DefaultControls.Shoot.performed += context => PlayerShot();

        playerControl.DefaultControls.Spell.performed += context => PlayerSpell();

        playerControl.DefaultControls.Menu.performed += context => SpellMenu();

        playerControl.Enable();*/

        
        

        

        Instance = this;

    }

    private void OnDisable()
    {
        //playerControl.Disable();
    }

    private void Shoot_performed(InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {

        objectPooler = ObjectPooler.Instance;
        bfs = BattlefieldScript.Instance;
        //objectPooler.allPooledObjects.Add(gameObject);
        currentTile = bfs.battleTilesGrid[(int)bfs.playerSpawn.x, (int)bfs.playerSpawn.y];
        currentTileClass = currentTile.GetComponent<TileClass>();


        transform.position = new Vector3(currentTile.transform.position.x, currentTile.transform.position.y, heightAboveGround);
        bfs.playerPosition = new Vector2Int((int)currentTileClass.gridLocation.x, (int)currentTileClass.gridLocation.y);
        previousTile = currentTile;

        standardShot = GetComponent<StandardPlayerShot>();
        chargedShot = GetComponent<ChargedPlayerShot>();

        //currentTileClass.SetColour(playerTileColour);
        currentTileClass.occupied = true;

        SetSortingOrder(-(int)currentTileClass.gridLocation.y + 5);
    }

    // Update is called once per frame
    void Update()
    {

        if (isPaused == false && Dying == false)
        {
            PlayerControls();

            if(shootIsHeld == true)
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

            /*if(moveVector.x != 0 || moveVector.y != 0)
            {
                if (isLerping == false /*&& playerControl.DefaultControls.Move.triggered)
                {
                    MovePlayer(moveVector);
                }
            }*/


            if (Input.GetAxisRaw("MoveVertical") == 0)
            {
                
                vertAxisInUse = false;
            }
            if (Input.GetAxis("MoveHorizontal") == 0)
            {
                horizontalAxisInUse = false;
            }

            

            
        }

       
    }

    private void FixedUpdate()
    {
        //Raycast downwards
        ray = new Ray(transform.position, transform.forward);

        RayCheck(ray);
    }

   
    /*void ContinuousMovement()
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
                        /*ZeroOutTheDelays();
                        UpdateBattlefield(0, movementRange);
                        UpdatePlayer();
                        MovePlayer(MoveDirection.Up);

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
                        MovePlayer(MoveDirection.Down);
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
                        MovePlayer(MoveDirection.Left);
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
                        MovePlayer(MoveDirection.Right);
                    }
                }

            }

        }
        if (Input.GetButtonUp("MoveUp"))
        {
            tempMovementDelayUp = 0;
        }
        if (Input.GetButtonUp("MoveDown"))
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
    }*/

    void PlayerControls()
    {

        moveVector.y = Input.GetAxisRaw("Vertical");

        moveVector.x = Input.GetAxisRaw("Horizontal");

        if(moveVector == Vector2.zero && instantMove == false || Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        {
            instantMove = true;
        }

        if (Input.GetButtonDown("MoveUp"))
        {
            if (isLerping == true)
            {
                lastMove = new Vector2(0, 1);
                bufferedMove = true;
            }
            else
            {
                MovePlayer(new Vector2(0, 1));
            }
        }
        else if (Input.GetButtonDown("MoveDown"))
        {
            if (isLerping == true)
            {
                lastMove = new Vector2(0, -1);
                bufferedMove = true;
            }
            else
            {
                MovePlayer(new Vector2(0, -1));
            }
        }
        else if (Input.GetButtonDown("MoveLeft"))
        {
            if (isLerping == true)
            {
                lastMove = new Vector2(-1, 0);
                bufferedMove = true;
            }
            else
            {
                MovePlayer(new Vector2(-1, 0));
            }
        }
        else if (Input.GetButtonDown("MoveRight"))
        {
            if (isLerping == true)
            {
                lastMove = new Vector2(1, 0);
                bufferedMove = true;
            }
            else
            {
                MovePlayer(new Vector2(1, 0));
            }
        }

        if(Input.GetButtonDown("Shoot"))
        {
            shootIsHeld = true;
        }

        if(Input.GetButtonUp("Shoot"))
        {
            PlayerShot();
        }

        if (Input.GetButtonUp("Use"))
        {
            PlayerSpell();
        }

        if (Input.GetButtonUp("StartTurn"))
        {
            SpellMenu();
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
        SetTileInfo(x, y);
        bfs.playerPosition = new Vector2Int((int)currentTileClass.gridLocation.x, (int)currentTileClass.gridLocation.y);
    }

    public void UpdatePlayer()
    {
        StartCoroutine(LerpPlayer(movementSpeed));
    }

    bool TileCheck(int movementRangeX = 0, int movementRangeY = 0)
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

    void SetTileInfo(int x, int y)
    {
        currentTile = bfs.battleTilesGrid[(int)currentTileClass.gridLocation.x + x, (int)currentTileClass.gridLocation.y + y];
        currentTileClass = currentTile.GetComponent<TileClass>();
        //currentTileClass.DebugCurrentTile();
    }

    IEnumerator LerpPlayer(float time)
    {
        isLerping = true;

        float _elapsedTime = 0f;

        //Set which tile the entity is on before it moves, so that it won't clip into another entity
        previousTile.GetComponent<TileClass>().occupied = false;


        currentTileClass.occupied = true;


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

        SetSortingOrder(-(int)currentTileClass.gridLocation.y + 5);


        //For continuous movement, resetting the delay
        isLerping = false;

        ///BUFFER MOVE
        if(bufferedMove == true)
        {
            MovePlayer(lastMove);
            bufferedMove = false;
        }


        yield return new WaitForSeconds(0);
    }

    #region Pausing/Unpausing

    public void Paused()
    {
        /*anim.enabled = false;
        emotionAnim.enabled = false;
        ParticleSystem _pSys = status.hitParticles;
        if (_pSys.isPlaying)
        {
            _pSys.Pause();
        }*/
        isPaused = true;

    }

    public void UnPaused()
    {
        /*
        ParticleSystem _pSys = status.hitParticles;
        if (_pSys.isPaused)
        {
            _pSys.Play();
        }
        anim.enabled = true;
        emotionAnim.enabled = true;
        */
        isPaused = false;

        if(shotChargeAmount >= maxShotChargeTime)
        {
            ShootCharged();
            ClearChargeParticles();
            shotChargeAmount = 0;
            shootIsHeld = false;
        }
        else if(shotChargeAmount <= maxShotChargeTime)
        {
            ClearChargeParticles();
            shotChargeAmount = 0;
            shootIsHeld = false;
        }

    }

    void RayCheck(Ray ray)
    {
        if (Physics.Raycast(ray, out hit, 10f))
        {
            Debug.DrawRay(transform.position, -transform.up);
            if (previousRaycastTile == null)
            {
                previousRaycastTile = hit.transform.gameObject;
                currentRaycastTile = hit.transform.gameObject;

                TileClass _previousRaycastTileClass = previousRaycastTile.GetComponent<TileClass>();
                _previousRaycastTileClass.SetColour(playerTileColour);
            }
            else
            {
                if (hit.transform.gameObject != currentRaycastTile | currentRaycastTile.GetComponent<TileClass>().currentColour != playerTileColour)
                {
                    
                    previousRaycastTile.GetComponent<TileClass>().SetColour(previousRaycastTile.GetComponent<TileClass>().initialMaterialColour, false, false, true);

                    currentRaycastTile = hit.transform.gameObject;
                    currentRaycastTile.GetComponent<TileClass>().SetColour(playerTileColour);
                    previousRaycastTile = currentRaycastTile;
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
    #endregion

    void ClearChargeParticles()
    {
        foreach (ParticleSystem ps in chargeParticles)
        {
            if (ps.isPlaying == true)
            {
                ps.Stop();
                ps.Clear();
            }
        }

        shotChargeAmount = 0;
    }

    void ShootCharged()
    {
        chargedShot.ShootCharged(spellOrigin[0].transform, gameObject);
        shotFullyCharged = false;

        anim.Play("Spell");

        spellParticles.Play();

        ChargedShootEvent?.Invoke();
    }

    public void SetSortingOrder(int i)
    {
        if (Dying == false)
        {
            playerSprite.sortingOrder = i;
        }
    }

    IEnumerator ShotDelay()
    {
        yield return new WaitForSeconds(0.2f);
        canShoot = true;
    }

    IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(0.2f);
        //instantMove = true;
    }

    void MovePlayer(Vector2 direction)
    {
        if (isPaused == false && !Dying)
        {
            if (isLerping == false)
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
                    if (direction.x < 0)
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
                    if (direction.y < 0)
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
                                vertAxisInUse = true;
                                _x = 0;
                                _y = 1;
                                break;
                            }
                        case (MoveDirection.Down):
                            {
                                vertAxisInUse = true;
                                _x = 0;
                                _y = -1;
                                break;
                            }
                        case (MoveDirection.Left):
                            {
                                horizontalAxisInUse = true;
                                _x = -1;
                                _y = 0;
                                break;
                            }
                        case (MoveDirection.Right):
                            {
                                horizontalAxisInUse = true;
                                _x = 1;
                                _y = 0;
                                break;
                            }
                        default:
                            {
                                return;
                            }
                    }



                    isLerping = true;
                    ZeroOutTheDelays();
                    UpdateBattlefield(_x * movementRange, _y * movementRange);
                    MoveEvent?.Invoke(moveDir);
                    UpdatePlayer();

                    StopCoroutine(MoveDelay());
                    StartCoroutine(MoveDelay());
                }
            }
        }
        if (instantMove == true)
        {
            instantMove = false;
        }
    }

    void PlayerSpell()
    {
        if (!isPaused && !Dying)
        {
            if (cardHolder.spellMiniatures.Count > 0)
            {
                cardHolder.UseSpell(gameObject, status, spellOrigin[0].transform);
                anim.Play("Spell");
                spellParticles.Play();
            }
        }
    }

    

    void PlayerShot()
    {
        shootIsHeld = false;
        if (!isPaused && !Dying)
        {
            if (canShoot)
            {
                canShoot = false;

                ClearChargeParticles();

                if (shotFullyCharged == false)
                {

                    standardShot.Shoot(spellOrigin[0].transform, gameObject);

                    anim.Play("Attack");

                    //objectPooler.SpawnFromPool("PlayerBullet", spellOrigin.transform.position, Quaternion.Euler(0, 0, 90), gameObject.transform);
                }
                else
                {
                    //objectPooler.SpawnFromPool("ChargedPlayerBullet", spellOrigin.transform.position, Quaternion.Euler(0, 0, 90), gameObject.transform);
                    ShootCharged();
                }
                ShootEvent?.Invoke();

                shotChargeAmount = 0;

                StartCoroutine(ShotDelay());
            }
        }
       
    }

    public void SpellMenu()
    {
        if (turnBarScript.CurrentTurnTime >= turnBarScript.MaxTurnTime)
        {
            if (isPaused == false)
            {
                //objectPooler.PauseAll();


                CombatMenu _tempCombatMenu = canvasAnim.gameObject.GetComponent<CombatMenu>();

                List<SpellCard> _tempSpellList = new List<SpellCard>();

                foreach (SpellCard s in _tempCombatMenu.playerCombatInUse)
                {
                    _tempSpellList.Add(s);
                }

                foreach (SpellCard s in _tempSpellList)
                {
                    _tempCombatMenu.MoveCardToDestination(s, CardDestination.Combat, CardDestination.Graveyard);
                }

                cardHolder.Purge();
                turnBarScript.Pause(false);

                canvasAnim.Play("MenuSlideIn");


                //EventSystem.current.SetSelectedGameObject(null);
                //EventSystem.current.SetSelectedGameObject(firstButton);
            }
            else
            {
                //canvasAnim.Play("MenuSlideOut");

                //EventSystem.current.SetSelectedGameObject(null);
                //objectPooler.UnPauseAll();
            }
        }
    }

    public void StartCombat()
    {
        anim = GetComponent<Animator>();
        status = GetComponent<EntityStatus>();
        emotionAnim = GameObject.Find("PlayerEmotionSprite").GetComponent<Animator>();
        canvasAnim = GameObject.Find("CombatCanvas").GetComponent<Animator>();
        firstButton = GameObject.Find("Slot1");
        turnBarScript = GameObject.Find("TurnBar").GetComponent<TurnBarScript>();
        cardHolder = GameObject.Find("PlayerCanvas").GetComponent<CardHolder>();

        CombatMenu.MenuUnPauseEvent += UnPaused;
        CombatMenu.MenuPauseEvent += Paused;

        cardHolder.Initialize();
        status.Initialize();

        CombatMenu _tempCombatMenu = canvasAnim.gameObject.GetComponent<CombatMenu>();

        List<SpellCard> _tempSpellList = new List<SpellCard>();

        foreach (SpellCard s in _tempCombatMenu.playerCombatInUse)
        {
            _tempSpellList.Add(s);
        }

        foreach (SpellCard s in _tempSpellList)
        {
            _tempCombatMenu.MoveCardToDestination(s, CardDestination.Combat, CardDestination.Graveyard);
        }

        cardHolder.Purge();
        turnBarScript.Pause(false);

        
        canvasAnim.Play("MenuSlideIn");
    }

   
}
