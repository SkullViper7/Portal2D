using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    private static PortalManager _instance;

    public static PortalManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PortalManager>();

                if (_instance == null)
                {
                    GameObject obj = new GameObject("PortalManager");
                    _instance = obj.AddComponent<PortalManager>();
                }
            }

            return _instance;
        }
    }

    public bool IsPurpVertical;
    public bool IsCyanVertical;
    public bool IsPurpUpLocked;
    public bool IsCyanUpLocked;
}
