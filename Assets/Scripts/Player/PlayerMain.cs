using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public PlayerCollider Collider;
    public PlayerInputs Input;
    public PlayerMovement Movement;
    public PlayerAim Aim;

    private void Awake()
    {
        BroadcastMessage("Init", this);
    }
}
