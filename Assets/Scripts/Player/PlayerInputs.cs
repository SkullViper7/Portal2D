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
        Debug.Log("Move");
        main.Movement.Direction = _move.Get<Vector2>().x;
    }

    void OnJump()
    {
        Debug.Log("jump");
    }

    void OnPortalBlue()
    {
        Debug.Log("blue");
    }

    public void OnPortalOrange()
    {
        Debug.Log("orange");
    }

}
