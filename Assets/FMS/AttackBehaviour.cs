using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackBehaviour : BaseBehaviour
{
    public float attackRange = 1.0f;
    public float attackCooldown = 1.0f;
    public int Damage = 10;

    public float lastAttackTime = 0.0f;

    public string playerTag = "Player";
    private GameObject playerObject;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        playerObject = GameObject.FindGameObjectWithTag(playerTag);
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
            playerObject = GameObject.FindGameObjectWithTag(playerTag);
            AttackPlayer();
            
            if(Time.time - lastAttackTime >= attackCooldown)
            {
                AttackPlayer();
                lastAttackTime = Time.time;
            }        
        }
    }
    private void AttackPlayer()
    {
        if(playerObject != null) 
        {
            Debug.Log("Attacks");
            playerObject.GetComponent<PlayerHealthSystem>().PlayerTakesDamage(Damage);
        }
        else
        {
            Debug.Log("Player doesn't exist");
        }
    }
}
