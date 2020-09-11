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
    public static PlayerScript Instance;
    ObjectPooler objectPooler;

    [SerializeField]
    public List<ParticleSystem> chargeParticles;
    [SerializeField]
    public ParticleSystem spellParticles;


    
    /*Spell origin:
    0 - Regular Slot
    */
    [SerializeField]
    public List<GameObject> spellOrigin;
    [SerializeField]
    public Animator anim;


    public Animator emotionAnim;
    public Animator canvasAnim;
    public CombatMenu combatMenu;

    public Canvas playerCanvas;
    GameObject firstButton;
    TurnBarScript turnBarScript;
    [SerializeField]
    public SpriteRenderer playerSprite;
    CardHolder cardHolder;

    public EntityStatus status;




    bool isLerping = false;
    bool isPaused = false;
    bool dying = false;
    bool bufferedAttack = false;
    bool bufferedSpell = false;

    public bool Dying { get => dying; set => dying = value; }
    public bool IsLerping { get => isLerping; set => isLerping = value; }
    public bool IsPaused { get => isPaused; set => isPaused = value; }
    public bool BufferedAttack { get => bufferedAttack; set => bufferedAttack = value; }
    public bool BufferedSpell { get => bufferedSpell; set => bufferedSpell = value; }

    public ICombatMove playerMove;
    public ICombatShoot playerShoot;
    public ICombatSpell playerSpell;

    public PlayerControl playerControl;

    // Start is called before the first frame update
    void Awake()
    {

        playerControl = new PlayerControl();

        Instance = this;

        playerMove = GetComponent<ICombatMove>();

        playerShoot = GetComponent<ICombatShoot>();

        playerSpell = GetComponent<ICombatSpell>();
    }

    private void OnEnable()
    {
        playerControl.Enable();
    }

    private void OnDisable()
    {
        playerControl.Disable();
    }

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        //objectPooler.allPooledObjects.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        if (IsPaused == false && Dying == false)
        {
            PlayerControls();
        }

       
    }

    void PlayerControls()
    {
        playerMove.Move();
        playerShoot.ChargeUpdate();
        //playerSpell.UseSpell();

        /*if (Input.GetButtonUp("Use"))
        {
            PlayerSpell();
        }*/

        if (Input.GetButtonUp("StartTurn"))
        {
            SpellMenu();
        }


    }
    #region Pausing/Unpausing

    public void Paused()
    {
        IsPaused = true;
    }

    public void UnPaused()
    {
        IsPaused = false;

        if(playerShoot.PauseHold && playerShoot.IsHeld == false && playerShoot.ShotChargeAmount > 0)
        {
            Debug.Log("Held Shot Released");
            playerShoot.Shoot();
        }
    }

    
    #endregion

    public void EndMove()
    {
        if(bufferedAttack)
        {
            playerShoot.Shoot();
        }
    }

    public void ClearChargeParticles()
    {
        foreach (ParticleSystem ps in chargeParticles)
        {
            if (ps.isPlaying == true)
            {
                ps.Stop();
                ps.Clear();
            }
        }

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

                playerSpell.CardHolder.Purge();
                turnBarScript.Pause(false);

                combatMenu.TweenMenu();
                //EventSystem.current.SetSelectedGameObject(null);
                //EventSystem.current.SetSelectedGameObject(firstButton);
            }
            else
            {
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
        //cardHolder = GameObject.Find("PlayerCanvas").GetComponent<CardHolder>();

        CombatMenu.MenuUnPauseEvent += UnPaused;
        CombatMenu.MenuPauseEvent += Paused;

        playerSpell.InitializeSpell();
        //cardHolder.Initialize();
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

        //cardHolder.Purge();
        turnBarScript.Pause(false);

        combatMenu.TweenMenu();
        //canvasAnim.Play("MenuSlideIn");
    }

   
}
