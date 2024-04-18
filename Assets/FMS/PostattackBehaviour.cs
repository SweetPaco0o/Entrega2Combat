using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostattackBehaviour : BaseBehaviour
{
    float cooldownTime = 1.0f;
    float startTime = Time.time;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool isPlayerClose = CheckPlayer2(animator.transform);
        animator.SetBool("IsPlayerClose", isPlayerClose);
        bool isReachable = CheckPlayer3(animator.transform);
        animator.SetBool("IsAttacking", isReachable);



        Hold(animator.transform);
    }

    private void Hold(Transform transform)
    {
        while(Time.time - startTime < cooldownTime)
        {
            transform.position = transform.position;
        }
    }
}
