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
        // Directions of the raycasts
        var up = Vector2.up;
        var down = Vector2.down;

        // Distance of the raycasts
        var distance = 1f;

        // LayerMask of the raycasts
        var layerMask = 3;

        // Raycast to check if the portal is near a vertical wall
        var isLocked = Physics2D.Raycast(transform.position, up, distance, layerMask);

        // If the portal is blue, set the corresponding instance variable
        if (gameObject.tag == "CyanPortal")
            PortalManager.Instance.IsCyanVertical = isLocked;
        // If the portal is orange, set the corresponding instance variable
        else
            PortalManager.Instance.IsPurpVertical = isLocked;

        // Raycast to check if the portal is near the top of the screen
        isLocked = Physics2D.Raycast(transform.position, down, distance, layerMask);

        // If the portal is blue, set the corresponding instance variable
        if (gameObject.tag == "CyanPortal")
            PortalManager.Instance.IsCyanUpLocked = !isLocked;
        // If the portal is orange, set the corresponding instance variable
        else
            PortalManager.Instance.IsPurpUpLocked = !isLocked;
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
        GetComponent<BoxCollider2D>().enabled = false;

        yield return new WaitForSeconds(1f);

        GetComponent<BoxCollider2D>().enabled = true;
    }
}
