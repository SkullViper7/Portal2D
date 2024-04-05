using UnityEngine;

public class EnnemyMain : MonoBehaviour
{
    public EnnemyMovement Movement;
    public EnnemyPatrol Patrol;
    public EnnemyVFX VFX;

    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _death;

    public void Die()
    {
        Debug.Log("Ennemy Die");
        _audioSource.PlayOneShot(_death);
    }
    private void Awake()
    {
        BroadcastMessage("Init", this);
    }
}
