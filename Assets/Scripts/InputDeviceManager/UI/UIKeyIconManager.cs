using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-9)]
public class UIKeyIconManager : MonoSingleton<UIKeyIconManager>
{
    [SerializeField] GamepadKeyIcons gamepadKeyIcons;

    InputDeviceManager inputDeviceManager;
    Dictionary<InputDeviceType, HashSet<IKeyIcon>> registeredObjects = new Dictionary<InputDeviceType, HashSet<IKeyIcon>>();
    HashSet<IKeyIcon> registeredObjectGlobal = new HashSet<IKeyIcon>();

    private void Start()
    {
        inputDeviceManager = InputDeviceManager.Instance;
        inputDeviceManager.OnDeviceChanged += OnDeviceChanged;
    }

    private void OnDestroy()
    {
        inputDeviceManager.OnDeviceChanged -= OnDeviceChanged;
    }

    private void OnDeviceChanged(InputDeviceType type)
    {
        Invoke(nameof(UpdateObjects), 0.1f);
    }
    public void UpdateObjects()
    {
        InputDeviceType type = inputDeviceManager.CurrentDeviceType;
        if (registeredObjects.TryGetValue(type, out var registeredItems))
        {
            foreach (var obj in registeredItems.ToArray())
                if (obj != null)
                    obj.DeviceChanged(type);
        }

        foreach (var item in registeredObjectGlobal.ToArray())
        {
            if (item != null)
                item.DeviceChanged(type);
        }
    }

    public void RegisterObject(IKeyIcon obj)
    {
        registeredObjectGlobal.Add(obj);
    }

    public void RegisterObject(IKeyIcon obj, InputDeviceType device)
    {
        if(registeredObjects.ContainsKey(device))
            registeredObjects[device].Add(obj);
        else
            registeredObjects.Add(device, new HashSet<IKeyIcon>() { obj });
    }

    public void UnregisterObject(IKeyIcon obj)
    {
        foreach (var item in registeredObjects)
        {
            if (item.Value.Contains(obj))
                item.Value.Remove(obj);
        }
    }

    public void UnregisterObject(IKeyIcon obj, InputDeviceType device)
    {
        if (registeredObjects.ContainsKey(device))
            registeredObjects[device].Remove(obj);
    }

    public Sprite GetActionIcon(InputAction action)
    {
        var icons = gamepadKeyIcons.Icons.Find(x => x.deviceType == inputDeviceManager.CurrentDeviceType);
        string startString = GetDeviceTypeString(inputDeviceManager.CurrentDeviceType);
        var deviceBinding = action.bindings.FirstOrDefault(x => x.effectivePath.StartsWith(startString));
        if (deviceBinding != null && deviceBinding.effectivePath != null)
        {
            int slashIndex = deviceBinding.effectivePath.IndexOf('/');
            string keyString = deviceBinding.effectivePath.Substring(slashIndex + 1);

            foreach (var item in icons.icons)
            {
                if (deviceBinding != null)
                {
                    if (item.inputPath == keyString)
                        return item.icon;
                }
            }
            Debug.LogError("[UIKeyIconManager] No icon found for " + keyString);
        }
        return null;
    }

    public string GetActionIconText(InputAction action)
    {
        var icons = gamepadKeyIcons.Icons.Find(x => x.deviceType == inputDeviceManager.CurrentDeviceType);
        string startString = GetDeviceTypeString(inputDeviceManager.CurrentDeviceType);
        var deviceBinding = action.bindings.FirstOrDefault(x => x.effectivePath.StartsWith(startString));
        string keyString = string.Empty;
        if (deviceBinding != null && deviceBinding.effectivePath != null)
        {
            int slashIndex = deviceBinding.effectivePath.IndexOf('/');
            keyString = deviceBinding.effectivePath.Substring(slashIndex + 1);

            foreach (var item in icons.icons)
            {
                if (deviceBinding != null)
                {
                    if (item.inputPath == keyString)
                        return $"<sprite name=\"{item.spriteName}\">";
                }
            }
            Debug.LogError("[UIKeyIconManager] No icon found for " + keyString);
        }
        return keyString;
    }

    public string GetDeviceTypeString(InputDeviceType type)
    {
        switch (type)
        {
            case InputDeviceType.KeyboardAndMouse:
                return "<Keyboard>";
            case InputDeviceType.Gamepad:
                return "<Gamepad>";
            default:
                return "<Keyboard>";
        }
    }

    public string GetControlSchemeName(InputDeviceType type)
    {
        switch (type)
        {
            case InputDeviceType.KeyboardAndMouse:
                return "KeyboardAndMouse";
            case InputDeviceType.Gamepad:
                return "Gamepad";
            default:
                return string.Empty;
        }
    }
}
