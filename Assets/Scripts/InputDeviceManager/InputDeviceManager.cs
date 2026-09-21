using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;

public enum InputDeviceType
{
    KeyboardAndMouse,
    Gamepad
}

[DefaultExecutionOrder(-10)]
public class InputDeviceManager : MonoSingleton<InputDeviceManager>
{
    [SerializeField] InputDeviceType currentDeviceType;

    public event Action<InputDeviceType> OnDeviceChanged;

    public InputDeviceType CurrentDeviceType => currentDeviceType;

    private void OnEnable()
    {
        InputSystem.onEvent += OnInputEvent;
    }

    private void OnDisable()
    {
        InputSystem.onEvent -= OnInputEvent;
    }

    private void OnInputEvent(InputEventPtr eventPtr, InputDevice device)
    {
        if (!eventPtr.IsA<StateEvent>() &&
            !eventPtr.IsA<DeltaStateEvent>())
            return;

        if(device is Gamepad gamepad)
        {
            if (HasGamepadAxisInput(eventPtr, gamepad)){
                SetDevice(InputDeviceType.Gamepad);
                return;
            }
        }else if(device is Mouse mouse)
        {
            if(HasMouseAxisInput(eventPtr, mouse))
            {
                SetDevice(InputDeviceType.KeyboardAndMouse);
                return;
            }
        }

        if (!eventPtr.HasButtonPress())
            return;

        if (device is Gamepad)
            SetDevice(InputDeviceType.Gamepad);
        else if (device is Keyboard || device is Mouse)
            SetDevice(InputDeviceType.KeyboardAndMouse);
    }

    private void SetDevice(InputDeviceType deviceType)
    {
        if (currentDeviceType == deviceType)
            return;

        currentDeviceType = deviceType;
        OnDeviceChanged?.Invoke(deviceType);

        Debug.Log("[InputDeviceManager] OnDeviceChanged " + deviceType);
    }

    private bool HasGamepadAxisInput(InputEventPtr eventPtr, Gamepad gamepad)
    {
        return IsAxisActive(eventPtr, gamepad.leftStick) ||
               IsAxisActive(eventPtr, gamepad.rightStick) ||
               IsAxisActive(eventPtr, gamepad.leftTrigger) ||
               IsAxisActive(eventPtr, gamepad.rightTrigger) ||
               IsAxisActive(eventPtr, gamepad.dpad);
    }

    private bool HasMouseAxisInput(InputEventPtr eventPtr, Mouse mouse)
    {
        return mouse.delta.HasValueChangeInEvent(eventPtr);
    }

    private bool IsAxisActive(InputEventPtr eventPtr, InputControl control)
    {
        if (!control.HasValueChangeInEvent(eventPtr))
            return false;

        if (control is StickControl stick)
        {
            Vector2 value = stick.ReadValueFromEvent(eventPtr);

            return value.sqrMagnitude > 0.1f * 0.1f;
        }

        if (control is AxisControl axis)
        {
            float value = axis.ReadValueFromEvent(eventPtr);

            return Mathf.Abs(value) > 0.1f;
        }

        return false;
    }
}
