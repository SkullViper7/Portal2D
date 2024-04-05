using UnityEngine;
using UnityEngine.VFX;

public class EnnemyVFX : MonoBehaviour
{
    [SerializeField]
    private VisualEffect WalkEffect;  

    [SerializeField]
    private Sprite dustSprite;
    // Start is called before the first frame update
    public void Init(EnnemyMain _main)
    {
        _main.VFX = this;
        WalkEffect.SetTexture("TextureDust", TextureSplit());
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
                break;
            case -1:
                WalkEffect.Play();
                WalkEffect.transform.eulerAngles = new Vector3(0, 0, 0);
                break;
        }
    }

        private Texture2D TextureSplit()
    {
        var croppedTexture = new Texture2D( (int)dustSprite.rect.width, (int)dustSprite.rect.height );

        var pixels = dustSprite.texture.GetPixels(  (int)dustSprite.textureRect.x, 
                                                    (int)dustSprite.textureRect.y, 
                                                    (int)dustSprite.textureRect.width, 
                                                    (int)dustSprite.textureRect.height );

        croppedTexture.SetPixels( pixels );
        croppedTexture.Apply();
        return croppedTexture;
    }

}
