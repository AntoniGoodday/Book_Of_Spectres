using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CounterState : State
{
    public CounterState(EnemyScript _enemy, BattlefieldScript _bfs, Animator _anim, PlayerScript _player, EnemyAI _ai) : base(_enemy, _bfs, _anim, _player, _ai)
    {
        name = STATE.COUNTER;
    }

    public override void Enter()
    {
        ai.isInCounterState = true;

        anim.Play("DefaultState", 1);
        enemy.CanBeCountered = false;

        enemy.CounterFlashes();
        base.Enter();
    }

    public override void Update()
    {
        if(ai.isInCounterState == false)
        {
            //nextState = previousState;
            Type stateType = Type.GetType("Attack");
            nextState = ai.customStates[stateType];
            stage = EVENT.EXIT;
        }
        //base.Update();
    }

    public override void Exit()
    {

        base.Exit();
    }


}
