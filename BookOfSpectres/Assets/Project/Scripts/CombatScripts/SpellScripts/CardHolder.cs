using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CardHolder : MonoBehaviour
{
    public List<GameObject> spellMiniatures;

    public TextMeshProUGUI spellName;

    Image background;

    private void Start()
    {
        
    }

    public void Purge()
    {
        List<GameObject> _tempSpellMiniatures = new List<GameObject>();

        foreach(GameObject g in spellMiniatures)
        {
            _tempSpellMiniatures.Add(g);
            g.SetActive(false);
        }

        foreach (GameObject g in _tempSpellMiniatures)
        {
            spellMiniatures.Remove(g);
        }
    }

    public void SetSpellName()
    {
        if (spellMiniatures.Count > 0)
        {
            background.enabled = true;
            SpellCard _tempSpell = spellMiniatures[0].GetComponent<SpellVisuals>().spell;
            if (_tempSpell.spellLogic.showPower == true)
            {
                
                spellName.text = _tempSpell.name + " " + spellMiniatures[0].GetComponent<CombatMiniatureProperties>().power.ToString();

            }
            else
            {
                spellName.text = _tempSpell.name + " ";
            }
            //spellMiniatures[0].transform.Translate(new Vector3(0.025f, -0.025f, -0.1f));
            spellMiniatures[0].GetComponent<SpellVisuals>().LoadSpell(_tempSpell);
            
        }
        else
        {
            background.enabled = false;
            spellName.text = "";
        }
    }

    public void RemoveFirst()
    {
        spellMiniatures[0].SetActive(false);
        spellMiniatures.RemoveAt(0);

        SetSpellName();
    }

    public void UseSpell(GameObject _user, EntityStatus _userStatus, Transform _shotOrigin)
    {
        GameObject _tempMiniature;

        _tempMiniature = spellMiniatures[0];

        CombatMiniatureProperties _tempMiniatureProperties = _tempMiniature.GetComponent<CombatMiniatureProperties>();

        _tempMiniatureProperties.cardHolder = this;

        _tempMiniatureProperties.spellLogic.Execute(_tempMiniatureProperties, _user, _userStatus, _shotOrigin );
        

    }

    public void Initialize()
    {
        background = GameObject.Find("SpellNameBackground").GetComponent<Image>();
        spellName = GameObject.Find("CurrentSpellName").GetComponent<TextMeshProUGUI>();
    }
}
