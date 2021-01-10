using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(menuName = "StandardEncounterObject")]
public class CombatEncounter : ScriptableObject
{
    [SerializeField]
    public List<EnemyWave> enemyWaves = new List<EnemyWave>();
    [SerializeField]
    GameObject playerAvatar;
    [SerializeField]
    public string flavourText = "";
}
