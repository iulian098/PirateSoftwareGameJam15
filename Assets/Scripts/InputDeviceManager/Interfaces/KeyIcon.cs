using UnityEngine;

public abstract class KeyIcon : MonoBehaviour, IDeviceChanged
{
    [SerializeField] bool autoRegister = true;

    private void OnEnable()
    {
        if(autoRegister)
            UIKeyIconManager.Instance.RegisterObject(this);
    }

    private void OnDisable()
    {
        if(autoRegister)
            UIKeyIconManager.Instance.UnregisterObject(this);
    }

    public abstract void DeviceChanged(InputDeviceType deviceType);
}
