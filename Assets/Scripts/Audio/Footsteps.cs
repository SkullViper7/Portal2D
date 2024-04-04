using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField] List<AudioClip> _steps;
    [SerializeField] List<AudioClip> _landings;
    [SerializeField] List<AudioClip> _jumps;

    AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayStep()
    {
        _audioSource.PlayOneShot(_steps[Random.Range(0, _steps.Count)]);
    }

    public void PlayLanding()
    {
        _audioSource.PlayOneShot(_landings[Random.Range(0, _landings.Count)]);
    }

    public void PlayJump()
    {
        _audioSource.PlayOneShot(_jumps[Random.Range(0, _jumps.Count)]);
    }
}
