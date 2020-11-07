using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class AxolotlStatus : EntityStatus
{
    [SerializeField]
    TextMeshPro hpText;

    [SerializeField]
    Transform visuals;

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

    public override void PlayHitAnim(int damage)
    {
        anim.Play("HitJoltRight", 2);
        if (damage > 40)
        {
            foreach (int h in HitLayers)
            {
                anim.Play("Hit", h, 0f);
            }

            EnemyScript _eScript = GetComponent<EnemyScript>();
            if (_eScript.CanBeCountered == true)
            {
                _eScript.IsInterrupted = true;
            }


        }

        

    }
}
