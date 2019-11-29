using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellLogic : ScriptableObject
{
    public int mana;
    public int power;

    public abstract void Execute(CombatMiniatureProperties _properties, GameObject _user, EntityStatus _userStatus, Transform _shotOrigin);
    
}
