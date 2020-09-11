using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVertical : Move
{
    bool processing = false;
    public MoveVertical(EnemyScript _enemy, BattlefieldScript _bfs, Animator _anim, PlayerScript _player, EnemyAI _ai) : base(_enemy, _bfs, _anim, _player, _ai)
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
        if(enemy.IsMoving == true && processing == false)
        {
            if (enemy.currentGridPosition.y < bfs.playerPosition.y)
            {
                if (enemy.TileCheck(0, 1))
                {
                    processing = true;
                    enemy.SetTileInfo(0, 1);
                    enemy.GetComponent<IEnemyCombatMove>().Move();

                }
                else
                {
                    Cooldown();
                }
            }
            else if(enemy.currentGridPosition.y > bfs.playerPosition.y)
            {
                if (enemy.TileCheck(0, -1))
                {
                    processing = true;
                    enemy.SetTileInfo(0, -1);
                    enemy.GetComponent<IEnemyCombatMove>().Move();
                }
                else
                {
                    Cooldown();
                }
            }
            else
            {
                Cooldown();
            }
        }
        else
        {
            Cooldown();
        }
        //base.Update();
    }
}
