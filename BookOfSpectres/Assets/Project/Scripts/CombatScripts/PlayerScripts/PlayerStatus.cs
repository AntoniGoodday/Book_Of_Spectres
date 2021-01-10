﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EnumScript;
using UnityEngine.SceneManagement;
public class PlayerStatus : EntityStatus
{
    [SerializeField]
    TextMeshProUGUI hpText;

    PlayerScript playerScript;

    Animator vCamAnim;

    Animator canvasAnim;

    CombatMenu combatMenu;

    [SerializeField]
    GameObject zoomInCam;

    public delegate void HitDelegate();
    public event HitDelegate hitEvent;

    public override void Start()
    {
        combatMenu = CombatMenu.Instance;

        CombatMenu.OnMenuUnpaused += UnPaused;
        CombatMenu.OnMenuPaused += Paused;

    }

    public override void DealDamage(int damage, float zPos = -1.4f, float amplitudeModifier = 0.05f, BulletAlignement damageSource = BulletAlignement.Enemy)
    {
        if (damage > 0)
        {
            hitEvent?.Invoke();
        }
        base.DealDamage(damage, zPos, amplitudeModifier, damageSource);
    }

    public override void PlayHitAnim(int damage)
    {
        foreach (int h in HitLayers)
        {
            anim.Play("Hit", h, 0f);
        }
    }

    public override void Die()
    {
        
        anim.Play("Die");
        vCamAnim.Play("PlayerDie");
        //canvasAnim.Play("FadeOut", 2);
        combatMenu.FadeOut();
        IsDying = true;
        playerScript.playerSprite.sortingOrder = 10;
        vCamAnim.gameObject.transform.GetComponentInChildren<Renderer>().sortingOrder = 9;
        zoomInCam.SetActive(true);
        StartCoroutine("RestartGame");
    }

    public override void UpdateUI()
    {
        if(hpText == null)
        {
            hpText = GameObject.Find("PlayerHp").GetComponent<TextMeshProUGUI>();
        }

        hpText.text = hp.ToString();
        if(hp >= maxHp/2)
        {
            hpText.color = new Color(0, 0, 0, 1);
            vCamAnim.SetBool("isLowHp", false);
        }
        else if(hp <= maxHp/2 && hp > maxHp/4)
        {
            hpText.color = new Color(1,0.5f,0,1);
            vCamAnim.SetBool("isLowHp", false);
        }
        else if(hp <= maxHp/4)
        {
            hpText.color = Color.red;
            vCamAnim.SetBool("isLowHp", true);
        }
        else
        {
            vCamAnim.SetBool("isLowHp", false);
        }
    }
    public override void ProgressWave()
    {
        //playerScript.Paused();
        IsPaused = true;
    }

    ////DELETE LATER
    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(0);
    }

    public override void EnemyStart()
    {
        aiMastermind.player = gameObject;
    }

    public override void Initialize()
    {
        playerScript = PlayerScript.Instance;
        hpText = GameObject.Find("PlayerHp").GetComponent<TextMeshProUGUI>();
        hpText.text = hp.ToString();
        if (vCamAnim == null)
        {
            vCamAnim = GameObject.Find("MainVirtualCam").GetComponent<Animator>();
            vCamAnim.gameObject.transform.GetComponentInChildren<Renderer>().sortingOrder = -10;
        }
        canvasAnim = GameObject.Find("CombatCanvas").GetComponent<Animator>();

        base.Start();
    }
}