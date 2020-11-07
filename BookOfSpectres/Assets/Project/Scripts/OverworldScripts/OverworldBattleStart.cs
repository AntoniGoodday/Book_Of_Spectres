using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldBattleStart : MonoBehaviour
{
    [SerializeField]
    int encounterValue;

    [SerializeField]
    CombatEncounter setEncounter = null;

    private void OnTriggerEnter(Collider other)
    {
        StartEncounter();
    }

    void StartEncounter()
    {
        if(setEncounter != null)
        {
            EncounterManager.Instance.GenerateEncounter(setEncounter);
            return;
        }

        EncounterManager.Instance.GenerateEncounter(encounterValue);


    }
}
