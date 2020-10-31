using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterManager : MonoBehaviour
{
    [SerializeField]
    List<EncounterContainer> encounters;

    public static EncounterManager Instance;

    private void Start()
    {
        Instance = this;
    }

    public void GenerateEncounter(int encounterValue)
    {
        CombatEncounter _c = null;

        foreach(EncounterContainer e in encounters)
        {
            if(e.encounterGroupValue == encounterValue)
            {
                _c = e.encounterGroup[Random.Range(0, e.encounterGroup.Capacity)];
                break;
            }

        }
        if (_c != null)
        {
            BattleInfo.Instance.currentEncounter = _c;
        }
    }

    public void GenerateEncounter(CombatEncounter encounter)
    {
        BattleInfo.Instance.currentEncounter = encounter;
    }
}

[System.Serializable]
class EncounterContainer
{

    public int encounterGroupValue;

    public List<CombatEncounter> encounterGroup;

}
