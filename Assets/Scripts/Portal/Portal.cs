using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    GameObject _otherPortal;
    Portal _otherPortalScript;

    public bool IsUpLocked;
    public bool IsVertical;

    // void Start()
    // {
    //     CheckWalls();

    //     Invoke("FindOtherPortal", 0.1f);
    // }

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
    public void CheckWalls()
    {
        // Raycast to check if the portal is near a vertical wall

        LayerMask mask = LayerMask.GetMask("Wall");
        Vector2 raycastDirection = Vector2.up;
        if (IsVertical)
        {
            raycastDirection = Vector2.right;
        }
        IsUpLocked = Physics2D.Raycast(transform.position, raycastDirection, 1, mask);
        //Destroy(hit.transform.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();

        StartCoroutine(_otherPortalScript.DisableCollider());

        other.transform.position = _otherPortal.transform.position;

        Vector3 otherNormal = _otherPortal.transform.InverseTransformDirection(_otherPortal.transform.up - _otherPortalScript.transform.position);
        playerRb.velocity = Vector2.Reflect(playerRb.velocity, otherNormal);
    }


    public IEnumerator DisableCollider()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;

        yield return new WaitForSeconds(0.25f);

        GetComponent<CapsuleCollider2D>().enabled = true;
    }
}
