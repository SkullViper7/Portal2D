using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Direction;

    [SerializeField]
    private float maxVelocity;

    [SerializeField]
    private float acceleration;

    [SerializeField]
    private float decceleration;

    [SerializeField]
    private float minVelocity;

    private float velocity;

    private Rigidbody2D rb;

    

    [Header("Jump")]

    private bool inJumpState;
    private float startTime;

    [SerializeField]
    private Vector2 JumpThrustPower;

    [SerializeField]
    private float jumpTime = 0.5f;

    public void Init(PlayerMain _main)
    {
        _main.Movement = this;
        rb = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        startTime = Time.time;
        rb.AddForce(JumpThrustPower);
    }

    private void FixedUpdate()
    {
        if (Direction != 0)
        {
            velocity += Direction * acceleration * Time.deltaTime;
            velocity = ChangeVelocity(velocity);
        }
        else
        {
            if (velocity < 0)
            {
                velocity += decceleration * Time.deltaTime;
            }
            else
            {
                velocity -= decceleration * Time.deltaTime;
            }

            if (Mathf.Abs(velocity) < minVelocity)
            {
                velocity = 0;
            }
        }
        
        rb.velocity = new Vector2(velocity, rb.velocity.y);

       /* if (inJumpState)
        {
            if (Time.time > startTime + jumpTime || !sm.pc.isHoldingJumpKey)
            {
                sm.Transition(sm.fallState);
            }
        }*/
    }

        private float ChangeVelocity(float _velocity)
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
