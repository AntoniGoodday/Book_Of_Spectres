using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityInputManager : MonoBehaviour
{
    [Header("Movement Actions")]
    public bool moveUp;

    public bool moveDown;

    public bool moveLeft;

    public bool moveRight;

    public Vector2 movementVector;

    [Header("Attack Actions")]

    public bool attack;
    public float attackHeldTime;

    public bool strongAttack;
    public float strongAttackHeldTime;
}
