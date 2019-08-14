using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerStatus : EntityStatus
{
    [SerializeField]
    TextMeshProUGUI hpText;

    public override void Start()
    {
        
        hpText = GameObject.Find("PlayerHp").GetComponent<TextMeshProUGUI>();
        base.Start();
    }

    public override void UpdateUI()
    {
        hpText.text = hp.ToString();
        if(hp <= maxHp/2 && hp > maxHp/4)
        {
            hpText.color = new Color(1,0.5f,0,1);
        }
        else if(hp <= maxHp/4)
        {
            hpText.color = Color.red;
        }
    }

}
