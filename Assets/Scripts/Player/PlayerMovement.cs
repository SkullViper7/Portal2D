using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float Direction;

    [SerializeField]
    private float maxVelocity;

    [SerializeField]
    private float acceleration;

    [SerializeField]
    private float decceleration;

    private float velocity;

    private Rigidbody2D rb;

    public void Init(PlayerMain _main)
    {
        _main.Movement = this;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        velocity += Direction * acceleration * Time.deltaTime;
        velocity = ChangeVelocity(velocity);
        rb.velocity = new Vector2(velocity, rb.velocity.y);
    }

    float ChangeVelocity(float _velocity)
    {
        if (Mathf.Abs(_velocity) > maxVelocity)
        {
            if (_velocity < 0)
            {
                return -maxVelocity;
            }
            else
            {
                return maxVelocity;
            }
        }
        else
        {
            return _velocity;
        }
    }
}
