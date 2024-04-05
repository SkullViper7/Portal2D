using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class PortalVFX : MonoBehaviour
{
    [SerializeField]
    private float delayEffectPortal;

    private Volume volume;

    private LensDistortion lensDistortion;
    private FilmGrain filmGrain;

    // Start is called before the first frame update
    void Start()
    {
        volume = FindAnyObjectByType<Volume>();
        if (volume.profile.TryGet<LensDistortion>(out var ldType))
        {
            lensDistortion = ldType;
        }

        if (volume.profile.TryGet<FilmGrain>(out var fgType))
        {
            filmGrain = fgType;
        }

        SendMessage("GiveDelay", delayEffectPortal);
    }

    public void PlayVFXPortal()
    {
        DOTween.To(() => lensDistortion.intensity.value, x => lensDistortion.intensity.value = x, 1f, delayEffectPortal)
        .OnComplete(() => lensDistortion.intensity.value = 0f);
        DOTween.To(() => filmGrain.intensity.value, x => filmGrain.intensity.value = x, 1f, delayEffectPortal)
        .OnComplete(() => filmGrain.intensity.value = 0f);

    }
}
