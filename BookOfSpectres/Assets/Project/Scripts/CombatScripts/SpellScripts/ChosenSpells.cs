using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EnumScript;
using UnityEngine.UI;
using DG.Tweening;
public class ChosenSpells : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> spellMiniatures;

    public List<GameObject> combatSpells;

    public List<TextMeshProUGUI> spellAdvanceText;

    public SpellAdvance spellAdvance;

    [SerializeField]
    CanvasGroup darkness;

    ObjectPooler objectPooler;
    PlayerScript playerScript;
    CardHolder cardHolder;
    Animator canvasAnim;
    CombatMenu combatMenu;


    bool canAdvanceSpell = false;

    private void Start()
    {
        
    }

    public void LoadChosenSpells()
    {
        objectPooler = ObjectPooler.Instance;
        playerScript = PlayerScript.Instance;
        GameObject _tempCanvas = GameObject.Find("CombatCanvas");
        canvasAnim = _tempCanvas.GetComponent<Animator>();
        combatMenu = _tempCanvas.GetComponent<CombatMenu>();
        cardHolder = GameObject.Find("PlayerCanvas").GetComponent<CardHolder>();
    }


    public void AddToList(GameObject _miniature)
    {
        spellMiniatures.Add(_miniature);
    }

    public void RemoveFromList(GameObject _miniature)
    {

        GameObject _tempMiniature = _miniature;
        spellMiniatures.Remove(_miniature);
    }

    public void RemoveLastFromList()
    {

        if (spellMiniatures.Count > 0)
        {
            GameObject _tempMiniature = spellMiniatures[spellMiniatures.Count - 1];
            SpellSlot _spellSlot = _tempMiniature.GetComponent<SpellMiniature>().spellSlot;
            _spellSlot.WhenClicked(_spellSlot.transform);
        }
    }

    public void CheckAdvancedSpells()
    {
        canAdvanceSpell = false;

        List<int> _tempNumbers = new List<int>();
        int _spellsRemoved = 0;

        spellAdvance.beforeMerge = "";
        spellAdvance.afterMerge = "";
        //if (spellMiniatures.Count > 1)
        //{
        for (int i = 0; i < spellMiniatures.Count; i++)
        {
            string _tempString = "";

            string _tempComponents = "";

            List<SpellCard> _tempList = new List<SpellCard>();

            SpellCard _spellCard = spellMiniatures[i].GetComponent<SpellVisuals>().spell;

            

            if (_spellCard.advancedSpellComponents.Count == 0)
            {

                //Debug.Log(_spellCard.advancedSpellComponents.Count);

                spellAdvance.beforeMerge += _spellCard.name + " ";

                SpawnMiniature(_spellCard,false,i);

                spellAdvance.afterMerge += _spellCard.name + " ";
            }

            if (_spellCard.advancedSpellComponents.Count > 0)
            {
                if (i + _spellCard.advancedSpellComponents[0].recipeLength < spellMiniatures.Count + 1)
                {
                    
                    _tempString += _spellCard.name + " ";
                    _tempList.Add(_spellCard);
                    for (int j = 1; j < _spellCard.advancedSpellComponents[0].recipeLength; j++)
                    {
                        _tempList.Add(spellMiniatures[i + j].GetComponent<SpellVisuals>().spell);
                        //spellAdvanceText[j].gameObject.SetActive(true);
                        //spellAdvanceText[j].text = _spellCard.name;
                        _tempComponents += spellMiniatures[i + j].GetComponent<SpellVisuals>().spell.name + " ";
                        _tempString += spellMiniatures[i + j].GetComponent<SpellVisuals>().spell.name + " ";

                        int _number = i + j;
                        _tempNumbers.Add(_number);
                    }
                    if (_tempString == _spellCard.advancedSpellComponents[0].advancedRecipe + " ")
                    {
                        if (spellAdvance.gameObject.activeSelf == false)
                        {
                            spellAdvance.gameObject.SetActive(true);
                        }

                        if (canAdvanceSpell == false)
                        {
                            canAdvanceSpell = true;
                        }

                        spellAdvance.InitialSetup(_spellCard);

                        /*GameObject _spawnedMiniature = objectPooler.SpawnFromPool("CombatMiniature", Vector3.zero, Quaternion.identity, playerScript.playerCanvas.transform);
                        _spawnedMiniature.transform.localPosition = Vector3.zero;
                        _spawnedMiniature.transform.localRotation = Quaternion.Euler(Vector3.zero);
                        SpellVisuals _spawnedMiniatureSpell = _spawnedMiniature.GetComponent<SpellVisuals>();
                        _spawnedMiniatureSpell.spell = _spellCard.advancedSpellComponents[0].advancedSpell;
                        _spawnedMiniatureSpell.LoadSpell(_spellCard.advancedSpellComponents[0].advancedSpell);
                        cardHolder.spellMiniatures.Add(_spawnedMiniature);*/

                        SpawnMiniature(_spellCard, true, i);


                        spellAdvance.beforeMerge += _spellCard.name + " ";
                        spellAdvance.beforeMerge += _tempComponents;

                        spellAdvance.whichFlashing.Add(i);
                        foreach (int num in _tempNumbers)
                        {
                            spellAdvance.whichFlashing.Add(num);
                        }


                        spellAdvance.whichFlashingAfter.Add(i - _spellsRemoved);
                        _spellsRemoved += _spellCard.advancedSpellComponents[0].recipeLength - 1;

                        spellAdvance.afterMerge += _spellCard.advancedSpellComponents[0].advancedSpell.name + " ";

                        i += _spellCard.advancedSpellComponents[0].recipeLength - 1;
                        _tempString = "";

                        foreach (SpellCard s in _tempList)
                        {
                            combatMenu.MoveCardToDestination(s, CardDestination.Hand, CardDestination.Graveyard);
                        }
                    }
                    else
                    {
                        spellAdvance.beforeMerge += _spellCard.name + " ";

                        SpawnMiniature(_spellCard, false, i);

                        spellAdvance.afterMerge += _spellCard.name + " ";
                    }
                }
                else
                {
                    spellAdvance.beforeMerge += _spellCard.name + " ";

                    SpawnMiniature(_spellCard, false, i);

                    spellAdvance.afterMerge += _spellCard.name + " ";
                }

                

                /*if (_spellCard.advancedSpellComponents.Count > 0)
                {

                    spellAdvance.beforeMerge += _spellCard.name + " ";
                    Debug.Log(spellAdvance.beforeMerge);

                    SpawnMiniature(_spellCard);

                    spellAdvance.afterMerge += _spellCard.name + " ";

                }*/
            }

            

        }
        //}
        List<GameObject> _tempMiniatures = new List<GameObject>();

        foreach(GameObject sm in spellMiniatures)
        {
            _tempMiniatures.Add(sm);
        }

        foreach(GameObject sm in _tempMiniatures)
        {
            sm.GetComponent<SpellMiniature>().spellSlot.StartCoroutine("LerpMovementBack");
        }

        //spellAdvance.SetSpellNames();
        if (canAdvanceSpell == true)
        {
            DOTween.Init();

            Sequence tweenSequence = DOTween.Sequence();

            CanvasGroup _visual = spellAdvance.GetComponent<CanvasGroup>();

            tweenSequence.Append(DOTween.To(() => _visual.alpha, x => _visual.alpha = x, 1, 0.5f))
                .Join(DOTween.To(() => darkness.alpha, x => darkness.alpha = x, 0.5f, 0.5f))
                .OnComplete(() => spellAdvance.SetSpellNames())
                .SetUpdate(true)
                .Play();
            //canvasAnim.Play("SpellAdvance");
        }
        else
        {
            TurnBarScript.Instance.UnPause();
            cardHolder.SetSpellName();
            combatMenu.MenuUnPause();
        }

        
    }

    void SpawnMiniature(SpellCard _spellCard, bool _loadAdvancedSpell = false, int offset = 0)
    {
        
        GameObject _spawnedMiniature = objectPooler.SpawnFromPool("CombatMiniature", Vector3.zero , Quaternion.identity, playerScript.playerCanvas.transform);
        _spawnedMiniature.transform.localPosition = Vector3.forward * (offset * 0.01f);
        _spawnedMiniature.transform.localRotation = Quaternion.Euler(Vector3.zero);
        SpellVisuals _spawnedMiniatureSpell = _spawnedMiniature.GetComponent<SpellVisuals>();

        CombatMiniatureProperties _miniatureProperties = _spawnedMiniature.GetComponent<CombatMiniatureProperties>();

        _spawnedMiniature.GetComponent<SpriteRenderer>().sprite = null;

        //Load combat spell visuals and logic
        if (_loadAdvancedSpell == false)
        {
            
            _spawnedMiniatureSpell.spell = _spellCard;
            _spawnedMiniatureSpell.LoadSpell(_spellCard);

            combatMenu.MoveCardToDestination(_spellCard, CardDestination.Hand, CardDestination.Combat);
        }
        else
        {
            SpellCard _tempAdvancedSpell = _spellCard.advancedSpellComponents[0].advancedSpell;

            _spawnedMiniatureSpell.spell = _tempAdvancedSpell;
            _spawnedMiniatureSpell.LoadSpell(_tempAdvancedSpell);

            combatMenu.MoveCardToDestination(_tempAdvancedSpell, CardDestination.Hand, CardDestination.Combat);
        }

        _miniatureProperties.OnSpellLogicChange(_spawnedMiniature.GetComponent<SpellVisuals>().spell.spellLogic);

        cardHolder.spellMiniatures.Add(_spawnedMiniature);
    }

    void CreateAdvancedSpell()
    {

    }

}
