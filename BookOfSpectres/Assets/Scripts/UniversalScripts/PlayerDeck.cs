using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDeck : MonoBehaviour
{
    [System.Serializable]
    public class DeckCardEntry
    {
        public SpellCard spell;
        public int amount;
    }

    public static PlayerDeck Instance;

    public List<SpellCard> pDeck = new List<SpellCard>();

    

    private void Start()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;

            LoadFromSave();
            SaveLoadGamestate.LoadEvent += LoadFromSave;
            SaveLoadGamestate.SaveEvent += Save;
            
        }
        else
        {
            Destroy(this);
        }
    }

    void LoadFromSave()
    {
        pDeck.Clear();
        foreach(string s in SaveLoadGamestate.Instance.currentGameData.pData.spellDeck)
        {
            SpellCard spell = (SpellCard)(Resources.Load("SpellCards/" + s));
            pDeck.Add(spell);
        }
    }

    void Save()
    {
        GameObject _pStats = GameObject.Find("PlayerStats");
        List<string> _spelldeck = _pStats.GetComponent<SaveLoadGamestate>().currentGameData.pData.spellDeck;
        _spelldeck.Clear();
        foreach (SpellCard s in pDeck)
        {
            _spelldeck.Add(s.name);
        }
    }
}
