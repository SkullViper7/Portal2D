using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "End")
        {
            Debug.Log("End");
        }
    }
}
