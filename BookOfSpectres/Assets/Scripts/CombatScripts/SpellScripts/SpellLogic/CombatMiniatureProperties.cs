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

    [SerializeField]
    SpellVisuals sv;
    [SerializeField]
    SpriteRenderer sr;

    private void Awake()
    {
        //sr = gameObject.GetComponent<SpriteRenderer>();
        //sr.sprite = null;
        
    }

    private void Start()
    {
        
        combatMenu = GameObject.Find("Canvas").GetComponent<CombatMenu>();
    }

    public void OnSpellLogicChange(SpellLogic _spellLogic)
    {
        spellLogic = _spellLogic;
        
        power = spellLogic.power[0];
        
        
    }

    public void UnPaused()
    {
       
    }

    public void Paused()
    {
        
    }

}
