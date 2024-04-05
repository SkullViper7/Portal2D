using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Portal : MonoBehaviour
{
    public GameObject _otherPortal;

    public GameObject Player;
    Portal _otherPortalScript;

    public bool IsUpLocked;
    public bool IsVertical;

    public Vector2 ForceDirection;

    private float delayEffectPortal;

    [SerializeField]
    private float delayDisablePortal;

    private Vector3 originalScale;
    private Vector3 scaleTo;

    // void Start()
    // {
    //     CheckWalls();

    //     Invoke("FindOtherPortal", 0.1f);
    // }

    private void Start()
    {
        Player.GetComponent<PlayerVFX>().portals.Add(transform);
        originalScale = transform.localScale;
        SendMessage("SpawnPortalVFX");
    }

    public void FindOtherPortal()
    {
        bool canFindPortal = true;
        if (PortalManager.Instance.Portals.Count == 2)
        {
            for (int i = 0; i < PortalManager.Instance.Portals.Count; i++)
            {
                GameObject portalToDestroy = PortalManager.Instance.Portals[i];
                if (gameObject != portalToDestroy && gameObject.tag == portalToDestroy.tag)
                {
                    portalToDestroy.GetComponent<Portal>().GetDisable();
                    canFindPortal = false;
                }
            }
            
        }

        if (canFindPortal)
        {
            if (gameObject.tag == "CyanPortal")
            {
                FindPortal("PurpPortal");
            }
            else if (gameObject.tag == "PurpPortal")
            {
                FindPortal("CyanPortal");
            }
        }
        
    }

    private void FindPortal(string _tag)
    {
        _otherPortal = GameObject.FindGameObjectWithTag(_tag);
        _otherPortalScript = _otherPortal.GetComponent<Portal>();

        if (_otherPortalScript._otherPortal != null)
        {
            _otherPortalScript.NewPortal(this);
        }

        StartCoroutine(_otherPortalScript.GetNewPortal(this));
    }

    public void NewPortal(Portal _newPortal)
    {
        _otherPortalScript.GetDisable();
    }

    /// <summary>
    /// Checks if the portal is near a vertical wall
    /// </summary>
    public void CheckWalls()
    {
        // Raycast to check if the portal is near a vertical wall

        LayerMask mask = LayerMask.GetMask("Wall");
        Vector2 raycastDirection = Vector2.up;
        int forcePoint = 0;
        if (IsVertical)
        {
            forcePoint = 1;
            raycastDirection = Vector2.right;
        }

        IsUpLocked = Physics2D.Raycast(transform.position, raycastDirection, 1, mask);
        if (IsUpLocked)
        {
            forcePoint += 2;
        }

        switch (forcePoint)
        {
            case 0:
                ForceDirection = Vector2.up;
                break;
            case 1:
                ForceDirection = Vector2.right;
                break;
            case 2:
                ForceDirection = Vector2.down;
                break;
            case 3:
                ForceDirection = Vector2.left;
                break;
        }
        //Destroy(hit.transform.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (PortalManager.Instance.Portals.Count == 2)
            {
                if (other.gameObject.tag == "Ennemy" || other.gameObject.tag == "Player")
                {
                    Rigidbody2D entityRb = other.GetComponent<Rigidbody2D>();

                    if (other.tag == "Ennemy")
                    {
                        other.transform.position = _otherPortal.transform.position;
                        float velocityTotal = entityRb.velocity.magnitude;
                        other.GetComponent<EnnemyMovement>().IsPortalInForce = true;
                        entityRb.velocity = _otherPortalScript.ForceDirection * Math.Abs(velocityTotal);
                        StartCoroutine(_otherPortalScript.DisableCollider());
                        scaleTo = originalScale * 1.2f;
                        transform.DOScale(scaleTo, 0.5f)
                            .SetEase(Ease.InBounce)
                            .SetDelay(0.05f)
                            .OnComplete(() =>
                                transform.DOScale(originalScale, 1));
                    }

                    else
                    {
                        StartCoroutine(_otherPortalScript.DisableCollider(other.transform, entityRb.velocity.magnitude));
                    }
                        
                }
            

            /*
            if (other.tag == "Ennemy")
            {
                other.GetComponent<EnnemyMovement>().IsPortalInForce = true;
                other.GetComponent<Rigidbody2D>().velocity = _otherPortalScript.ForceDirection * Math.Abs(velocityTotal);
                scaleTo = originalScale * 1.2f;
                transform.DOScale(scaleTo, 0.5f)
                    .SetEase(Ease.InBounce)
                    .SetDelay(0.05f)
                    .OnComplete(() =>
                    transform.DOScale(originalScale, 1));
            }

            if (other.tag == "Player")
            {
                playerRb.GetComponent<PlayerMovement>().IsPortalInForce = true;
                playerRb.velocity = _otherPortalScript.ForceDirection * Math.Abs(velocityTotal);
                scaleTo = originalScale * 1.2f;
                transform.DOScale(scaleTo, 0.5f)
                    .SetEase(Ease.InBounce)
                    .SetDelay(0.05f)
                    .OnComplete(() =>
                    transform.DOScale(originalScale, 1));
            }*/
        }
        
    }


    public IEnumerator DisableCollider()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;

        yield return new WaitForSeconds(delayDisablePortal * 2);

        GetComponent<CapsuleCollider2D>().enabled = true;
    }

    public IEnumerator DisableCollider(Transform _player, float _velocity)
    {
        float velocityTotal = _velocity;
        
        GetComponent<CapsuleCollider2D>().enabled = false;
        PlayerInputs inputs = _player.GetComponent<PlayerInputs>();
        Rigidbody2D playerRb = _player.GetComponent<Rigidbody2D>();

        playerRb.gravityScale = 0;  
        playerRb.velocity = Vector2.zero;
        playerRb.gameObject.SendMessage("ScalePlayerEnterPortal", delayEffectPortal);

        inputs.OnPortal = true;

        float oldDirection = inputs.main.Movement.Direction;
        inputs.main.Movement.Direction = 0;

        SendMessage("PlayVFXPortal");

        yield return new WaitForSeconds(delayEffectPortal);
        
        //tp le joueur
         _player.position = transform.position;

         PlayerMovement playerMovement = _player.GetComponent<PlayerMovement>();
         playerMovement.IsPortalInForce = true;
         playerMovement.canJump = false;
         inputs.OnPortal = false;

         

         playerRb.velocity = ForceDirection * Math.Abs(velocityTotal);
         playerRb.gravityScale = 2;  

         inputs.main.Movement.Direction = oldDirection;

        yield return new WaitForSeconds(delayDisablePortal);

        GetComponent<CapsuleCollider2D>().enabled = true;
    }


    public void GetDisable()
    {
        PortalManager.Instance.Portals.Remove(gameObject);
        Player.GetComponent<PlayerVFX>().portals.Remove(transform);
        Destroy(gameObject);
    }

    public IEnumerator GetNewPortal(Portal _theNewPortal)
    {
        yield return new WaitForSeconds(0.1f);
        _otherPortal = _theNewPortal.gameObject;
        _otherPortalScript = _theNewPortal;
    }

    public void GiveDelay(float delay)
    {
        delayEffectPortal = delay;
    }
}
