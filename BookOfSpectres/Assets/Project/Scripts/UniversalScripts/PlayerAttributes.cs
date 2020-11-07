using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    public int handSize;
    public int startingColourlessMana;
    public int maxMana;
    public int manaRegen;
    public static PlayerAttributes Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
