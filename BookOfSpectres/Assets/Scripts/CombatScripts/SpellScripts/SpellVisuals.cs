using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SpellVisuals : MonoBehaviour
{
    

    public SpellCard spell;
    public List<SpellVisualsProperties> properties;
    public GameObject damageHolder;
    public bool requiresType = true;

    private void Start()
    {
        LoadSpell(spell);
    }

    public void LoadSpell(SpellCard s)
    {
        if (s == null)
            return;

        spell = s;

        if (requiresType == true)
        {
            s.spellType.OnSetType(this);
        }

        for (int i = 0; i < s.properties.Count; i++)
        {
            SpellProperties sp = s.properties[i];

            SpellVisualsProperties p = GetProperty(sp.spellElement);
            if(p == null)
            {
                continue;
            }

            if(sp.spellElement is SpellElementInt)
            {
                p.textUI.text = sp.intValue.ToString();
            }
            else if(sp.spellElement is SpellElementText)
            {
                p.textUI.text = sp.stringValue;
            }
            else if(sp.spellElement is SpellElementImage)
            {
                p.img.sprite = sp.sprite;
            }
            else if (sp.spellElement is SpellElementSprite)
            {
                p.spr.sprite = sp.sprite;
            }
            else if (sp.spellElement is SpellElementCombatText)
            {
                p.text.text = sp.stringValue;
            }
        }
        
    }

    public SpellVisualsProperties GetProperty(SpellElement e)
    {
        SpellVisualsProperties result = null;

        for (int i = 0; i < properties.Count; i++)
        {
            if(properties[i].element == e)
            {
                result = properties[i];
                break;
            }
        }


        return result;
    }

}
