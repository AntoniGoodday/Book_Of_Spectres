using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToRow : StateMachineBehaviour
{
    PlayerScript playerScript;
    EnemyScript enemyScript;
    BattlefieldScript bfs;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bfs = BattlefieldScript.Instance;
        playerScript = PlayerScript.Instance;
        enemyScript = animator.GetComponent<EnemyScript>();

        if (enemyScript.currentGridPosition.y < bfs.playerPosition.y)
        {
            enemyScript.SetTileInfo(0, 1);
            enemyScript.StartCoroutine("LerpMovement", enemyScript.movmentSpeed);
        }
        else if (enemyScript.currentGridPosition.y > bfs.playerPosition.y)
        {
            enemyScript.SetTileInfo(0, -1);
            enemyScript.StartCoroutine("LerpMovement", enemyScript.movmentSpeed);
        }
        else if(enemyScript.currentGridPosition.y == bfs.playerPosition.y)
        {
            animator.SetBool("isMoving", false);
        }
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
