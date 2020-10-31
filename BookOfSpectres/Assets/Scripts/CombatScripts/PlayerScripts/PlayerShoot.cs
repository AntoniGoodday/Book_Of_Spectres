using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using System;

public class PlayerShoot : MonoBehaviour, ICombatShoot
{
    public delegate void ShootDelegate();
    //public event ShootDelegate ShootEvent;
    Action shootEvent;
    public delegate void ChargedShootDelegate();
    Action chargedShootEvent;
    //public event ShootDelegate ChargedShootEvent;
    Action ICombatShoot.ShootEvent { get => shootEvent; set => shootEvent = value; }
    Action ICombatShoot.ChargedShootEvent { get => chargedShootEvent; set => chargedShootEvent = value; }


    [SerializeField]
    float maxShotChargeTime = 2;
    [SerializeField]
    float shotChargeAmount = 0;
    [SerializeField]
    bool shotFullyCharged = false;

    [SerializeField]
    StandardPlayerShot standardShot;
    [SerializeField]
    ChargedPlayerShot chargedShot;

    [SerializeField]
    bool isCharging = false;
    bool isHeld = false;
    bool canShoot = true;

    private bool pauseHold = false;

    PlayerScript playerScript;
    PlayerStatus status;

    EntityInputManager inputManager;

    public float MaxShotChargeTime { get => maxShotChargeTime; set => maxShotChargeTime = value; }
    public float ShotChargeAmount { get => shotChargeAmount; set => shotChargeAmount = value; }
    public bool ShotFullyCharged { get => shotFullyCharged; set => shotFullyCharged = value; }
    public bool PauseHold { get => pauseHold; set => pauseHold = value; }
    public bool IsHeld { get => isHeld; set => isHeld = value; }
    public bool CanShoot { get => canShoot; set => canShoot = value; }
    public bool IsCharging { get => isCharging; set => isCharging = value; }

    private void Start()
    {
        playerScript = GetComponent<PlayerScript>();
        status = GetComponent<PlayerStatus>();
        inputManager = this.GetComponent<EntityInputManager>();

        standardShot = GetComponent<StandardPlayerShot>();
        chargedShot = GetComponent<ChargedPlayerShot>();
    }


    public void ChargeUpdate()
    {
        if(inputManager.attack)
        {
            IsCharging = true;

            if (inputManager.attackHeldTime < maxShotChargeTime && inputManager.attackHeldTime > 0.05f)
            {
                if (playerScript.chargeParticles[0].isPlaying == false)
                {
                    playerScript.chargeParticles[0].Play();
                }
            }
            else if (inputManager.attackHeldTime >= maxShotChargeTime && shotFullyCharged == false)
            {

                playerScript.chargeParticles[0].Stop();
                playerScript.chargeParticles[1].Play();
                shotFullyCharged = true;

            }
        }
        else if(!inputManager.attack && IsCharging == true)
        {
            Shoot();
            IsCharging = false;
        }
    }

    public void Charge()
    {
        isHeld = true;
        if (!status.IsPaused)
        {
            IsCharging = true;
        }
    }

    public void Shoot()
    {
        isHeld = false;
        if(playerScript.IsLerping)
        {
            playerScript.ClearChargeParticles();
            playerScript.BufferedAttack = true;
            return;
        }

        if (!status.IsPaused)
        {
            if (!shotFullyCharged)
            {
                IsCharging = false;
                ShootBasic();
                shotChargeAmount = 0;

            }
            else
            {
                IsCharging = false;
                ShootCharged();
                shotChargeAmount = 0;
            }
            playerScript.ClearChargeParticles();
        }
        else
        {
            PauseHold = true;
        }
    }

    void ShootCharged()
    {
        chargedShot.ShootCharged(playerScript.spellOrigin[0].transform, gameObject);
        shotFullyCharged = false;

        playerScript.anim.Play("Spell");

        playerScript.spellParticles.Play();

        shootEvent?.Invoke();

        chargedShootEvent?.Invoke();
    }

    void ShootBasic()
    {

         playerScript.ClearChargeParticles();

         standardShot.Shoot(playerScript.spellOrigin[0].transform, gameObject);

         playerScript.anim.Play("Attack");

         shootEvent?.Invoke();

         shotChargeAmount = 0;

         StartCoroutine(ShotDelay());

    }

    IEnumerator ShotDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(0.2f);
        canShoot = true;
    }


}
