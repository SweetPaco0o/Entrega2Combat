using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PatrolBehaviour : BaseBehaviour
{
    float _timer;
    private float Speed = 2;
    private float ChanceToWaypoint = 0.5f;
    public bool IsWaypointing = false;

    public Transform CheckPoint;
    public LayerMask WhatIsGround;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        _timer = 0f;        

        Vector3 rdmPointInPlane = new Vector3(Random.Range(-100, 100), animator.transform.position.y, Random.Range(-100, 100));
        animator.transform.LookAt(rdmPointInPlane);
        IsWaypointing = Random.value < ChanceToWaypoint;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Check triggers
        bool isTimeUp = CheckTime();
        bool isPlayerClose = CheckPlayer(animator.transform);

        animator.SetBool("IsPatrolling", !isTimeUp);
        animator.SetBool("IsChasing", isPlayerClose);

        if (IsWaypointing)
        {
            Move(animator.transform);
        }
        else if (!isPlayerClose && Random.value < ChanceToWaypoint)
        {
            IsWaypointing = true;
            animator.SetBool("IsWaypointing", IsWaypointing);
        }

        // Do stuff
        Move(animator.transform);
    }

    private void Move(Transform mytransform)
    {
        mytransform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }

    private bool CheckTime()
    {
        _timer += Time.deltaTime;
        return _timer > 4;
    }

    private bool CheckPlayer(Transform transform)
    {
        // Aquí debes implementar la lógica para verificar si el jugador está cerca
        // Puedes usar colliders o raycasts para esto
        return false;
    }
}

