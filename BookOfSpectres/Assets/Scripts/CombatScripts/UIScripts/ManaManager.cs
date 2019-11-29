using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EnumScript;
public class ManaManager : MonoBehaviour
{
    PlayerAttributes playerAttributes;
    public List<ManaScript> manaType;
    private void Awake()
    {
        playerAttributes = PlayerAttributes.Instance;
    }
    private void Start()
    {
        foreach (ManaScript m in manaType)
        {
            Debug.Log(m.gameObject.name);
            if (m.gameObject.name == SpellColour.Colourless.ToString())
            {
                m.InitialSetText(PlayerAttributes.Instance.startingColourlessMana,PlayerAttributes.Instance.maxMana);

            }
        }
    }
    public bool CheckMana(int _amount)
    {
        if(_amount <= manaType[0].currentAmount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void UseMana(int _amount)
    {
        manaType[0].currentAmount -= _amount;
        manaType[0].SetText();
    }
}
