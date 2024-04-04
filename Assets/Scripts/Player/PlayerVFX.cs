using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.VFX;

public class PlayerVFX : MonoBehaviour
{
    [Header("Chromatic Aberration")]

    public Transform Portal;

    [SerializeField]
    private Volume volume;

    [SerializeField]
    private float minDistanceChromaticAberration;

    [SerializeField]
    private float maxDistanceChromaticAberration;

    private ChromaticAberration ca;

    [Header("VisualEffects")]

    [SerializeField]
    private VisualEffect JumpEffect;  

    [SerializeField]
    private VisualEffect LandingEffect;  

    [SerializeField]
    private VisualEffect WalkEffect;  

    private PlayerMain main;

    public void Init(PlayerMain _main)
    {
        _main.VFX = this;
        main = _main;
        main.Movement.OnJump += PlayJumpEffect;
        main.Movement.OnLanding += PlayLandingEffect;
        /*if (volume.profile.TryGet<ChromaticAberration>(out var type))
        {
            ca = type;
        }*/
    }

    private void PlayJumpEffect()
    {
        JumpEffect.Play();
    }

    private void PlayLandingEffect()
    {
        LandingEffect.Play();
    }

    public void UpdateWalkEffect(float _direction)
    {
        switch(_direction)
        {
            case 0:
                WalkEffect.Stop();
                break;
            case 1:
                WalkEffect.Play();
                WalkEffect.transform.eulerAngles = new Vector3(0, 90, 0);
                JumpEffect.transform.eulerAngles = new Vector3(0, 90, 0);
                break;
            case -1:
                WalkEffect.Play();
                WalkEffect.transform.eulerAngles = new Vector3(0, 0, 0);
                JumpEffect.transform.eulerAngles = new Vector3(0, 0, 0);
                break;
        }
    }

    private void FixedUpdate()
    {
        if (Portal != null)
        {
            Vector3 _direction = Portal.position - transform.position;
            Debug.Log(_direction.x + _direction.y);
            if (_direction.x + _direction.y < maxDistanceChromaticAberration)
            {
                ca.intensity.value =  (maxDistanceChromaticAberration - (_direction.x + _direction.y)) / maxDistanceChromaticAberration;

            }
        }
    }
}
