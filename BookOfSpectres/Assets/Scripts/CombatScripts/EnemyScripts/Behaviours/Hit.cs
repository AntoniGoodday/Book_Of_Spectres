using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : State
{

    [SerializeField] private State cooldownState;
    /*public Hit(EnemyScript _enemy, BattlefieldScript _bfs, Animator _anim, PlayerScript _player, EnemyAI _ai) : base(_enemy, _bfs, _anim, _player, _ai)
    {

    }*/

    public override void Enter()
    {
        anim.SetTrigger("Hit");
        base.Enter();
    }

    public override void Exit()
    {
        anim.ResetTrigger("Hit");

        //nextState = new ActionCooldown(enemy, bfs, anim, player, ai);
        //stage = EVENT.EXIT;

        ai.ChangeState(cooldownState);
        base.Exit();
    }


}
