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

    private bool _aim;
    public bool Aim { get { return _aim; } }

    private bool _zoom;
    public bool Zoom { get { return _zoom; } }

    private bool _arma1;
    public bool Arma1 { get { return _arma1; } }

    private bool _arma2;
    public bool Arma2 { get { return _arma2; } }

    private bool _arma3;
    public bool Arma3 { get { return _arma3; } }

    private void LateUpdate()
    {
        _jumped = false;
        _zoom = false;
        _shoot = false;
        _aim = false;
        _arma1 = false;
        _arma2 = false;
        _arma3 = false;
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
        _shoot = true;
    }
    private void OnAim()
    {
        _aim = true;
    }
    private void OnArma1()
    {
        _arma1 = true;
    }
    private void OnArma2()
    {
        _arma2 = true;
    }
    private void OnArma3()
    {
        _arma3 = true;
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
