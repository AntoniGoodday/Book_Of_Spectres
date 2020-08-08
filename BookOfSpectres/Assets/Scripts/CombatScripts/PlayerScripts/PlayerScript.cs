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
    float maxShotChargeTime = 2;
    [SerializeField]
    float shotChargeAmount = 0;
    [SerializeField]
    bool shotFullyCharged = false;
    [SerializeField]
    List<ParticleSystem> chargeParticles;
    [SerializeField]
    ParticleSystem spellParticles;


    bool isLerping = false;
    bool isPaused = false;


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
    CombatMenu combatMenu;

    public Canvas playerCanvas;
    GameObject firstButton;
    TurnBarScript turnBarScript;
    [SerializeField]
    public SpriteRenderer playerSprite;
    CardHolder cardHolder;

    public EntityStatus status;





    bool dying = false;
    bool shootIsHeld = false;
    bool bufferedAttack = false;
    bool bufferedSpell = false;

    public bool Dying { get => dying; set => dying = value; }
    public bool IsLerping { get => isLerping; set => isLerping = value; }
    public bool IsPaused { get => isPaused; set => isPaused = value; }

    ICombatMove playerMove;

    public PlayerControl playerControl;

    // Start is called before the first frame update
    void Awake()
    {

        playerControl = new PlayerControl();

        Instance = this;

        playerMove = GetComponent<ICombatMove>();
    }

    private void OnEnable()
    {
        playerControl.Enable();
    }

    private void OnDisable()
    {
        playerControl.Disable();
    }

    private void Shoot_performed(InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {

        objectPooler = ObjectPooler.Instance;
        //objectPooler.allPooledObjects.Add(gameObject);
        

        standardShot = GetComponent<StandardPlayerShot>();
        chargedShot = GetComponent<ChargedPlayerShot>();

    }

    // Update is called once per frame
    void Update()
    {

        if (IsPaused == false && Dying == false)
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
        }

       
    }

    void PlayerControls()
    {
        playerMove.Move();

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
        IsPaused = true;

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
        IsPaused = false;

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

    IEnumerator ShotDelay()
    {
        yield return new WaitForSeconds(0.2f);
        canShoot = true;
    }

    void PlayerSpell()
    {
        if (!IsPaused && !Dying)
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
        if (!IsPaused && !Dying)
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
            if (IsPaused == false)
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

                combatMenu.TweenMenu();
                //canvasAnim.Play("MenuSlideIn");


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
        combatMenu = canvasAnim.GetComponent<CombatMenu>();
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

        combatMenu.TweenMenu();
        //canvasAnim.Play("MenuSlideIn");
    }

   
}
