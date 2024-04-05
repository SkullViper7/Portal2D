using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.VFX;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField] CinemachineVirtualCamera _cam;

    private VisualEffect spawnVfx;
    // Start is called before the first frame update
    void Start()
    {
        spawnVfx = GetComponent<VisualEffect>();
        StartCoroutine(ZoomOut());
        Invoke("Spawn", 2.1f);
    }

    IEnumerator ZoomOut()
    {
        float startTime = Time.time;
        float endTime = startTime + 2.1f;
        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / (endTime - startTime);
            _cam.m_Lens.FieldOfView = Mathf.Lerp(20, 60, t);
            yield return null;
        }
    }

    // Update is called once per frame
    void Spawn()
    {
        player.SetActive(true);
        Destroy(gameObject);
    }
}
