using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CardHolder : MonoBehaviour
{
    public List<GameObject> spellMiniatures;

    public TextMeshProUGUI spellName;

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
            spellName.text = spellMiniatures[0].GetComponent<SpellVisuals>().spell.name + " " + spellMiniatures[0].GetComponent<CombatMiniatureProperties>().power.ToString();
        }
        else
        {
            spellName.text = "";
        }
    }

    public void RemoveFirst()
    {
        spellMiniatures[0].SetActive(false);
        spellMiniatures.RemoveAt(0);

        SetSpellName();
    }

    public void UseSpell()
    {
        GameObject _tempMiniature = new GameObject();

        _tempMiniature = spellMiniatures[0];

        CombatMiniatureProperties _tempMiniatureProperties = _tempMiniature.GetComponent<CombatMiniatureProperties>();

        _tempMiniatureProperties.cardHolder = this;

        _tempMiniatureProperties.spellLogic.Execute(_tempMiniatureProperties);
        

    }
}
