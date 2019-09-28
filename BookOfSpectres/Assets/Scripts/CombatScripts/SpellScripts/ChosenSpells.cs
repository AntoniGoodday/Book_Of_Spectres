using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ChosenSpells : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> spellMiniatures;

    public List<GameObject> combatSpells;

    public List<TextMeshProUGUI> spellAdvanceText;

    public SpellAdvance spellAdvance;

    ObjectPooler objectPooler;
    PlayerScript playerScript;
    CardHolder cardHolder;
    Animator canvasAnim;

    bool canAdvanceSpell = false;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        playerScript = PlayerScript.Instance;
        canvasAnim = GameObject.Find("Canvas").GetComponent<Animator>();
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
        List<SpellCard> _tempList = new List<SpellCard>();
        List<int> _tempNumbers = new List<int>();
        int _spellsRemoved = 0;
        if (spellMiniatures.Count > 1)
        {
            for (int i = 0; i < spellMiniatures.Count; i++)
            {
                string _tempString = "";

                string _tempComponents = "";

                
                
                SpellCard _spellCard = spellMiniatures[i].GetComponent<SpellVisuals>().spell;

                

                if(_spellCard.advancedSpellComponents.Count > 0)
                {
                    if (i + _spellCard.advancedSpellComponents[0].recipeLength < spellMiniatures.Count + 1)
                    {
                        _tempString += _spellCard.name + " ";
                        for (int j = 1; j < _spellCard.advancedSpellComponents[0].recipeLength; j++)
                        {
                            //_tempList.Add(_spellCard);
                            //spellAdvanceText[j].gameObject.SetActive(true);
                            //spellAdvanceText[j].text = _spellCard.name;
                            _tempComponents += spellMiniatures[i + j].GetComponent<SpellVisuals>().spell.name + " ";
                            _tempString += spellMiniatures[i + j].GetComponent<SpellVisuals>().spell.name + " ";

                            int _number = i + j;
                            _tempNumbers.Add(_number);
                        }
                    }
                }
                else
                {
                    
                }
                if (_spellCard.advancedSpellComponents.Count > 0)
                {
                    if (_tempString == _spellCard.advancedSpellComponents[0].advancedRecipe + " ")
                    {
                        if (spellAdvance.gameObject.activeSelf == false)
                        {
                            spellAdvance.gameObject.SetActive(true);
                        }

                        if(canAdvanceSpell == false)
                        {
                            canAdvanceSpell = true;
                        }

                        spellAdvance.InitialSetup(_spellCard);
                        GameObject _spawnedMiniature = objectPooler.SpawnFromPool("CombatMiniature", Vector3.zero, Quaternion.identity, playerScript.playerCanvas.transform);
                        _spawnedMiniature.transform.localPosition = Vector3.zero;
                        _spawnedMiniature.transform.localRotation = Quaternion.Euler(Vector3.zero);
                        SpellVisuals _spawnedMiniatureSpell = _spawnedMiniature.GetComponent<SpellVisuals>();
                        _spawnedMiniatureSpell.spell = _spellCard.advancedSpellComponents[0].advancedSpell;
                        _spawnedMiniatureSpell.LoadSpell(_spellCard.advancedSpellComponents[0].advancedSpell);
                        cardHolder.spellMiniatures.Add(_spawnedMiniature);


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
                    }
                    else
                    {
                        spellAdvance.beforeMerge += _spellCard.name + " ";

                        GameObject _spawnedMiniature = objectPooler.SpawnFromPool("CombatMiniature", Vector3.zero, Quaternion.identity, playerScript.playerCanvas.transform);
                        _spawnedMiniature.transform.localPosition = Vector3.zero;
                        _spawnedMiniature.transform.localRotation = Quaternion.Euler(Vector3.zero);
                        SpellVisuals _spawnedMiniatureSpell = _spawnedMiniature.GetComponent<SpellVisuals>();
                        _spawnedMiniatureSpell.spell = _spellCard;
                        _spawnedMiniatureSpell.LoadSpell(_spellCard);
                        cardHolder.spellMiniatures.Add(_spawnedMiniature);

                        spellAdvance.afterMerge += _spellCard.name + " ";
                    }

                }
                else
                {
                    spellAdvance.beforeMerge += _spellCard.name + " ";

                    GameObject _spawnedMiniature = objectPooler.SpawnFromPool("CombatMiniature", Vector3.zero, Quaternion.identity, playerScript.playerCanvas.transform);
                    _spawnedMiniature.transform.localPosition = Vector3.zero;
                    _spawnedMiniature.transform.localRotation = Quaternion.Euler(Vector3.zero);
                    SpellVisuals _spawnedMiniatureSpell = _spawnedMiniature.GetComponent<SpellVisuals>();
                    _spawnedMiniatureSpell.spell = _spellCard;
                    _spawnedMiniatureSpell.LoadSpell(_spellCard);
                    cardHolder.spellMiniatures.Add(_spawnedMiniature);

                    spellAdvance.afterMerge += _spellCard.name + " ";
                }

            }
        }

        
        //spellAdvance.SetSpellNames();
        if (canAdvanceSpell == true)
        {
            canvasAnim.Play("SpellAdvance");
        }
        else
        {
            objectPooler.UnPauseAll();
        }
        
    }

}
