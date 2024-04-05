using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public Vector2 AimDirection;

    public void Init(PlayerMain _main)
    {
        _main.Aim = this;
    }

    public Vector2 FindAimDirection()
    {
        if (AimDirection == Vector2.zero)
        {
            return (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;
        }
        else
        {
            return AimDirection;
        }
    }

    void FixedUpdate()
    {
        if (AimDirection == Vector2.zero)
        {
            transform.up = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;
        }
        else
        {
            transform.up = AimDirection;
        }
        
    }
}
