using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumScript;
using System;
[System.Serializable]
public class State : MonoBehaviour, IState
{
    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    public EVENT stage;
    protected EntityScript entity;
    protected PlayerScript player;
    protected BattlefieldScript bfs;
    protected Animator anim;
    public EnemyAI ai;
    protected State nextState;
    protected State previousState;

    /*public  State(EnemyScript _enemy, BattlefieldScript _bfs, Animator _anim, PlayerScript _player, EnemyAI _ai)
    {
        enemy = _enemy;
        bfs = _bfs;
        stage = EVENT.ENTER;
        player = _player;
        anim = _anim;
        ai = _ai;
    }*/


    private void Start()
    {
        if (ai != null)
        {
            entity = ai.enemy;
            bfs = ai.bfs;
            stage = EVENT.ENTER;
            player = ai.player;
            anim = ai.anim;
        }
    }


    public virtual void Enter()
    {
        stage = EVENT.UPDATE;
    }

    public virtual void Tick()
    {
        stage = EVENT.EXIT;
    }

    public virtual void Exit()
    {
        stage = EVENT.ENTER;
    }



    public State Process()
    {
        if(stage == EVENT.ENTER)
        {
            Enter();
        }
        if(stage == EVENT.UPDATE)
        {
            Tick();
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
        if(bfs.playerPosition.y == entity.currentGridPosition.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



}
