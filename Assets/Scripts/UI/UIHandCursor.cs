using UnityEngine;
using UnityEngine.UI;

public class UIHandCursor : MonoSingleton<UIHandCursor>
{
    public enum SpriteType
    {
        Normal,
        Drag
    }

    [SerializeField] Image handImage;
    [SerializeField] Sprite[] handSprites;

    public void SetHandSprite(SpriteType type)
    {
        handImage.sprite = handSprites[(int)type];
    }

    public void Show()
    {
        Cursor.visible = false;
        handImage.gameObject.SetActive(true);
    }

    public void Hide()
    {
        Cursor.visible = true;
        handImage.gameObject.SetActive(false);
    }
}
