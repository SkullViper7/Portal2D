using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public PlayerCollider Collider;
    public PlayerInputs Input;
    public PlayerMovement Movement;
    public PlayerAim Aim;
    public PlayerShoot Shoot;

    public void Die()
    {
        Debug.Log("Die");
    }

    private void Awake()
    {
        BroadcastMessage("Init", this);
    }
}
