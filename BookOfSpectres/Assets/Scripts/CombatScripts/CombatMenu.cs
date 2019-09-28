using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CombatMenu : MonoBehaviour
{
    public Animator canvasAnimator;
    ObjectPooler objectPooler;
    PlayerDeck playerDeck;
    PlayerAttributes playerAttributes;


    [SerializeField]
    Queue<SpellCard> playerCombatDeck = new Queue<SpellCard>();
    [SerializeField]
    List<SpellCard> playerCombatHand = new List<SpellCard>();
    [SerializeField]
    List<SpellCard> playerCombatGraveyard = new List<SpellCard>();

    public SpellAdvance spellAdvance;

    public List<SpellSlot> spellSlots;

    public ChosenSpells chosenSpells;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        playerDeck = PlayerDeck.Instance;
        playerAttributes = PlayerAttributes.Instance;

        canvasAnimator = gameObject.GetComponent<Animator>();

        foreach(SpellCard d in playerDeck.pDeck)
        {
            
           playerCombatDeck.Enqueue(d);
            
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
        PopulateSlots();
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
                if (spellSlots[i].currentCard == null)
                {
                    
                    SpellCard _currentSpell = playerCombatDeck.Dequeue();
                    spellSlots[i].currentCard = _currentSpell;

                    if (spellSlots[i].gameObject.GetComponent<SpellVisuals>() == true)
                    {
                        spellSlots[i].gameObject.GetComponent<SpellVisuals>().LoadSpell(_currentSpell);
                    }
                    //playerCombatDeck.Dequeue();
                    playerCombatHand.Add(_currentSpell);
                }
            }
        }
    }

    public void AdvanceSpell()
    {
        spellAdvance.SetSpellNames();
    }
}
