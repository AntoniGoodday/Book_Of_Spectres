using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumScript;
using System;
[System.Serializable]
public class State
{
    public enum STATE
    {
        COMBAT_START, IDLE, MOVE, ATTACK, HIT, INTERRUPT, ACTION_COOLDOWN, COUNTER
    };

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    public STATE name;
    public EVENT stage;
    protected EnemyScript enemy;
    protected PlayerScript player;
    protected BattlefieldScript bfs;
    protected Animator anim;
    protected EnemyAI ai;
    protected State nextState;
    protected State previousState;

    public  State(EnemyScript _enemy, BattlefieldScript _bfs, Animator _anim, PlayerScript _player, EnemyAI _ai)
    {
        enemy = _enemy;
        bfs = _bfs;
        stage = EVENT.ENTER;
        player = _player;
        anim = _anim;
        ai = _ai;
    }

    public virtual void Enter()
    {
        ai.InitializeStateMachine();
        stage = EVENT.UPDATE;
    }

    public virtual void Update()
    {
        stage = EVENT.UPDATE;
    }

    public virtual void Exit()
    {
        stage = EVENT.EXIT;
    }

    public State Process()
    {
        if(stage == EVENT.ENTER)
        {
            Enter();
        }
        if(stage == EVENT.UPDATE)
        {
            Update();
        }
        if(stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }

    public bool IsOnSameRow()
    {
        if(bfs.playerPosition.y == enemy.currentGridPosition.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



}
