using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    //public Vector2 Direction;

    public void Init(PlayerMain _main)
    {
        _main.Aim = this;
    }

    public Vector2 FindAimDirection()
    {
        return (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;
    }

    void FixedUpdate()
    {
        transform.up = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;
    }
}
