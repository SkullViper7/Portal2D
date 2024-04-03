using UnityEngine;

public class EnnemyCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Spike")
        {
            SendMessage("Die");
        }

        if (collision.tag == "DeadZone")
        {
            SendMessage("Die");
        }
    }
}
