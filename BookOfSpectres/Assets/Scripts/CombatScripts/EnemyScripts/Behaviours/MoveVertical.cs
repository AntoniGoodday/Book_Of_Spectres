using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVertical : Move
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void Tick()
    {
        if (enemy.IsMoving == true && processing == false)
        {
            if(ai.entityInput.moveUp)
            {
                if(enemy.TileCheck(0, 1))
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
            else if(ai.entityInput.moveDown)
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
        }
        else
        {
            Cooldown();
        }
        /*if(enemy.IsMoving == true && processing == false)
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
        }*/
    }

    public override void Exit()
    {
        base.Exit();

    }
}
