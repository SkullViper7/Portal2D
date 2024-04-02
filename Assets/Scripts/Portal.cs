using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Portal : MonoBehaviour
{
    Portal _otherPortal;

    void Start()
    {
        FindOtherPortal();
        CheckWalls();
    }

    public void FindOtherPortal()
    {
        _otherPortal = FindObjectOfType<Portal>();
    }

    void CheckWalls()
    {
        if (Physics2D.Raycast(transform.position, Vector2.up, 1f, 3))
        {
            PortalManager.Instance.IsUpLocked = true;
        }

        if (Physics2D.Raycast(transform.position, Vector2.down, 1f, 3))
        {
            PortalManager.Instance.IsUpLocked = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();

        Vector2 oldVelocity = playerRb.velocity;

        playerRb.velocity = Vector2.zero;

        other.transform.position = _otherPortal.transform.position;

        if (PortalManager.Instance.IsUpLocked && PortalManager.Instance.IsVertical)
        {
            playerRb.AddForce(Vector2.left * oldVelocity, ForceMode2D.Impulse);
        }
        
        if (PortalManager.Instance.IsUpLocked && !PortalManager.Instance.IsVertical)
        {
            playerRb.AddForce(Vector2.down * oldVelocity, ForceMode2D.Impulse);
        }

        if (!PortalManager.Instance.IsUpLocked && PortalManager.Instance.IsVertical)
        {
            playerRb.AddForce(Vector2.right * oldVelocity, ForceMode2D.Impulse);
        }

        if (!PortalManager.Instance.IsUpLocked && !PortalManager.Instance.IsVertical)
        {
            playerRb.AddForce(Vector2.up * oldVelocity, ForceMode2D.Impulse);
        }
    }
}
