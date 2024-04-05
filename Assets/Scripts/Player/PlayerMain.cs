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

    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _deathSFX;

    public event Action OnDie;

    public void Die()
    {
        Debug.Log("Die");
        _audioSource.PlayOneShot(_deathSFX);
        OnDie();
    }

    private void Awake()
    {
        BroadcastMessage("Init", this);
    }
}
