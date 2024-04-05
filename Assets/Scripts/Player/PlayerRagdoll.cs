using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerRagdoll : MonoBehaviour
{
    List<Rigidbody> _rigidbodies;
    Rigidbody2D _rb;
    Collider2D _collider;
    Animator _animator;
    [SerializeField] CinemachineVirtualCamera _cam;
    PlayerMain main;

    public void Init(PlayerMain _main)
    {
        _main.Ragdoll = this;
        main = _main;
        _main.OnDie += EnableRagdoll;
    }

    void Awake()
    {
        _rigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _collider = GetComponent<Collider2D>();
    }

    void EnableRagdoll()
    {
        _cam.m_Follow = null;
        _animator.enabled = false;
        _collider.enabled = false;
        _rb.velocity = Vector2.zero;
        _rb.AddForce(new Vector2(Random.Range(-1000, 1000), Random.Range(500, 1000)));
        _rb.AddTorque(Random.Range(-1000, 1000));

        for (int i = 0; i < _rigidbodies.Count; i++)
        {
            _rigidbodies[i].isKinematic = false;
        }
    }
}
