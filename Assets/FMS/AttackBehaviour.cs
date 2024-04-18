using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : BaseBehaviour
{
    private float attackRange = 1.0f;
    private float attackCooldown = 1.0f;
    private float Damage = 10f;

    private float lastAttackTime = 0.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool isPlayerClose = CheckPlayer2(animator.transform);
        animator.SetBool("IsPlayerClose", isPlayerClose);
        bool isReachable = CheckPlayer3(animator.transform);
        animator.SetBool("IsAttacking", isReachable);

        if (isPlayerClose & isReachable)
        {
            if(Time.time - lastAttackTime >= attackCooldown)
            {
                AttackPlayer();
                lastAttackTime = Time.time;
            }
            

        }
    }
    private void AttackPlayer()
    {
        Debug.Log("Attack!");
    }
}
