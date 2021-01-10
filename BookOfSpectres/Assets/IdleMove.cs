using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IdleMove : StateMachineBehaviour
{
    PlayerScript playerScript;
    EntityScript enemyScript;
    BattlefieldScript bfs;
    Random rand;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bfs = BattlefieldScript.Instance;
        playerScript = PlayerScript.Instance;
        enemyScript = animator.GetComponent<EntityScript>();
        int i = Random.Range(0, 100);
        if (i < 50 && enemyScript.currentGridPosition.y + 1 < bfs.yMax && enemyScript.TileCheck(0,1))
        {
            
            enemyScript.SetTileInfo(0, 1);
            enemyScript.StartCoroutine("LerpMovement", enemyScript.movementSpeed);
        }
        else if (i < 50 && enemyScript.currentGridPosition.y + 1 >= bfs.yMax && enemyScript.TileCheck(0, -1))
        {
            enemyScript.SetTileInfo(0, -1);
            enemyScript.StartCoroutine("LerpMovement", enemyScript.movementSpeed);
        }
        else if (i >= 50 && enemyScript.currentGridPosition.y - 1 >= 0 && enemyScript.TileCheck(0, -1))
        {
            enemyScript.SetTileInfo(0, -1);
            enemyScript.StartCoroutine("LerpMovement", enemyScript.movementSpeed);
        }
        else if (i >= 50 && enemyScript.currentGridPosition.y - 1 < 0 && enemyScript.TileCheck(0, 1))
        {
            enemyScript.SetTileInfo(0, 1);
            enemyScript.StartCoroutine("LerpMovement", enemyScript.movementSpeed);
        }

        animator.SetBool("isMoving", false);
    }
}
