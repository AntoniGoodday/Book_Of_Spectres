using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : State
{
    public bool processing = false;
    public int _movementX;
    public int _movementY;

    [SerializeField] private State cooldownState;
    /*public Move(EnemyScript _enemy, BattlefieldScript _bfs, Animator _anim, PlayerScript _player, EnemyAI _ai) : base(_enemy, _bfs, _anim, _player, _ai)
    {
        name = STATE.MOVE;
    }*/


    public override void Enter()
    {
        base.Enter();
        processing = false;
        entity.IsMoving = true;
        _movementX = (int)ai.entityInput.movementVector.x * entity.movementRange;
        _movementY = (int)ai.entityInput.movementVector.y * entity.movementRange;
    }

    public override void Tick()
    {
       
    }

    public override void Exit()
    {
        base.Exit();
    }

    public void Cooldown()
    {
        ai.ChangeState(cooldownState);
        //nextState = cooldownState;
        //nextState.stage = EVENT.ENTER;
        //stage = EVENT.EXIT;
    }
}
