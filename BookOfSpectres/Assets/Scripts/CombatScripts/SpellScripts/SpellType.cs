using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellType : ScriptableObject
{
    public string typeName;

    public virtual void OnSetType(SpellVisuals spellVisuals)
    {
        SpellElement t = BattleManager.GetResourcesManager().typeElement;

        SpellVisualsProperties type = spellVisuals.GetProperty(t);
        type.textUI.text = typeName;
    }
    
}
