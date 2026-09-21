using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class KeyActionIcon : KeyIcon
{
    [SerializeField] protected InputActionReference action;
    [SerializeField] protected Image image;

    public override void DeviceChanged(InputDeviceType deviceType)
    {
        Sprite icon = UIKeyIconManager.Instance.GetActionIcon(action);
        if (icon == null)
            return;

        image.sprite = icon;
    }
}
