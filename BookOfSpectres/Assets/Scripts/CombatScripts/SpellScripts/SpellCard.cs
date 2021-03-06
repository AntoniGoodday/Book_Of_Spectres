﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumScript;
[CreateAssetMenu(menuName = "SpellCard")]
public class SpellCard : ScriptableObject
{
    [SerializeField]
    public SpellLogic spellLogic;
    public SpellType spellType;
    public bool exileOnUse = false;
    public List<SpellProperties> properties;
    

    [System.Serializable]
    public class AdvancedSpellComponent
    {
        public SpellCard advancedSpell;
        public string advancedRecipe;
        public int recipeLength;
    }

    public List<AdvancedSpellComponent> advancedSpellComponents;
}
