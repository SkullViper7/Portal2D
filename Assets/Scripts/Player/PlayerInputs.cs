using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    private PlayerMain main;

    public event Action OnIdle;
    public event Action OnRun;
    public event Action OnJumpEvent;   
    public event Action OnFlipRight;
    public event Action OnFlipLeft; 

    public void Init(PlayerMain _main)
    {
        _main.Input = this;
        main = _main;
    }

    void OnMove(InputValue _move)
    {
        main.Movement.Direction = _move.Get<Vector2>().x;
        main.VFX.UpdateWalkEffect(_move.Get<Vector2>().x);
        OnRun();

        if (_move.Get<Vector2>().x > 0)
        {
            OnFlipRight();
        }
        if (_move.Get<Vector2>().x < 0)
        {
            OnFlipLeft();
        }

        if (_move.Get<Vector2>().x == 0)
        {
            OnIdle();
        }
    }

    void OnJump()
    {
        main.Movement.Jump();
        OnJumpEvent();
    }

    void OnPortalCyan()
    {
        main.Shoot.FireCyanProjectile();
    }

    void OnPortalPurple()
    {
        main.Shoot.FirePurpleProjectile();
    }

    void OnAim(InputValue value)
    {
        main.Aim.AimDirection = value.Get<Vector2>();
    }
}

