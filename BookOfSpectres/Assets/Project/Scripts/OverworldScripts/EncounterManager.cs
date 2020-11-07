using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterManager : MonoBehaviour
{
    [SerializeField]
    List<EncounterContainer> encounters;

    public static EncounterManager Instance;

    public delegate void EncounterStarted();
    public static event EncounterStarted OnEncounterStart;

    public delegate void ActiveSceneChanged();
    public static event ActiveSceneChanged OnSceneChange;

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
            StartEncounter();
        }
    }

    public void GenerateEncounter(CombatEncounter encounter)
    {
        BattleInfo.Instance.currentEncounter = encounter;
        StartEncounter();
    }

    void StartEncounter()
    {
        OnEncounterStart();
        bool _sceneLoaded = false;
        if (_sceneLoaded == false)
        {
            SceneManager.LoadScene("BattleTest", LoadSceneMode.Additive);
        }

        StartCoroutine(SetActiveScene());
    }

    IEnumerator SetActiveScene()
    {
        yield return null;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("BattleTest"));
        OnSceneChange();
    }
}

[System.Serializable]
class EncounterContainer
{

    public int encounterGroupValue;

    public List<CombatEncounter> encounterGroup;

}
