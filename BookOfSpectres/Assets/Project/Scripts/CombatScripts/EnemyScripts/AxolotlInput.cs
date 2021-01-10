using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxolotlInput : MonoBehaviour
{
    EntityScript enemy;
    EnemyAI ai;
    [SerializeField]
    State idleState;
    [SerializeField]
    State moveState;

    BattlefieldScript bfs;
    EntityInputManager entityInput;
    // Start is called before the first frame update
    void Start()
    {
        enemy = this.GetComponent<EntityScript>();
        ai = this.GetComponent<EnemyAI>();
        entityInput = this.GetComponent<EntityInputManager>();
        bfs = BattlefieldScript.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        VerticalMovement();
        Attack();
    }

    void Attack()
    {
        if (ai.currentState == idleState)
        {
            if (enemy.currentGridPosition.y == bfs.playerPosition.y)
            {
                entityInput.attack = true;
            }
        }
        else
        {
            entityInput.attack = false;
        }
    }

    void VerticalMovement()
    {
        if (enemy.currentGridPosition.y < bfs.playerPosition.y)
        {
            if (enemy.TileCheck(0, 1))
            {
                entityInput.moveUp = true;
                entityInput.moveDown = false;
                entityInput.movementVector = new Vector2(0, 1);
            return;
            }
        }
        else if (enemy.currentGridPosition.y > bfs.playerPosition.y)
        {
            if (enemy.TileCheck(0, -1))
            {
                entityInput.moveDown = true;
                entityInput.moveUp = false;
                entityInput.movementVector = new Vector2(0, -1);
            return;
            }
        }
        else
        {
            entityInput.movementVector = new Vector2(0, 0);
            entityInput.moveUp = false;
            entityInput.moveDown = false;
        }


    }

    
}
