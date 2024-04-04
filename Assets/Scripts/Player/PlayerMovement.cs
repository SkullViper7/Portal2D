using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Direction;

    public bool IsPortalInForce;

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
    private Collider2D playerCollider;
    private ContactFilter2D contactFilter;



    [Header("Jump")]

    private bool inJumpState;
    private float startTime;
    private bool isGrounded;
    private bool canJump;

    public event Action OnJump;
    public event Action OnLanding;
    public event Action OnFall;

    [SerializeField]
    private Vector2 JumpThrustPower;

    [SerializeField]
    private float jumpTime = 0.5f;

    public void Init(PlayerMain _main)
    {
        _main.Movement = this;
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        contactFilter.layerMask = LayerMask.GetMask("Wall");
        contactFilter.useLayerMask = true;
        canJump = true;
        OnJump += InitJump;
        OnLanding += PlayerIsLanding;
    }

    public void Jump()
    {
        if (canJump && !IsPortalInForce)
        {
            OnJump();
        }
    }

    private void FixedUpdate()
    {
        isGrounded = CheckForGround();

        if (!IsPortalInForce)
        {
            if (inJumpState)
            {
                if (Time.time > startTime + jumpTime)
                {
                    if (isGrounded)
                    {
                        OnLanding();
                    }
                }

                if (Direction != 0)
                {
                    velocity += Direction * (acceleration / 3) * Time.deltaTime;
                    velocity = ChangeVelocity(velocity);
                }
            }
            else if(isGrounded && !IsPortalInForce)
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
            }

            else if (!inJumpState)
            {
                OnFall();
            }

            rb.velocity = new Vector2(velocity, rb.velocity.y);
        }

        else
        {
            if (rb.velocity.magnitude <= maxVelocity)
            {
                IsPortalInForce = false;
            }
        }

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

    private bool CheckForGround()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, GetComponent<Collider2D>().bounds.size.y/2+0.1f, LayerMask.GetMask("Solid"));
        RaycastHit2D[] hits = new RaycastHit2D[1];
        int i = Physics2D.CircleCast(transform.position, playerCollider.bounds.size.x / 2, Vector2.down, contactFilter, hits, playerCollider.bounds.size.y / 2 + 0.1f);
        Debug.DrawRay(transform.position, Vector3.down * (GetComponent<Collider2D>().bounds.size.y / 2 + 0.1f), Color.red);
        //Debug.Log(hit.collider.name);
        //return  hit ;
        return i > 0;//rb.velocity.y == 0;

    }

    private void InitJump()
    {
        startTime = Time.time;
        rb.AddForce(JumpThrustPower);
        inJumpState = true;
        canJump = false;
    }

    private void PlayerIsLanding()
    {
        inJumpState = false;
        canJump = true;
    }
}
