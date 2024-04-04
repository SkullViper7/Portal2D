using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RumbleManager : MonoBehaviour
{
    private static RumbleManager _instance;

    public static RumbleManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<RumbleManager>();

                if (_instance == null)
                {
                    GameObject go = new GameObject("RumbleManager");
                    _instance = go.AddComponent<RumbleManager>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
    }


    public IEnumerator Rumble(float intensity, float duration)
    {
        Gamepad.current.SetMotorSpeeds(intensity, intensity);

        yield return new WaitForSeconds(duration);

        Gamepad.current.SetMotorSpeeds(0, 0);
    }
}
