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
        if (entity.IsMoving == true)
        {
            if (processing == false)
            {
                Debug.Log(_movementX + " and " + _movementY);
                if (entity.TileCheck(_movementX, _movementY))
                {
                    
                    //Debug.Log(_movementX + " and " + _movementY);
                    //Vector2 _moveVec = new Vector2(_movementX, _movementY);
                    processing = true;
                    entity.move.Move(_movementX, _movementY);

                }
                else
                {
                    Cooldown();
                }
            }

            /*if(ai.entityInput.moveUp)
            {
                if(entity.TileCheck(0, 1))
                {
                    processing = true;
                    //entity.SetTileInfo(0, 1);
                    Debug.Log("go up");
                    entity.GetComponent<ICombatMove>().Move();

                }
                else
                {
                    Cooldown();
                }
            }
            else if(ai.entityInput.moveDown)
            {
                if (entity.TileCheck(0, -1))
                {
                    processing = true;
                    //entity.SetTileInfo(0, -1);
                    Debug.Log("go down");
                    entity.GetComponent<ICombatMove>().Move();
                }
                else
                {
                    Cooldown();
                }
            }*/
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
