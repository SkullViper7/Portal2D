using UnityEngine;
using UnityEngine.VFX;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private VisualEffect spawnVfx;
    // Start is called before the first frame update
    void Start()
    {
        spawnVfx = GetComponent<VisualEffect>();
        Invoke("Spawn", 2.1f);
    }

    // Update is called once per frame
    void Spawn()
    {
        player.SetActive(true);
        Destroy(gameObject);
    }
}
