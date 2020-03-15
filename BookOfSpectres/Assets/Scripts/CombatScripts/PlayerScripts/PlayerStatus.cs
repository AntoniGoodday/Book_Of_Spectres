using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerStatus : EntityStatus
{
    [SerializeField]
    TextMeshProUGUI hpText;

    PlayerScript playerScript;

    Animator vCamAnim;

    Animator canvasAnim;

    [SerializeField]
    GameObject zoomInCam;

    public override void Start()
    {
        playerScript = PlayerScript.Instance;
        hpText = GameObject.Find("PlayerHp").GetComponent<TextMeshProUGUI>();
        if(vCamAnim == null)
        {
            vCamAnim = GameObject.Find("MainVirtualCam").GetComponent<Animator>();
            vCamAnim.gameObject.transform.GetComponentInChildren<Renderer>().sortingOrder = -10;
        }
        canvasAnim = GameObject.Find("Canvas").GetComponent<Animator>();
        base.Start();
        
    }

    public override void Die()
    {
        
        anim.Play("Die");
        vCamAnim.Play("PlayerDie");
        canvasAnim.Play("FadeOut", 2);
        playerScript.playerSprite.sortingOrder = 10;
        vCamAnim.gameObject.transform.GetComponentInChildren<Renderer>().sortingOrder = 9;
        zoomInCam.SetActive(true);
        StartCoroutine("RestartGame");
    }

    public override void UpdateUI()
    {
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
        playerScript.isPaused = true;
    }

    ////DELETE LATER
    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(10);
        Application.LoadLevel(0);
    }

    public override void EnemyStart()
    {
        aiMastermind.player = gameObject;
    }
}
