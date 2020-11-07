using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Reflection;
public class EnemyIdle : State
{
    [SerializeField] private State moveState;
    [SerializeField] private State attackState;
    /*public EnemyIdle(EnemyScript _enemy, BattlefieldScript _bfs, Animator _anim, PlayerScript _player, EnemyAI _ai) : base(_enemy, _bfs, _anim, _player, _ai)
    {
        name = STATE.IDLE;

    }*/

    public override void Enter()
    {
        base.Enter();
    }

    public override void Tick()
    {
        if (!ai.wait)
        {
            if(ai.entityInput.attack)
            {
                if(ai.hasAttackToken)
                {
                    ai.canBeCounteredAgain = true;
                    ai.ChangeState(attackState);
                }
            }
            else if(ai.entityInput.moveUp || ai.entityInput.moveDown)
            {
                ai.ChangeState(moveState);
            }
            /*if (IsOnSameRow())
            {
                if (ai.hasAttackToken)
                {
                    //nextState = new Attack(enemy, bfs, anim, player, ai);
                    ai.canBeCounteredAgain = true;
                    ai.ChangeState(attackState);
                    //Type stateType = Type.GetType("Attack");
                    //nextState = ai.customStates[stateType];
                    //stage = EVENT.EXIT;
                }
            }
            else
            {
                //nextState = new MoveVertical(enemy, bfs, anim, player, ai);
                //Type stateType = Type.GetType("Move");
                //nextState = ai.customStates[stateType];

                //nextState = moveState;

                //stage = EVENT.EXIT;
                ai.ChangeState(moveState);
            }*/
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

}
