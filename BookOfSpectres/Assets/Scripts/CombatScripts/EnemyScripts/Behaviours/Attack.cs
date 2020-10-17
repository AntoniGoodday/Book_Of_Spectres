using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Attack : State
{
    float interruptedAnimationTime = 0;
    public Attack(EnemyScript _enemy, BattlefieldScript _bfs, Animator _anim, PlayerScript _player, EnemyAI _ai) : base(_enemy, _bfs, _anim, _player, _ai)
    {
        name = STATE.ATTACK;
    }

    public override void Enter()
    {

        anim.SetTrigger("Attack");

        interruptedAnimationTime = enemy.InterruptedAnimationTime;
        //anim.Play("Attack", 0, 0);
        //anim.speed = 1;
        if(ai.canBeCounteredAgain)
        {
            anim.Play("Attack",0,0);
        }
        else
        {

        }
        
        base.Enter();

    }

    public override void Update()
    {

        //interruptedAnimationTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;

        if (enemy.IsInterrupted == true && enemy.CanBeCountered)
        {
            ai.canBeCounteredAgain = false;
            nextState = new CounterState(enemy, bfs, anim, player, ai);
            previousState = this;
            stage = EVENT.EXIT;
        }

        if (enemy.AnimationEnd)
        {
            nextState = new ActionCooldown(enemy, bfs, anim, player, ai);
            stage = EVENT.EXIT;
        }

        //base.Update();


    }

    public override void Exit()
    {
        for (int i = 0; i < ai.aiMastermind.attackTokens.Count; i++)
        {
            if (ai.aiMastermind.attackTokens[i] == false)
            {
                ai.aiMastermind.attackTokens[i] = true;
                ai.aiMastermind.StartCoroutine("GiveToken", 1);
                break;
            }
        }
        ai.hasAttackToken = false;

        if (enemy.IsInterrupted)
        {
            enemy.InterruptedAnimationTime = interruptedAnimationTime;
        }
        else
        {
            enemy.InterruptedAnimationTime = 0;
            enemy.AnimationEnd = false;
            anim.ResetTrigger("Attack");
        }
        
        base.Exit();
    }
}
