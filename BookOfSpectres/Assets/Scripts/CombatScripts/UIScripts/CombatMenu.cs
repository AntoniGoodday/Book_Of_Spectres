using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using EnumScript;

using UnityEngine.UI;
using DG.Tweening;
using System;

//using PlayerControlNamespace;

public class CombatMenu : MonoBehaviour
{
    public Animator canvasAnimator;
    ObjectPooler objectPooler;
    PlayerDeck playerDeck;
    PlayerAttributes playerAttributes;

    [SerializeField]
    Vector3 destinationPosition;
    [SerializeField]
    Vector3 initialPosition;

    [SerializeField]
    CanvasGroup darkness;
    [SerializeField]
    CanvasGroup light;


    bool hasSlid = false;

    [SerializeField]
    List<SpellCard> playerCombatDeck = new List<SpellCard>();

    public List<SpellCard> playerCombatHand = new List<SpellCard>();

    public List<SpellCard> playerCombatInUse = new List<SpellCard>(); 

    public List<SpellCard> playerCombatGraveyard = new List<SpellCard>();

    public bool recycleGraveyard = false;

    public bool shuffleDeck = false;

    public List<SpellCard> playerCombatExile = new List<SpellCard>();


    public SpellAdvance spellAdvance;

    public List<SpellSlot> spellSlots;

    public ChosenSpells chosenSpells;

    GameObject firstButton;


    //PlayerControl playerControl;
    public Transform UIMenu;

    [SerializeField]
    ManaManager manaManager;

    [SerializeField]
    GameObject visuals;

    [SerializeField]
    CanvasGroup flavourVisuals;

    [SerializeField]
    GameObject turnBar;

    int minimumSpellsChosen = 0;

    public int MinimumSpellsChosen { get => minimumSpellsChosen; set => minimumSpellsChosen = value; }


    public delegate void MenuPauseDelegate();
    public static event MenuPauseDelegate MenuPauseEvent;
    public delegate void MenuUnPauseDelegate();
    public static event MenuUnPauseDelegate MenuUnPauseEvent;

    public delegate void UILoadDelegate();
    public static event UILoadDelegate UILoadEvent;

    public static CombatMenu Instance;

    bool hasToRestart = false;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        initialPosition = visuals.GetComponent<RectTransform>().localPosition;
        destinationPosition = initialPosition + new Vector3(850, 0, 0);

