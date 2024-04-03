using UnityEngine;
using UnityEngine.VFX;

public class PlayerVFX : MonoBehaviour
{
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
                JumpEffect.transform.eulerAngles = new Vector3(0, 90, 0);
                break;
        }
    }
}
