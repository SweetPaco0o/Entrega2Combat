using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreattackBehaviour : BaseBehaviour
{
    private float CloseSpeed = 6;

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



        Move(animator.transform);
    }
    private void Move(Transform mySelf)
    {
        Vector3 PlayerPos = new Vector3(_player.transform.position.x, _player.transform.position.y, _player.transform.position.z);

        mySelf.transform.LookAt(PlayerPos);

        mySelf.Translate(Vector3.forward * CloseSpeed * Time.deltaTime, Space.Self);
    }
}
