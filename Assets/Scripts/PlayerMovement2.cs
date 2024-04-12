using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class PlayerMovement2 : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 8f;
    public float runSpeed = 16f;
    public float jumpPower = 8f;
    public float gravity = 16f;
    //public float lookXLimit = 45f;
    //public float defaultHeight = 2f;
    //public float crouchHeight = 1f;
    //public float crouchSpeed = 3f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController characterController;

    private InputController inputController;

    private bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        inputController = GetComponent<InputController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = inputController.Run;
        if(isRunning)
        {
            Debug.Log("Me corrooooooo");
        }
        float SpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * inputController.InputMove.y : 0;
        float SpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * inputController.InputMove.x : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * SpeedX) + (right * SpeedY);

        if (inputController.Jumped && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        //Para agacharse

        /*if (Input.GetKey(KeyCode.R) && canMove) 
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;

        }
        else
        {
            characterController.height = defaultHeight;
            walkSpeed = 6f;
            runSpeed = 12f;
        }*/

        characterController.Move(moveDirection * Time.deltaTime);
    }
}