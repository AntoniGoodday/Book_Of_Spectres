using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellLogic : ScriptableObject
{
    public int power;

    public abstract void Execute(CombatMiniatureProperties _properties);
    
}
