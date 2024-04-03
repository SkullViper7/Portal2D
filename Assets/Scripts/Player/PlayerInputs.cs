using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    private PlayerMain main;
    public void Init(PlayerMain _main)
    {
        _main.Input = this;
        main = _main;
    }

    void OnMove(InputValue _move)
    {
        main.Movement.Direction = _move.Get<Vector2>().x;
        main.VFX.UpdateWalkEffect(_move.Get<Vector2>().x);
    }

    void OnJump()
    {
        main.Movement.Jump();
    }

    void OnPortalCyan()
    {
        main.Shoot.FireCyanProjectile();
    }

    void OnPortalPurple()
    {
        main.Shoot.FirePurpleProjectile();
    }
}

