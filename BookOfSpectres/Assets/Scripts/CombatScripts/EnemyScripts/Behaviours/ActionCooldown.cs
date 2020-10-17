using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//This state is for adding a standard delay between each action
public class ActionCooldown : State
{
    bool coolingDown = true;
    float tweenTime = 0;
    public ActionCooldown(EnemyScript _enemy, BattlefieldScript _bfs, Animator _anim, PlayerScript _player, EnemyAI _ai) : base(_enemy, _bfs, _anim, _player, _ai)
    {
        name = STATE.ACTION_COOLDOWN;
    }

    public override void Enter()
    {
        
        var tweener = DOTween.To(() => tweenTime, x => tweenTime = x, 1, enemy.ActionCooldown)
           .OnStart(() => coolingDown = true)
           .OnComplete(() => coolingDown = false);

        base.Enter();
    }

    public override void Update()
    {
        if (coolingDown == false && ai.wait == false)
        {
            nextState = new EnemyIdle(enemy, bfs, anim, player, ai);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();

    }
}
