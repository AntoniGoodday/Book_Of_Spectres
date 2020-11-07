using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpellType/Utility")]
public class UtilitySpell : SpellType
{
    public override void OnSetType(SpellVisuals spellVisuals)
    {
        base.OnSetType(spellVisuals);
        spellVisuals.damageHolder.SetActive(false);
    }
}
