using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumScript;
public class CombatMiniatureProperties : MonoBehaviour
{
    public CardHolder cardHolder;

    public SpellLogic spellLogic;

    public int power;

    public List<StatusEffect> statusEffects;

    public CombatMenu combatMenu;

    private void Start()
    {
        combatMenu = GameObject.Find("Canvas").GetComponent<CombatMenu>();
    }

    public void OnSpellLogicChange(SpellLogic _spellLogic)
    {
        spellLogic = _spellLogic;
        power = spellLogic.power;
    }

    public void UnPaused()
    {

    }

    public void Paused()
    {
        
    }

}
