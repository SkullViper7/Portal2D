using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public void Init(PlayerMain _main)
    {
        _main.Aim = this;
    }

    void FixedUpdate()
    {
        transform.up = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;
    }
}
