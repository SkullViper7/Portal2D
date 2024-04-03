using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Portal : MonoBehaviour
{
    GameObject _otherPortal;
    Portal _otherPortalScript;

    void Start()
    {
        CheckWalls();

        Invoke("FindOtherPortal", 0.1f);
    }

    public void FindOtherPortal()
    {
        if (gameObject.tag == "CyanPortal")
        {
            _otherPortal = GameObject.FindGameObjectWithTag("PurpPortal");
            _otherPortalScript = _otherPortal.GetComponent<Portal>();
        }

        else if (gameObject.tag == "PurpPortal")
        {
            _otherPortal = GameObject.FindGameObjectWithTag("CyanPortal");
            _otherPortalScript = _otherPortal.GetComponent<Portal>();
        }
    }

    /// <summary>
    /// Checks if the portal is near a vertical wall
    /// </summary>
    void CheckWalls()
    {
        // Raycast to check if the portal is near a vertical wall
        var isLockedUp = Physics2D.Raycast(transform.position, Vector2.up, 1, 3);
        var isLockedDown = Physics2D.Raycast(transform.position, Vector2.down, 1, 3);

        if (gameObject.tag == "CyanPortal" && isLockedUp)
        {
            PortalManager.Instance.IsCyanUpLocked = true;
        }
        if (gameObject.tag == "PurpPortal" && isLockedUp)
        {
            PortalManager.Instance.IsPurpUpLocked = true;
        }
        if (gameObject.tag == "CyanPortal" && isLockedDown)
        {
            PortalManager.Instance.IsCyanUpLocked = false;
        }
        if (gameObject.tag == "PurpPortal" && isLockedDown)
        {
            PortalManager.Instance.IsPurpUpLocked = false;
        }
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();

        Vector2 oldVelocity = playerRb.velocity;

        playerRb.velocity = Vector2.zero;

        StartCoroutine(_otherPortalScript.DisableCollider());

        other.transform.position = _otherPortal.transform.position;

        if (PortalManager.Instance.IsCyanUpLocked && PortalManager.Instance.IsCyanVertical || 
            PortalManager.Instance.IsPurpUpLocked && PortalManager.Instance.IsPurpVertical)
        {
            playerRb.AddForce(Vector2.left * oldVelocity * 10, ForceMode2D.Impulse);
        }
        
        if (PortalManager.Instance.IsCyanUpLocked && !PortalManager.Instance.IsCyanVertical
            || PortalManager.Instance.IsPurpUpLocked && !PortalManager.Instance.IsPurpVertical)
        {
            playerRb.AddForce(Vector2.down * oldVelocity * 10, ForceMode2D.Impulse);
        }

        if (!PortalManager.Instance.IsCyanUpLocked && PortalManager.Instance.IsCyanVertical
            || !PortalManager.Instance.IsPurpUpLocked && PortalManager.Instance.IsPurpVertical)
        {
            playerRb.AddForce(oldVelocity.normalized * oldVelocity.magnitude * Vector2.right, ForceMode2D.Impulse);
        }

        if (!PortalManager.Instance.IsCyanUpLocked && !PortalManager.Instance.IsCyanVertical
            || !PortalManager.Instance.IsPurpUpLocked && !PortalManager.Instance.IsPurpVertical)
        {
            playerRb.AddForce(Vector2.up * oldVelocity * 10, ForceMode2D.Impulse);
        }
    }

    public IEnumerator DisableCollider()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;

        yield return new WaitForSeconds(1f);

        GetComponent<CapsuleCollider2D>().enabled = true;
    }
}
