using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//This state is for adding a standard delay between each action
public class ActionCooldown : State
{
    bool coolingDown = true;
    float tweenTime = 0;
    [SerializeField] private State idleState;
    /*public ActionCooldown(EnemyScript _enemy, BattlefieldScript _bfs, Animator _anim, PlayerScript _player, EnemyAI _ai) : base(_enemy, _bfs, _anim, _player, _ai)
    {
        name = STATE.ACTION_COOLDOWN;
    }*/

    public override void Enter()
    {
        base.Enter();

        tweenTime = 0;


        var tweener = DOTween.To(() => tweenTime, x => tweenTime = x, 1, enemy.ActionCooldown)
           .OnStart(() => coolingDown = true)
           .OnComplete(() => coolingDown = false);


    }

    public override void Tick()
    {
        if (coolingDown == false && ai.wait == false)
        {
            //nextState = idleState;
            //stage = EVENT.EXIT;
            ai.ChangeState(idleState);
        }
    }

    public override void Exit()
    {
        coolingDown = true;
        tweenTime = 0;
        base.Exit();
    }
}
