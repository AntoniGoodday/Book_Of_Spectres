using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : State
{
    public bool processing = false;

    [SerializeField] private State cooldownState;
    /*public Move(EnemyScript _enemy, BattlefieldScript _bfs, Animator _anim, PlayerScript _player, EnemyAI _ai) : base(_enemy, _bfs, _anim, _player, _ai)
    {
        name = STATE.MOVE;
    }*/


    public override void Enter()
    {
        base.Enter();
        processing = false;
        enemy.IsMoving = true;
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
