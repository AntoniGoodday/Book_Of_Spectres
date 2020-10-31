using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(menuName = "StandardEncounterObject")]
public class CombatEncounter : ScriptableObject
{
    [SerializeField]
    List<EnemyWave> enemyWaves = new List<EnemyWave>();
    [SerializeField]
    string flavourText = "";
}
