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

        Invoke("FindOtherPortal", 1f);
    }

    public void FindOtherPortal()
    {
        if (gameObject.tag == "BluePortal")
        {
            _otherPortal = GameObject.FindGameObjectWithTag("OrangePortal");
            _otherPortalScript = _otherPortal.GetComponent<Portal>();
        }

        else if (gameObject.tag == "OrangePortal")
        {
            _otherPortal = GameObject.FindGameObjectWithTag("BluePortal");
            _otherPortalScript = _otherPortal.GetComponent<Portal>();
        }
    }

    void CheckWalls()
    {
        if (Physics2D.Raycast(transform.position, Vector2.up, 1f, 3) && gameObject.tag == "BluePortal")
        {
            PortalManager.Instance.IsBlueUpLocked = true;
        }

        if (Physics2D.Raycast(transform.position, Vector2.down, 1f, 3) && gameObject.tag == "BluePortal")
        {
            PortalManager.Instance.IsBlueUpLocked = false;
        }

        if (Physics2D.Raycast(transform.position, Vector2.up, 1f, 3) && gameObject.tag == "OrangePortal")
        {
            PortalManager.Instance.IsOrangeUpLocked = true;
        }

        if (Physics2D.Raycast(transform.position, Vector2.down, 1f, 3) && gameObject.tag == "OrangePortal")
        {
            PortalManager.Instance.IsOrangeUpLocked = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();

        Vector2 oldVelocity = playerRb.velocity;

        playerRb.velocity = Vector2.zero;

        StartCoroutine(_otherPortalScript.DisableCollider());

        other.transform.position = _otherPortal.transform.position;

        if (PortalManager.Instance.IsBlueUpLocked && PortalManager.Instance.IsBlueVertical || 
            PortalManager.Instance.IsOrangeUpLocked && PortalManager.Instance.IsOrangeVertical)
        {
            playerRb.AddForce(Vector2.left * oldVelocity, ForceMode2D.Impulse);
        }
        
        if (PortalManager.Instance.IsBlueUpLocked && !PortalManager.Instance.IsBlueVertical
            || PortalManager.Instance.IsOrangeUpLocked && !PortalManager.Instance.IsOrangeVertical)
        {
            playerRb.AddForce(Vector2.down * oldVelocity, ForceMode2D.Impulse);
        }

        if (!PortalManager.Instance.IsBlueUpLocked && PortalManager.Instance.IsBlueVertical
            || !PortalManager.Instance.IsOrangeUpLocked && PortalManager.Instance.IsOrangeVertical)
        {
            playerRb.AddForce(Vector2.right * oldVelocity, ForceMode2D.Impulse);
        }

        if (!PortalManager.Instance.IsBlueUpLocked && !PortalManager.Instance.IsBlueVertical
            || !PortalManager.Instance.IsOrangeUpLocked && !PortalManager.Instance.IsOrangeVertical)
        {
            playerRb.AddForce(Vector2.up * oldVelocity, ForceMode2D.Impulse);
        }
    }

    public IEnumerator DisableCollider()
    {
        GetComponent<BoxCollider2D>().enabled = false;

        yield return new WaitForSeconds(1f);

        GetComponent<BoxCollider2D>().enabled = true;
    }
}
