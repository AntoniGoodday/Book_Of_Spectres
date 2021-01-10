using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumScript;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using PlayerControlNamespace;
public class PlayerScript : EntityScript
{
    public static PlayerScript Instance;
    //ObjectPooler objectPooler;

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
    //public Animator anim;


    public Animator emotionAnim;
    public Animator canvasAnim;
    public CombatMenu combatMenu;

    public Canvas playerCanvas;
    GameObject firstButton;
    TurnBarScript turnBarScript;
    [SerializeField]
    public SpriteRenderer playerSprite;
    CardHolder cardHolder;

    //public EntityStatus status;




    bool isLerping = false;
    bool bufferedAttack = false;
    bool bufferedSpell = false;

    public bool IsLerping { get => isLerping; set => isLerping = value; }
    public bool BufferedAttack { get => bufferedAttack; set => bufferedAttack = value; }
    public bool BufferedSpell { get => bufferedSpell; set => bufferedSpell = value; }

    public CombatMove playerMove;
    public ICombatShoot playerShoot;
    public ICombatSpell playerSpell;

    public PlayerControl playerControl;

    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        //playerControl = new PlayerControl();

        //anim = GetComponent<Animator>();

        //status = GetComponent<EntityStatus>();

        Instance = this;

        playerMove = GetComponent<CombatMove>();

        playerShoot = GetComponent<ICombatShoot>();

        playerSpell = GetComponent<ICombatSpell>();

        
    }

    

    private void Start()
    {
        //objectPooler = ObjectPooler.Instance;
        //objectPooler.allPooledObjects.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(!Status)
        {
            Status = GetComponent<EntityStatus>();
        }

        if (Status.IsPaused == false && Status.IsDying == false)
        {
            PlayerControls();
        }
       
    }

    void PlayerControls()
    {
        playerMove.Move();
        playerShoot.ChargeUpdate();
        playerSpell.UseSpell();

        /*if (Input.GetButtonUp("Use"))
        {
            PlayerSpell();
        }*/

        /*if (Input.GetButtonUp("StartTurn"))
        {
            SpellMenu();
        }*/


    }
    #region Pausing/Unpausing

    /*public void Paused()
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
    }*/

    
    #endregion

    public void EndMove()
    {
        if(bufferedAttack)
        {
            playerShoot.Shoot();
            bufferedAttack = false;
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
        if (!Status.IsPaused && !Status.IsDying)
        {
            if (cardHolder.spellMiniatures.Count > 0)
            {
                cardHolder.UseSpell(gameObject, Status, spellOrigin[0].transform);
                anim.Play("Spell");
                spellParticles.Play();
            }
        }
    }

    /*public void SpellMenu()
    {
        if (turnBarScript.CurrentTurnTime >= turnBarScript.MaxTurnTime)
        {
            if (status.IsPaused == false)
            {
                //objectPooler.PauseAll();



                List<SpellCard> _tempSpellList = new List<SpellCard>();

                foreach (SpellCard s in combatMenu.playerCombatInUse)
                {
                    _tempSpellList.Add(s);
                }

                foreach (SpellCard s in _tempSpellList)
                {
                    combatMenu.MoveCardToDestination(s, CardDestination.Combat, CardDestination.Graveyard);
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
    }*/

    public void StartCombat()
    {
        emotionAnim = GameObject.Find("PlayerEmotionSprite").GetComponent<Animator>();
        combatMenu = CombatMenu.Instance;
        firstButton = GameObject.Find("Slot1");
        turnBarScript = GameObject.Find("TurnBar").GetComponent<TurnBarScript>();
        //cardHolder = GameObject.Find("PlayerCanvas").GetComponent<CardHolder>();

        //CombatMenu.MenuUnPauseEvent += UnPaused;
        //CombatMenu.MenuPauseEvent += Paused;

        playerSpell.InitializeSpell();
        //cardHolder.Initialize();
        Status.Initialize();

        List<SpellCard> _tempSpellList = new List<SpellCard>();

        foreach (SpellCard s in combatMenu.playerCombatInUse)
        {
            _tempSpellList.Add(s);
        }

        foreach (SpellCard s in _tempSpellList)
        {
            combatMenu.MoveCardToDestination(s, CardDestination.Combat, CardDestination.Graveyard);
        }

        //cardHolder.Purge();
        turnBarScript.Pause(false);

        ObjectPooler.StartWave();
        //combatMenu.TweenMenu();
        //canvasAnim.Play("MenuSlideIn");
    }

   
}
