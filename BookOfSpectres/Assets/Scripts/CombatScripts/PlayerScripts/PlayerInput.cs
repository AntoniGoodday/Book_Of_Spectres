using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayerControlNamespace;
using System;

public class PlayerInput : MonoBehaviour
{
    EntityInputManager entityInput;

    PlayerControl playerControl;

    PlayerControl.DefaultControlsActions playerInput;

    private void Awake()
    {
        playerControl = new PlayerControl();
    }

    // Start is called before the first frame update
    void Start()
    {

        entityInput = this.GetComponent<EntityInputManager>();
        
        playerInput =  playerControl.DefaultControls;

        playerInput.Shoot.started += context => AttackStart();
        playerInput.Shoot.canceled += context => AttackRelease();

        playerInput.Spell.started += context => SpellStart();
        playerInput.Spell.canceled += context => SpellRelease();
    }

    private void OnEnable()
    {
        playerControl.Enable();
    }

    private void OnDisable()
    {
        playerControl.Disable();
    }


    // Update is called once per frame
    void Update()
    {
        Movement();

        HoldTimes();
    }

    void Movement()
    {
        Vector2 _movementInput = playerInput.Move.ReadValue<Vector2>();

        entityInput.movementVector = _movementInput;

        if(_movementInput.y > 0)
        {
            entityInput.moveUp = true;
            entityInput.moveDown = false;
        }
        else if(_movementInput.y < 0)
        {
            entityInput.moveDown = true;
            entityInput.moveUp = false;
        }
        else
        {
            entityInput.moveUp = false;
            entityInput.moveDown = false;
        }
    }

    void HoldTimes()
    {
        if (!ObjectPooler.Instance.isPaused)
        {
            if (entityInput.attack == true)
            {
                entityInput.attackHeldTime += Time.deltaTime;
            }
            else
            {
                entityInput.attackHeldTime = 0;
            }

            if(entityInput.strongAttack == true)
            {
                entityInput.strongAttackHeldTime += Time.deltaTime;
            }
        }
    }

    void AttackStart()
    {
        entityInput.attack = true;
    }

    void AttackRelease()
    {
        entityInput.attack = false;
    }

    void SpellStart()
    {
        entityInput.strongAttack = true;
    }

    void SpellRelease()
    {
        entityInput.strongAttack = false;
    }

    /*private void OnDisable()
    {
        playerInput.Shoot.started -= context => AttackStart();
        playerInput.Shoot.performed -= context => AttackRelease();
    }*/
}
