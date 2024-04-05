using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInputs : MonoBehaviour
{
    public PlayerMain main;

    public event Action OnIdle;
    public event Action OnRun;
    public event Action OnJumpEvent;   
    public event Action OnFlipRight;
    public event Action OnFlipLeft; 

    public bool OnPortal;

    public void Init(PlayerMain _main)
    {
        _main.Input = this;
        main = _main;
    }

    void OnMove(InputValue _move)
    {
        if (!OnPortal)
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
        
    }

    void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnJump()
    {
        if (!OnPortal)
        {
            main.Movement.Jump();
            OnJumpEvent();
        }
    }

    void OnPortalCyan()
    {
        if (!OnPortal)
        {
            main.Shoot.FireCyanProjectile();
        }
    }

    void OnPortalPurple()
    {
        if (!OnPortal)
        {
            main.Shoot.FirePurpleProjectile();
        }
    }

    void OnAim(InputValue value)
    {
        if (!OnPortal)
        {
            main.Aim.AimDirection = value.Get<Vector2>();
        }
    }
}

