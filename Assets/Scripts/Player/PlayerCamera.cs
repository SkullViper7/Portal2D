using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    PlayerMain main;

    public void Init(PlayerMain _main)
    {
        _main.Camera = this;
        main = _main;
        _main.Movement.OnLanding += GroundImpact;
        _main.Shoot.OnShoot += ShootShake;
        _main.OnDie += Die;
    }

    void GroundImpact()
    {
        ImpulseManager.Instance.Shake(0, 2, new Vector3(0, -0.25f, 0), 0.25f);
        StartCoroutine(RumbleManager.Instance.Rumble(0.5f, 0.25f));
    }

    void ShootShake()
    {
        ImpulseManager.Instance.Shake(0, 1, new Vector3(0, -0.25f, 0), 0.25f);
        StartCoroutine(RumbleManager.Instance.Rumble(0.5f, 0.25f));
    }

    void Die()
    {
        ImpulseManager.Instance.Shake(1, 3, new Vector3(0, -0.5f, 0), 0.5f);
        StartCoroutine(RumbleManager.Instance.Rumble(1, 0.5f));
    }
}
