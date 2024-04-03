using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] public GameObject _portalPrefab;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "VerticalWall" || other.gameObject.tag == "HorizontalWall")
        {
            if (other.gameObject.tag == "VerticalWall" && gameObject.tag == "PurpProjectile")
            {
                PortalManager.Instance.IsPurpVertical = true;

                GameObject newPortal = Instantiate(_portalPrefab, transform.position, Quaternion.Euler(0, 0, 90));
            }

            if (other.gameObject.tag == "HorizontalWall" && gameObject.tag == "PurpProjectile")
            {
                PortalManager.Instance.IsPurpVertical = false;

                GameObject newPortal = Instantiate(_portalPrefab, transform.position, Quaternion.identity);
            }

            if (other.gameObject.tag == "VerticalWall" && gameObject.tag == "CyanProjectile")
            {
                PortalManager.Instance.IsCyanVertical = true;

                GameObject newPortal = Instantiate(_portalPrefab, transform.position, Quaternion.Euler(0, 0, 90));
            }

            if (other.gameObject.tag == "HorizontalWall" && gameObject.tag == "CyanProjectile")
            {
                PortalManager.Instance.IsCyanVertical = false;

                GameObject newPortal = Instantiate(_portalPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
