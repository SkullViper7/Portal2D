using System.Collections.Generic;
using UnityEngine;

public class EnnemyPatrol : MonoBehaviour
{
    [SerializeField]
    private List<Transform> PatrolPoints = new List<Transform>();

    private Transform nextPoint;

    private int actualPoint;

    private EnnemyMain main;

    public void Init(EnnemyMain _main)
    {
        main = _main;
        _main.Patrol = this;
        actualPoint = 0;
        ChangePoint();
    }

    private void FixedUpdate()
    {
        //if(transform.position)
        Vector3 _direction = nextPoint.position - transform.position;

        if (Mathf.Abs(_direction.x) < 1)
        {
            if (actualPoint < PatrolPoints.Count - 1)
            {
                actualPoint++;
            }
            else
            {
                actualPoint = 0;
            }

            ChangePoint();
        }
    }

    private void ChangePoint()
    {
        nextPoint = PatrolPoints[actualPoint];
        float newDirection = 1;

        if (nextPoint.position.x < transform.position.x)
        {
            newDirection = -1;
        }

        main.Movement.Direction = new Vector3(newDirection, 0, 0);
    }
}
