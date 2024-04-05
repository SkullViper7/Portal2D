using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _finish;
    [SerializeField] Animator _blackAnimator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "End")
        {
            Debug.Log("End");
            _audioSource.PlayOneShot(_finish);
            _blackAnimator.Play("BlackEndFadeIn");

            Invoke("Restart", _finish.length);
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

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ennemy")
        {
            SendMessage("Die");
        }
    }
}
