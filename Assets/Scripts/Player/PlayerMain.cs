using System;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public PlayerCollider Collider;
    public PlayerInputs Input;
    public PlayerMovement Movement;
    public PlayerAim Aim;
    public PlayerShoot Shoot;
    public PlayerVFX VFX;
    public PlayerCamera Camera;
    public PlayerAnim Anim;
    public PlayerRagdoll Ragdoll;

    public event Action OnDie;

    public void Die()
    {
        Debug.Log("Die");
        OnDie();
    }

    private void Awake()
    {
        BroadcastMessage("Init", this);
    }
}
