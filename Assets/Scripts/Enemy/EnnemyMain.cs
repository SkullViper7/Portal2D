using UnityEngine;

public class EnnemyMain : MonoBehaviour
{
    public EnnemyMovement Movement;
    public EnnemyPatrol Patrol;
    public EnnemyVFX VFX;

    public void Die()
    {
        Debug.Log("Ennemy Die");
    }
    private void Awake()
    {
        BroadcastMessage("Init", this);
    }
}
