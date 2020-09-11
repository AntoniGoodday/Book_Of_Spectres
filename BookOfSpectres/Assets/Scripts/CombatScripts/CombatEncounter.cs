using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(menuName = "StandardEncounterObject")]
public class CombatEncounter : ScriptableObject
{
    List<EnemyWave> enemyWaves = new List<EnemyWave>();
    string flavourText = "";
}
