using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private PlayerMain main;

    Animator _animator;

    [SerializeField] Transform _mesh;

    public void Init(PlayerMain _main)
    {
        _main.Anim = this;
        main = _main;
        _main.Input.OnIdle += Idle;
        _main.Input.OnRun += Run;
        _main.Input.OnJumpEvent += Jump;
        _main.Movement.OnFall += Fall;
        _main.Movement.OnLanding += Land;
        _main.Input.OnFlipRight += FlipRight;
        _main.Input.OnFlipLeft += FlipLeft;
    }

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    void Run()
    {
        _animator.SetInteger("State", 1);
    }

    void Jump()
    {
        _animator.SetInteger("State", 2);
    }

    void Idle()
    {
        _animator.SetInteger("State", 0);
    }

    void Fall()
    {
        _animator.SetInteger("State", 3);
    }

    void Land()
    {
        _animator.SetInteger("State", 0);
    }

    void FlipRight()
    {
        _mesh.rotation = Quaternion.Euler(0, 90, 0);
    }

    void FlipLeft()
    {
        _mesh.rotation = Quaternion.Euler(0, -90, 0);
    }
}
