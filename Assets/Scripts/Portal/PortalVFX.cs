using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class PortalVFX : MonoBehaviour
{
    [SerializeField]
    private float delayEffectPortal;

    [SerializeField]
    private float delayAnimationSpawnPortal;

    private Volume volume;

    private LensDistortion lensDistortion;
    private FilmGrain filmGrain;
    private Bloom bloom;

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

        
        if (volume.profile.TryGet<Bloom>(out var bloomType))
        {
            bloom = bloomType;
        }

        SendMessage("GiveDelay", delayEffectPortal);
    }

    public void PlayVFXPortal()
    {
        DOTween.To(() => lensDistortion.intensity.value, x => lensDistortion.intensity.value = x, 1f, delayEffectPortal)
        .OnComplete( () => DOTween.To(() => lensDistortion.intensity.value, x => lensDistortion.intensity.value = x, 0f, delayEffectPortal));
        DOTween.To(() => filmGrain.intensity.value, x => filmGrain.intensity.value = x, 1f, delayEffectPortal)
        .OnComplete( () => DOTween.To(() => filmGrain.intensity.value, x => filmGrain.intensity.value = x, 0f, delayEffectPortal));

    }

    public void SpawnPortalVFX()
    {
        DOTween.To(() => bloom.intensity.value, x => bloom.intensity.value = x, 50f, delayAnimationSpawnPortal)
        .OnComplete(() => bloom.intensity.value = 0f);
    }
}
