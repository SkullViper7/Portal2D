using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject purpleProjectile;

    [SerializeField]
    private GameObject cyanProjectile;

    [SerializeField]
    private float speedProjectile;

    private PlayerMain main;

    public event Action OnShoot;

    public void Init(PlayerMain _main)
    {
        _main.Shoot = this;
        main = _main;
    }

    public void FireCyanProjectile()
    {
        GameObject newGameObject = Instantiate(cyanProjectile, transform);
        PortalProjectile(newGameObject);
    }

    public void FirePurpleProjectile()
    {
        GameObject newGameObject = Instantiate(purpleProjectile, transform);
        PortalProjectile(newGameObject);
    }

    private void PortalProjectile(GameObject _newGameObject)
    {   
        _newGameObject.GetComponent<Rigidbody2D>().velocity = main.Aim.FindAimDirection() * speedProjectile;
        _newGameObject.GetComponent<Projectile>().Player = gameObject;
        //Call an event when portal is created, our playerVFX will use this to change chromatic aberration;
        //OnShoot();
    }
}

