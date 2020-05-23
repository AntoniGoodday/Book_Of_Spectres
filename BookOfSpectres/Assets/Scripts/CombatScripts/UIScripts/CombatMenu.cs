using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using EnumScript;

public class CombatMenu : MonoBehaviour
{
    public Animator canvasAnimator;
    ObjectPooler objectPooler;
    PlayerDeck playerDeck;
    PlayerAttributes playerAttributes;


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

    

    [SerializeField]
    ManaManager manaManager;

    int minimumSpellsChosen = 0;

    public int MinimumSpellsChosen { get => minimumSpellsChosen; set => minimumSpellsChosen = value; }

    public delegate void MenuPauseDelegate();
    public event MenuPauseDelegate menuPauseEvent;
    public delegate void MenuUnPauseDelegate();
    public event MenuUnPauseDelegate menuUnPauseEvent;

    private void Start()
    {
        Random.seed = System.Environment.TickCount;
        objectPooler = ObjectPooler.Instance;
        playerDeck = PlayerDeck.Instance;
        playerAttributes = PlayerAttributes.Instance;

        firstButton = GameObject.Find("Slot1");

        canvasAnimator = gameObject.GetComponent<Animator>();

        foreach(SpellCard d in playerDeck.pDeck)
        { 
           playerCombatDeck.Add(d);   
        }

        if(shuffleDeck == true)
        {
            for(int i = 0; i < playerCombatDeck.Count; i++)
            {
                SpellCard temp = playerCombatDeck[i];
                int randomIndex = Random.Range(i, playerCombatDeck.Count);
                playerCombatDeck[i] = playerCombatDeck[randomIndex];
                playerCombatDeck[randomIndex] = temp;
            }
        }

        manaManager = GameObject.Find("ManaManager").GetComponent<ManaManager>();
    }

    public void ReadyButton()
    {
        if (chosenSpells.spellMiniatures.Count >= minimumSpellsChosen)
        {
            EventSystem.current.SetSelectedGameObject(null);
            canvasAnimator.Play("MenuSlideOut");
        }
    }

    public void MenuPause()
    {
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
        menuPauseEvent?.Invoke();
        this.GetComponent<CanvasGroup>().interactable = true;
    }

    public void MenuClosed()
    {
        menuUnPauseEvent?.Invoke();
        this.GetComponent<CanvasGroup>().interactable = false;
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


