using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpellType/Standard")]
public class StandardSpell : SpellType
{
    public override void OnSetType(SpellVisuals spellVisuals)
    {
        base.OnSetType(spellVisuals);

        spellVisuals.damageHolder.SetActive(true);
    }
}
