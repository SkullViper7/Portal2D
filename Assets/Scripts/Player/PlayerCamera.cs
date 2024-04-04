using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    PlayerMain main;

    public void Init(PlayerMain _main)
    {
        _main.Camera = this;
        main = _main;
        main.Movement.OnLanding += GroundImpact;
        main.Shoot.OnShoot += ShootShake;
    }

    void GroundImpact()
    {
        ImpulseManager.Instance.Shake(0, 2, new Vector3(0, -0.25f, 0), 0.25f);
    }

    void ShootShake()
    {
        ImpulseManager.Instance.Shake(0, 1, new Vector3(0, -0.25f, 0), 0.25f);
    }
}
