using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInfo : MonoBehaviour
{

    public static BattleInfo Instance;
    public CombatEncounter currentEncounter;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
