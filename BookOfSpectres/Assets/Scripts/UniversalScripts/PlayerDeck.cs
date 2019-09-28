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
        }
        else
        {
            Destroy(this);
        }
    }
}
