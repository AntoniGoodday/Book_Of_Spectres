using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Reflection;
public class EnemyIdle : State
{

    public EnemyIdle(EnemyScript _enemy, BattlefieldScript _bfs, Animator _anim, PlayerScript _player, EnemyAI _ai) : base(_enemy, _bfs, _anim, _player, _ai)
    {
        name = STATE.IDLE;

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        if(IsOnSameRow())
        {
            //nextState = new Attack(enemy, bfs, anim, player, ai);
            ai.canBeCounteredAgain = true;
            Type stateType = Type.GetType("Attack");
            nextState = ai.customStates[stateType];
            stage = EVENT.EXIT;
        }
        else
        {
            //nextState = new MoveVertical(enemy, bfs, anim, player, ai);
            Type stateType = Type.GetType("Move");
            nextState =  ai.customStates[stateType];

            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

}
