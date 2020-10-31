using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CombatStart : State
{
    [SerializeField] private State postStartState;
    // Start is called before the first frame update
    /*public CombatStart(EnemyScript _enemy, BattlefieldScript _bfs, Animator _anim, PlayerScript _player, EnemyAI _ai) : base(_enemy, _bfs, _anim, _player, _ai)
    {
        name = STATE.COMBAT_START;
    }*/

    public override void Enter()
    {
        anim.Play("StartCombat");
        Debug.Log("CombatStart Entered");
        previousState = this;

        base.Enter();
    }

    public override void Tick()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= anim.GetCurrentAnimatorStateInfo(0).length - 0.01f)
        {

            //nextState = new EnemyIdle(enemy, bfs, anim, player, ai);
            //Type stateType = Type.GetType("EnemyIdle");
            //nextState = ai.customStates[stateType];
            //nextState = postStartState;

            //stage = EVENT.EXIT;

            ai.ChangeState(postStartState);
        }
    }

    public override void Exit()
    {
        anim.SetTrigger("CombatStarted");
        Debug.Log("CombatStart Exited");
        base.Exit();
    }
}
