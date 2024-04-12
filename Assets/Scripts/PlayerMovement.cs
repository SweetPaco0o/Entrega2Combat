using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    public float defaultSpeed = 10f;
    public float increasedSpeed = 20f;
    public float JumpSpeed = 4f;
    public float Jumps = 2f;
    public float JumpsLeft = 0f;
    public float SmoothRotation = 0.01f;
    

    public float gravity = -9.81f;
    public float gravityMultiplier = 2f;
    private Vector3 moveDirection = Vector3.zero;

    public Transform GroundChecker;
    public float groundSphereRadius= 0.1f;

    public LayerMask WhatIsGround;

    Vector3 _lastvelocity;

    CharacterController _characterController;
    InputController _inputController;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _inputController = GetComponent<InputController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 velocity = _lastvelocity;

        Vector3 localInput = transform.right * _inputController.InputMove.x
            + transform.forward * _inputController.InputMove.y;

        float smoothy = 0.2f;

        if (!IsGrounded())
        {
            smoothy = 0.01f;
            if (moveDirection.y > 0)
            {
                moveDirection.y -= gravityMultiplier * Time.deltaTime;
                _characterController.Move(moveDirection * Time.deltaTime);
            }
            
        }
        else
        {
            JumpsLeft = Jumps;
        }

        float WalkingSpeed = _inputController.Run ?  increasedSpeed : defaultSpeed;

        if(localInput.magnitude > 0)
        {
            velocity.x = Mathf.Lerp(velocity.x, localInput.x * WalkingSpeed, smoothy);
            velocity.z = Mathf.Lerp(velocity.z, localInput.z * WalkingSpeed, smoothy);
        }
        else
        {
            velocity.x = 0;
            velocity.y = 0;
        }

        velocity.y = GetGravity();

        if (ShouldJump())
        {
            velocity.y = JumpSpeed;
            --JumpsLeft;
        }

        _lastvelocity = velocity;

        _characterController.Move(velocity * Time.deltaTime);
    }

    private bool ShouldJump()
    {
        return _inputController.Jumped && (IsGrounded() || JumpsLeft>0);
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(
            GroundChecker.position, groundSphereRadius, WhatIsGround);
    }

    private float GetGravity()
    {
        float currentVelocity = _lastvelocity.y;
        currentVelocity += gravity * Time.deltaTime;
        return currentVelocity;
    }
}
