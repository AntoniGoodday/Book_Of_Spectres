using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : State
{
    bool processing = false;
    public Move(EnemyScript _enemy, BattlefieldScript _bfs, Animator _anim, PlayerScript _player, EnemyAI _ai) : base(_enemy, _bfs, _anim, _player, _ai)
    {
        name = STATE.MOVE;
    }


    public override void Enter()
    {
        base.Enter();
        processing = false;
        enemy.IsMoving = true;
    }

    public override void Update()
    {
        Debug.Log("Base Move");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public void Cooldown()
    {
        nextState = new ActionCooldown(enemy, bfs, anim, player, ai);
        stage = EVENT.EXIT;
    }
}
