using UnityEngine;

public class EnnemyMain : MonoBehaviour
{
    public EnnemyMovement Movement;
    public EnnemyPatrol Patrol;

    private void Awake()
    {
        BroadcastMessage("Init", this);
    }
}
