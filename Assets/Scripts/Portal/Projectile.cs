using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class Projectile : MonoBehaviour
{

    public GameObject Player;

    [SerializeField] public GameObject _portalPrefab;

    [SerializeField]
    private float shakeDuration;

    [SerializeField]
    private float shakeStrength;

    [SerializeField]
    private int shakeVibrato;

    [SerializeField]
    private float shakeRandomness;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "VerticalWall" || other.gameObject.tag == "HorizontalWall")
        {
           /* if (other.gameObject.tag == "VerticalWall" )
            {
                GameObject newPortal = Instantiate(_portalPrefab, transform.position, Quaternion.Euler(0, 0, 90));

                PortalManager.Instance.Portals.Add(newPortal);

                newPortal.GetComponent<Portal>().IsVertical = true;
                newPortal.GetComponent<Portal>().CheckWalls();

                if (PortalManager.Instance.Portals.Count > 1)
                {
                    for (int i = 0; i < PortalManager.Instance.Portals.Count; i++)
                    {
                        PortalManager.Instance.Portals[i].GetComponent<Portal>().FindOtherPortal();
                    }
                }
            }

            if (other.gameObject.tag == "HorizontalWall")
            {
                GameObject newPortal = Instantiate(_portalPrefab, transform.position, Quaternion.identity);

                PortalManager.Instance.Portals.Add(newPortal);

                newPortal.GetComponent<Portal>().IsVertical = false;
                newPortal.GetComponent<Portal>().CheckWalls();

                if (PortalManager.Instance.Portals.Count > 1)
                {
                    for (int i = 0; i < PortalManager.Instance.Portals.Count; i++)
                    {
                        PortalManager.Instance.Portals[i].GetComponent<Portal>().FindOtherPortal();
                    }
                }
            }

            if (other.gameObject.tag == "VerticalWall" && gameObject.tag == "CyanProjectile")
            {
                GameObject newPortal = Instantiate(_portalPrefab, transform.position, Quaternion.Euler(0, 0, 90));

                PortalManager.Instance.Portals.Add(newPortal);

                newPortal.GetComponent<Portal>().IsVertical = true;
                newPortal.GetComponent<Portal>().CheckWalls();

                if (PortalManager.Instance.Portals.Count > 1)
                {
                    for (int i = 0; i < PortalManager.Instance.Portals.Count; i++)
                    {
                        PortalManager.Instance.Portals[i].GetComponent<Portal>().FindOtherPortal();
                    }
                }
            }

            if (other.gameObject.tag == "HorizontalWall" && gameObject.tag == "CyanProjectile")
            {
                GameObject newPortal = Instantiate(_portalPrefab, transform.position, Quaternion.identity);

                PortalManager.Instance.Portals.Add(newPortal);

                newPortal.GetComponent<Portal>().IsVertical = false;
                newPortal.GetComponent<Portal>().CheckWalls();

                if (PortalManager.Instance.Portals.Count > 1)
                {
                    for (int i = 0; i < PortalManager.Instance.Portals.Count; i++)
                    {
                        PortalManager.Instance.Portals[i].GetComponent<Portal>().FindOtherPortal();
                    }
                }
            }*/
            other.transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness);
            if (other.gameObject.tag == "VerticalWall" )
            {
                GameObject newPortal = Instantiate(_portalPrefab, transform.position, Quaternion.Euler(0, 0, 90));
                newPortal.GetComponent<Portal>().IsVertical = true;
                InitPortal(newPortal);
            }

            if (other.gameObject.tag == "HorizontalWall")
            {
                GameObject newPortal = Instantiate(_portalPrefab, transform.position, Quaternion.identity);
                newPortal.GetComponent<Portal>().IsVertical = false;
                InitPortal(newPortal);
            }

            Destroy(gameObject);
        }
    }

    private void InitPortal(GameObject _newPortal)
    {
        PortalManager.Instance.Portals.Add(_newPortal);
        Portal portal = _newPortal.GetComponent<Portal>();
        portal.Player = Player;
        portal.CheckWalls();

            if (PortalManager.Instance.Portals.Count > 1)
            {
                portal.FindOtherPortal();
            }
    }
}
