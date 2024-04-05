using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.VFX;

public class PlayerVFX : MonoBehaviour
{
    [Header("Chromatic Aberration")]

    public List<Transform> portals = new List<Transform>();

    [SerializeField]
    private Volume volume;

    [SerializeField]
    private float minDistanceChromaticAberration;

    [SerializeField]
    private float maxDistanceChromaticAberration;

    private ChromaticAberration ca;
    private Vignette vignette;

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
        if (volume.profile.TryGet<ChromaticAberration>(out var caType))
        {
            ca = caType;
        }

        if (volume.profile.TryGet<Vignette>(out var vignetteType))
        {
            vignette = vignetteType;
        }

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
        if (portals.Count != 0)
        {
            Vector3 closestDirection = portals[0].position - transform.position;
            foreach(var portal in portals)
            {
                Vector3 _direction = portal.position - transform.position; 
                if (closestDirection.magnitude > _direction.magnitude)
                {
                    closestDirection = _direction;
                }
            }
            
            if (closestDirection.magnitude < maxDistanceChromaticAberration)
            {
                ca.intensity.value =  (maxDistanceChromaticAberration - closestDirection.magnitude) / maxDistanceChromaticAberration;
                vignette.intensity.value = (maxDistanceChromaticAberration - closestDirection.magnitude) / maxDistanceChromaticAberration;
            }
        }
    }

    
}
