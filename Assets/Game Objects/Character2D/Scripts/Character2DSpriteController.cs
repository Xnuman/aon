using UnityEngine;

public class Character2DSpriteController : MonoBehaviour
{

    [SerializeField] private SpriteRenderer sr;

    [SerializeField] private Sprite standStillSprite;
    [SerializeField] private Sprite moveRightSprite;
    [SerializeField] private Sprite moveLeftSprite;

    public void SetSpriteOrientationForAllSprites(HorizontalOrientation orientation)
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        // Loop through each renderer and set the color property
        foreach (SpriteRenderer renderer in spriteRenderers)
        {
            int coeff =  orientation == HorizontalOrientation.Left ? 1 : -1;
            renderer.transform.localScale *= coeff;
        }
    }

    public void SetSpriteOrientation(HorizontalOrientation orientation)
    {
        switch (orientation)
        {
            case HorizontalOrientation.Left:
                if(moveLeftSprite == null)
                {
                    sr.sprite = moveRightSprite;
                    sr.flipX = true;
                }
                else
                {
                    sr.sprite = moveLeftSprite;
                    sr.flipX = false;
                }
                break;
            case HorizontalOrientation.Right:
                sr.sprite = moveRightSprite;
                sr.flipX = false;
                break;
            case HorizontalOrientation.None:
                sr.sprite = standStillSprite;
                sr.flipX = false;
                break;
            default:
                return;
        }
    }

}
