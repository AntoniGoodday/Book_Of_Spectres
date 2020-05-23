using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SpellSlot : MonoBehaviour, ISelectHandler
{
    [SerializeField]
    SpellVisuals cardDiplay;
    [SerializeField]
    public SpellCard currentCard;
    [SerializeField]
    GameObject miniature;
    [SerializeField]
    ChosenSpells chosenSpells;
    [SerializeField]
    ManaManager manaManager;

    Button slotButton;

    float lerpTime = 0.1f;

    bool isClicked = false;

    void Awake()
    {
        slotButton = GetComponent<Button>();
        chosenSpells = GameObject.Find("ChosenSpells").GetComponent<ChosenSpells>();
        manaManager = GameObject.Find("ManaManager").GetComponent<ManaManager>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        ChangeDisplayedCard();
    }

    public void WhenClicked(Transform _movementGoal)
    {
        if (isClicked == false)
        {
            if (manaManager.CheckMana(currentCard.spellLogic.mana))
            {
                manaManager.UseMana(currentCard.spellLogic.mana);
                StartCoroutine("LerpMovement", _movementGoal);
            }
        }
        else
        {
            manaManager.UseMana(-currentCard.spellLogic.mana);
            StartCoroutine("LerpMovementBack");
        }
    }

    public void ChangeDisplayedCard()
    {
        cardDiplay.spell = currentCard;
        cardDiplay.LoadSpell(currentCard);
    }

    public void SetMiniature()
    {
        SpellVisuals _spellVis = miniature.GetComponent<SpellVisuals>();
        _spellVis.spell = currentCard;
        _spellVis.LoadSpell(currentCard);
        _spellVis.gameObject.GetComponent<SpellMiniature>().spellSlot = this;
    }


    IEnumerator LerpMovement(Transform _mGoal)
    {
        miniature.SetActive(true);
        SetMiniature();
        chosenSpells.AddToList(miniature);
        ColorBlock _colors = slotButton.colors;
        _colors.normalColor = new Color(0.5f, 0.5f, 0.5f);
        _colors.selectedColor = new Color(0.7f, 0.7f, 0.7f);
        slotButton.colors = _colors;
        isClicked = true;
        float _elapsedTime = 0;
        Vector3 _startingPos = gameObject.transform.position;

       
        while (_elapsedTime <= lerpTime)
        {
          
                miniature.transform.position = Vector3.Lerp(_startingPos, _mGoal.position, (_elapsedTime / lerpTime));
                _elapsedTime += Time.unscaledDeltaTime;
            
            yield return null;
        }
        miniature.transform.position = _mGoal.position;
        miniature.transform.SetParent(_mGoal.parent.transform);
        miniature.transform.SetSiblingIndex(miniature.transform.parent.childCount - 2);
        yield return null;   
    }

    IEnumerator LerpMovementBack()
    {

        Vector3 _startingPos = miniature.transform.position;
        chosenSpells.RemoveFromList(miniature);
         
        ColorBlock _colors = slotButton.colors;
        _colors.normalColor = new Color(1f, 1f, 1f);
        _colors.selectedColor = new Color(0, 1, 1);
        slotButton.colors = _colors;
        isClicked = false;
        float _elapsedTime = 0;
        
        while (_elapsedTime <= lerpTime)
        {
            miniature.transform.position = Vector3.Lerp(_startingPos, transform.position, (_elapsedTime / lerpTime));
            _elapsedTime += Time.unscaledDeltaTime;

            yield return null;
        }
        miniature.transform.position = transform.position;
        miniature.transform.SetParent(transform);
        miniature.SetActive(false);
        yield return null;
    }
}
