using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionSpell : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<PlayerScript>().emotionAnim.Play("EmotionSpell", 0, 0f);
    }
}
