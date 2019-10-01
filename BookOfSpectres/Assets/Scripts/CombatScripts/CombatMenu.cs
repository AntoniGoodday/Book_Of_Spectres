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

    public List<SpellCard> playerCombatExile = new List<SpellCard>();


    public SpellAdvance spellAdvance;

    public List<SpellSlot> spellSlots;

    public ChosenSpells chosenSpells;

    GameObject firstButton;
    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        playerDeck = PlayerDeck.Instance;
        playerAttributes = PlayerAttributes.Instance;

        firstButton = GameObject.Find("Slot1");

        canvasAnimator = gameObject.GetComponent<Animator>();

        foreach(SpellCard d in playerDeck.pDeck)
        {
            
           playerCombatDeck.Add(d);
            
        }

    }

    public void ReadyButton()
    {
        
        canvasAnimator.Play("MenuSlideOut");
    }

    public void MenuPause()
    {
        if (objectPooler.isPaused == false)
        {
            objectPooler.PauseAll();
        }
        
        RefillHand();

        PopulateSlots();

        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    public void CheckForAdvancedSpells()
    {
        chosenSpells.CheckAdvancedSpells();
    }

    public void MenuUnPause()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (objectPooler.isPaused)
        {
            objectPooler.UnPauseAll();
        }
    }

    void RefillHand()
    {

        for (int i = playerCombatHand.Count; i < playerAttributes.handSize; i++)
        {
            if (playerCombatDeck.Count > 0)
            {
                SpellCard _currentSpell = playerCombatDeck[0];
                MoveCardToDestination(_currentSpell, CardDestination.Deck, CardDestination.Hand);
            }
            else
            {
                break;
            }
        }
    }

    void PopulateSlots()
    {
        int _handSize = playerAttributes.handSize;

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
}


