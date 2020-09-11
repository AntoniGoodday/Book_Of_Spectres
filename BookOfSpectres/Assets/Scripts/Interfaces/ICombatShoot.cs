using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatShoot
{


    float MaxShotChargeTime { get; set; }
    float ShotChargeAmount { get; set; }
    bool ShotFullyCharged { get; set; }
    bool PauseHold { get; set; }
    bool IsHeld { get; set; }
    bool CanShoot { get; set; }
    Action ShootEvent { get; set; }
    Action ChargedShootEvent { get; set; }

    void Charge();
    void Shoot();
    void ChargeUpdate();
}
