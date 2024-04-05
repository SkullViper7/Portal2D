using System;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "End")
        {
            Debug.Log("End");
        }

        if (collision.tag == "Spike")
        {
            SendMessage("Die");
        }

        if (collision.tag == "DeadZone")
        {
            SendMessage("Die");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ennemy")
        {
            SendMessage("Die");
        }
    }
}
