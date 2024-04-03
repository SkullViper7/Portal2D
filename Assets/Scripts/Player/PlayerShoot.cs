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

    public void Init(PlayerMain _main)
    {
        _main.Shoot = this;
        main = _main;
    }

    public void FireCyanProjectile()
    {
        GameObject newGameObject = Instantiate(purpleProjectile);
        newGameObject.GetComponent<Rigidbody2D>().velocity = (main.Aim.FindAimDirection() * speedProjectile);
    }

    public void FirePurpleProjectile()
    {

    }
}