        //TweenMenu();
        //playerControl = new PlayerControl();
        //playerControl.DefaultControls.Enable();

    }

    private void Start()
    {
        SceneManager.UnloadSceneAsync("BattleUIScene");
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
            GetComponent<Canvas>().worldCamera = Camera.main;
            LoadPlayerStats();
            UILoadEvent?.Invoke();
        }
        else
        {
            Destroy(this);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().buildIndex > 2)
        {
            //LoadPlayerStats();
            //UILoadEvent?.Invoke();
            if (hasToRestart == true)
            {
                canvasAnimator.Rebind();
                hasToRestart = false;
            }
            else
            {
                hasToRestart = true;
            }
        }

    }

    public void ReadyButton()
    {
        if (chosenSpells.spellMiniatures.Count >= minimumSpellsChosen)
        {
            EventSystem.current.SetSelectedGameObject(null);
            TweenMenu(false);
            //canvasAnimator.Play("MenuSlideOut");
        }
    }

    public void TweenMenu(bool tweenIn = true)
    {
        DOTween.Init();
        RectTransform _visualsTransform = visuals.GetComponent<RectTransform>();
        Debug.Log(_visualsTransform.localPosition);

        CanvasGroup _turnBar = turnBar.GetComponent<CanvasGroup>();
        
        if (tweenIn == true)
        {
            if (hasSlid == false)
            {
                Sequence _tweenSequence = DOTween.Sequence();
                _tweenSequence.Append(DOTween.To(() => _visualsTransform.localPosition, x => _visualsTransform.localPosition = x, destinationPosition, 0.5f)
                    .SetEase(Ease.OutBack));
                _tweenSequence.Insert(0, DOTween.To(() => _turnBar.alpha, x => _turnBar.alpha = x, 0, 0.1f));
                _tweenSequence.Insert(0.25f, DOTween.To(() => flavourVisuals.alpha, x => flavourVisuals.alpha = x, 1, 0.25f))
                     .SetUpdate(true)
                     .OnStart(() => MenuPause())
                     .OnComplete(() => MenuOpened());

                _tweenSequence.PlayForward();

                hasSlid = true;
            }
        }
        else
        {
            if (hasSlid == true)
            {


                Sequence _tweenOut = DOTween.Sequence();

                _tweenOut.Append(DOTween.To(() => _visualsTransform.localPosition, x => _visualsTransform.localPosition = x, initialPosition, 0.5f))
                    .SetEase(Ease.InBack);
                _tweenOut.Insert(0, DOTween.To(() => _turnBar.alpha, x => _turnBar.alpha = x, 1, 0.1f));
                _tweenOut.Insert(0.25f, DOTween.To(() => flavourVisuals.alpha, x => flavourVisuals.alpha = x, 0, 0.25f))
                    .SetUpdate(true)
                    .OnComplete(() => CheckForAdvancedSpells())
                    .Play();
                hasSlid = false;
            }
            else
            {
                Debug.Log("Can't slide back");
            }
        }
    }

    public void UnDarkenScreen(string s)
    {

        DOTween.Init();
        Sequence _darkenScreen = DOTween.Sequence();

        CanvasGroup _visual = spellAdvance.GetComponent<CanvasGroup>();



        _darkenScreen.Append(DOTween.To(() => _visual.alpha, x => _visual.alpha = x, 0, 0.5f))
                .Join(DOTween.To(() => darkness.alpha, x => darkness.alpha = x, 0f, 0.5f))
                .OnComplete(() => Invoke(s, 0))
                .SetUpdate(true)
                .Play();

        

        
    }

 


    public void FlashScreen(string s)
    {
        DOTween.Init();
        //tweenAction = endAction;
        Sequence _flashScreen = DOTween.Sequence();

        CanvasGroup _visual = spellAdvance.GetComponent<CanvasGroup>();

        _flashScreen.Append(DOTween.To(() => light.alpha, x => light.alpha = x, 1f, 0.1f).SetEase(Ease.InFlash, 2, 0.1f))
                .OnComplete(() => Invoke(s, 0))
                .SetUpdate(true)
                .Play();
    }

    public void MenuPause()
    {
        //playerControl.DefaultControls.Submit.Enable();

        //WIP MANA< CHANGE LATER
        if (objectPooler.isPaused == false)
        {
            objectPooler.PauseAll();
        }

        manaManager.manaType[0].currentAmount = playerAttributes.maxMana;
        manaManager.manaType[0].SetText();

        
        
        RefillHand();

        PopulateSlots(playerCombatHand.Count);

        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    public void CheckForAdvancedSpells()
    {
        chosenSpells.CheckAdvancedSpells();
    }

    public void MenuUnPause()
    {
        TurnBarScript.Instance.UnPause();
        EventSystem.current.SetSelectedGameObject(null);

        if (objectPooler.isPaused)
        {
            objectPooler.UnPauseAll();
        }
        MenuClosed();  
    }


    void RefillHand()
    {
        

        for (int i = playerAttributes.handSize - playerCombatHand.Count; i > 0; i--)
        {
            if (playerCombatDeck.Count > 0)
            {
                SpellCard _currentSpell = playerCombatDeck[0];
                MoveCardToDestination(_currentSpell, CardDestination.Deck, CardDestination.Hand);
            }
            else
            {
                if (recycleGraveyard == true)
                {
                    if(playerCombatGraveyard.Count <= 0)
                    {
                        break;
                    }

                    Recycle(CardDestination.Graveyard, CardDestination.Deck);
                    i++;
                }
                else
                {
                    //PopulateSlots()
                    break;
                }
            }
        }
    }

    void PopulateSlots(int combatHandLimit)
    {
        int _handSize = playerCombatHand.Count;

        for(int j = _handSize; j < spellSlots.Count; j++)
        {
            if(spellSlots[j].gameObject.activeSelf == true)
            {
                spellSlots[j].gameObject.SetActive(false);
            }
        }

        if (_handSize <= spellSlots.Count)
        {
            for (int i = 0; i < _handSize; i++)
            {
                if(spellSlots[i].gameObject.activeSelf == false)
                {
                    spellSlots[i].gameObject.SetActive(true);
                }
                //if (spellSlots[i].currentCard == null)
                //{

                //SpellCard _currentSpell = playerCombatDeck[0];
                //playerCombatDeck.RemoveAt(0);
                
                    //playerCombatDeck.Dequeue();
                    //playerCombatHand.Add(_currentSpell);
                //}
            }
            int _iteration = 0;
            foreach (SpellCard s in playerCombatHand)
            {
                
                spellSlots[_iteration].currentCard = s;

                if (spellSlots[_iteration].gameObject.GetComponent<SpellVisuals>() == true)
                {
                    spellSlots[_iteration].gameObject.GetComponent<SpellVisuals>().LoadSpell(s);
                }
                _iteration++;
            }

            if(_iteration < spellSlots.Count)
            {
                for(int i = _iteration; i < spellSlots.Count; i++)
                {
                    spellSlots[_iteration].currentCard = null;

                    if (spellSlots[_iteration].gameObject.GetComponent<SpellVisuals>() == true)
                    {
                        spellSlots[_iteration].gameObject.GetComponent<SpellVisuals>().LoadSpell(null);
                    }
                }
            }

        }
    }

    public void AdvanceSpell()
    {
        spellAdvance.SetSpellNames();
    }

    public void MenuOpened()
    {
        MenuPauseEvent?.Invoke();
        this.GetComponent<CanvasGroup>().interactable = true;
    }

    public void MenuClosed()
    {
        MenuUnPauseEvent?.Invoke();
        this.GetComponent<CanvasGroup>().interactable = false;
    }

    public void LoadPlayerStats()
    {
        visuals.SetActive(true);

        GetComponent<Canvas>().worldCamera = GameObject.Find("MainVirtualCam").GetComponent<Camera>();

        UnityEngine.Random.seed = System.Environment.TickCount;
        objectPooler = ObjectPooler.Instance;
        playerDeck = PlayerDeck.Instance;
        playerAttributes = PlayerAttributes.Instance;

        firstButton = GameObject.Find("Slot1");

        canvasAnimator = gameObject.GetComponent<Animator>();

        foreach (SpellCard d in playerDeck.pDeck)
        {
            playerCombatDeck.Add(d);
        }

        Debug.Log(playerDeck.pDeck);

        if (shuffleDeck == true)
        {
            for (int i = 0; i < playerCombatDeck.Count; i++)
            {
                SpellCard temp = playerCombatDeck[i];
                int randomIndex = UnityEngine.Random.Range(i, playerCombatDeck.Count);
                playerCombatDeck[i] = playerCombatDeck[randomIndex];
                playerCombatDeck[randomIndex] = temp;
            }
        }

        //manaManager = GameObject.Find("ManaManager").GetComponent<ManaManager>();

        LoadChildren();

       
    }

    void LoadChildren()
    {
        spellAdvance.LoadSpellAdvance();
        chosenSpells.LoadChosenSpells();
        manaManager.LoadManaManager();
    }


    public void MoveCardToDestination(SpellCard s, CardDestination from, CardDestination to)
    {
        switch(from)
        {
            case CardDestination.Hand:
                {
                    playerCombatHand.Remove(s);
                    break;
                }
            case CardDestination.Deck:
                {
                    playerCombatDeck.Remove(s);
                    break;
                }
            case CardDestination.Combat:
                {
                    playerCombatInUse.Remove(s);
                    break;
                }
            case CardDestination.Graveyard:
                {
                    playerCombatGraveyard.Remove(s);
                    break;
                }
            case CardDestination.Exile:
                {
                    playerCombatExile.Remove(s);
                    break;
                }
        }
        switch (to)
        {
            case CardDestination.Hand:
                {
                    playerCombatHand.Add(s);
                    break;
                }
            case CardDestination.Deck:
                {
                    playerCombatDeck.Add(s);
                    break;
                }
            case CardDestination.Combat:
                {
                    playerCombatInUse.Add(s);
                    break;
                }
            case CardDestination.Graveyard:
                {
                    playerCombatGraveyard.Add(s);
                    break;
                }
            case CardDestination.Exile:
                {
                    playerCombatExile.Add(s);
                    break;
                }
        }
    }

    public void Recycle(CardDestination from, CardDestination to)
    {
        switch(from)
        {
            case CardDestination.Hand:
                {
                    int _count = playerCombatHand.Count;
                    for(int i = 0; i < _count; i++)
                    {
                        SpellCard _s = playerCombatHand[0];
                        MoveCardToDestination(_s, from, to);
                    }
                    break;
                }
            case CardDestination.Deck:
                {
                    int _count = playerCombatDeck.Count;
                    for (int i = 0; i < _count; i++)
                    {
                        SpellCard _s = playerCombatDeck[0];
                        MoveCardToDestination(_s, from, to);
                    }
                    break;
                }
            case CardDestination.Combat:
                {
                    int _count = playerCombatInUse.Count;
                    for (int i = 0; i < _count; i++)
                    {
                        SpellCard _s = playerCombatInUse[0];
                        MoveCardToDestination(_s, from, to);
                    }
                    break;
                }
            case CardDestination.Graveyard:
                {
                    int _count = playerCombatGraveyard.Count;
                    for (int i = 0; i < _count; i++)
                    {
                        SpellCard _s = playerCombatGraveyard[0];
                        MoveCardToDestination(_s, from, to);
                    }
                    break;
                }
            case CardDestination.Exile:
                {
                    int _count = playerCombatExile.Count;
                    for (int i = 0; i < _count; i++)
                    {
                        SpellCard _s = playerCombatExile[0];
                        MoveCardToDestination(_s, from, to);
                    }
                    break;
                }
        }
    }
}


