using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private Vector2 _inputMovement;
    public Vector2 InputMove { get { return _inputMovement; } }

    private bool _jumped;
    public bool Jumped { get { return _jumped; } }
    
    private bool _run;
    public bool Run { get { return _run; } }

    private bool _shoot;
    public bool Shoot { get { return _shoot; } }

    private bool _zoom;
    public bool Zoom { get { return _zoom; } }
    
    private void LateUpdate()
    {
        _jumped = false;
        _zoom = false;
        _shoot = false;
    }

    private void OnMove(InputValue input)
    {
        _inputMovement = input.Get<Vector2>();
    }
    
    private void OnJump()
    {
        _jumped = true;
    }
    private void OnShoot()
    {
        Debug.Log("Dispara");
        _shoot = true;
    }
    private void OnRunStart()
    {
        _run = true;
    }
    private void OnRunEnd()
    {
        _run = false;
    }
    private void OnZoomStart()
    {
        _zoom = true;
    }
    private void OnZoomEnd()
    {
        _zoom = false;
    }
}
