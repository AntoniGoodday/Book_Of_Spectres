using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : StateMachineBehaviour
{
    PlayerScript playerScript;
    EntityScript enemyScript;
    BattlefieldScript bfs;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bfs = BattlefieldScript.Instance;
        playerScript = PlayerScript.Instance;
        enemyScript = animator.GetComponent<EntityScript>();

        animator.SetBool("isMoving", true);

    }
}
