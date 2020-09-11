using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class StartCombat : State
{
    // Start is called before the first frame update
    public StartCombat(EnemyScript _enemy, BattlefieldScript _bfs, Animator _anim, PlayerScript _player, EnemyAI _ai) : base(_enemy, _bfs, _anim, _player, _ai)
    {
        name = STATE.COMBAT_START;
    }

    public override void Enter()
    {
        anim.Play("StartCombat");
        Debug.Log("CombatStart Entered");
        previousState = this;
        base.Enter();
    }

    public override void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= anim.GetCurrentAnimatorStateInfo(0).length - 0.01f)
        {

            //nextState = new EnemyIdle(enemy, bfs, anim, player, ai);
            Type stateType = Type.GetType("EnemyIdle");
            nextState = ai.customStates[stateType];
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        anim.SetTrigger("CombatStarted");
        base.Exit();
    }
}
