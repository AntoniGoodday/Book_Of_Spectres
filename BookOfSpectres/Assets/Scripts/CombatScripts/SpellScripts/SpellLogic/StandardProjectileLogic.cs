using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumScript;
[CreateAssetMenu(menuName = "SpellLogic/StandardProjectile")]
public class StandardProjectileLogic : SpellLogic
{

    public override void Execute(CombatMiniatureProperties _properties)
    {
        Debug.Log("Power: " + _properties.power);
        SpellCard _tempSpell = _properties.gameObject.GetComponent<SpellVisuals>().spell;
        if (_tempSpell.exileOnUse == false)
        {
            _properties.combatMenu.MoveCardToDestination(_tempSpell, CardDestination.Combat, CardDestination.Graveyard);
        }
        else
        {
            _properties.combatMenu.MoveCardToDestination(_tempSpell, CardDestination.Combat, CardDestination.Exile);
        }
        _properties.cardHolder.RemoveFirst();
    }
}
