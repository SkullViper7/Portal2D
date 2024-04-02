using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    GameObject _portalPrefab;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "VerticalWall")
        {
            PortalManager.Instance.IsVertical = true;

            GameObject newPortal = Instantiate(_portalPrefab, transform.position, Quaternion.Euler(0, 0, 90));
        }

        else if (other.gameObject.tag == "HorizontalWall")
        {
            PortalManager.Instance.IsVertical = false;

            GameObject newPortal = Instantiate(_portalPrefab, transform.position, Quaternion.identity);
        }
    }
}
