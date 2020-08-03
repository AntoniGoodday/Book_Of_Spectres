using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadGamestate : MonoBehaviour
{
    public static SaveLoadGamestate Instance;

    [SerializeField]
    public GameData currentGameData = new GameData();

    public delegate void SaveDelegate();
    public static event SaveDelegate SaveEvent;

    public delegate void LoadDelegate();
    public static event LoadDelegate LoadEvent;

    private void Awake()
    {
        if (Instance == null)
        {
            SaveSystem.Init();

            Instance = this;

            //GameData _tempGD = new GameData();
            //currentGameData = _tempGD;

            LoadGameState();
        }
        else
        {
            Debug.Log("Destroyed Duplicate");

            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        SaveGameState();
    }

    public void SaveGameState()
    {
        SaveEvent?.Invoke();
        //currentGameData.spellDeck.Add("h");
        string json = JsonUtility.ToJson(currentGameData, true);
        Debug.Log(json);
        SaveSystem.Save(json);
    }

    public void LoadGameState()
    {
        currentGameData = JsonUtility.FromJson<GameData>(SaveSystem.Load());
        LoadEvent?.Invoke();

    }

    public void ResetProgress()
    {
        GameData _tempGD = new GameData();
        currentGameData = _tempGD;
        string json = JsonUtility.ToJson(currentGameData, true);
        SaveSystem.Save(json);
        Application.Quit();
    }

    [System.Serializable]
    public class GameData
    {
        public PlayerData pData = new PlayerData();

        public WorldData wData = new WorldData();

    }

    public class PlayerData
    {
        public List<string> spellDeck = new List<string>();
    }

    public class WorldData
    {

    }

}
