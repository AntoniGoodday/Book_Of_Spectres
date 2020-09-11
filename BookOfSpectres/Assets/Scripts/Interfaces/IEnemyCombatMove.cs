using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyCombatMove
{
    int MovementRange { get; set; }
    float MovementSpeed { get; set; }
    float HeightAboveGround { get; set; }

    void Move();
}
