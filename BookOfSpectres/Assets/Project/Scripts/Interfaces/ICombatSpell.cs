using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatSpell
{
    CardHolder CardHolder { get; set; }

    void InitializeSpell();

    void UseSpell();
}
