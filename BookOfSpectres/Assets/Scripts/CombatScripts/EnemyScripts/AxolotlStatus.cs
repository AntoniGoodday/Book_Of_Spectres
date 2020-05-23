using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AxolotlStatus : EntityStatus
{
    [SerializeField]
    TextMeshPro hpText;

    public override void Start()
    {

        hpText = transform.GetComponentInChildren<TextMeshPro>();
        base.Start();
        hpText.sortingOrder = 6;
    }

    public override void UpdateUI()
    {
        hpText.text = hp.ToString();
        if (hp <= maxHp / 2 && hp > maxHp / 4)
        {
            hpText.color = new Color(1, 0.5f, 0, 1);
        }
        else if (hp <= maxHp / 4)
        {
            hpText.color = Color.red;
        }
    }
}
