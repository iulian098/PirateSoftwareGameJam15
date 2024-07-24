using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class MouseHelper : MonoSingleton<MouseHelper>
{

    [SerializeField] InputSystemUIInputModule uiInputModule;

    Vector2 mouseStartPos;
    Vector2 mousePos;

    bool dragging;
    bool mouseDown;
    public Action OnDrag;
    public Action OnDrop;

    public bool Dragging { get => dragging;
        set {
            dragging = value;
            if (value)
                OnDrag?.Invoke();
            else
                OnDrop?.Invoke();
        }
    }

    public Vector2 MousePos => mousePos;

    void Update()
    {
        if (uiInputModule.leftClick.action.WasReleasedThisFrame()) {
            if (Dragging)
                Dragging = false;
            mouseDown = false;
        }

        if (uiInputModule.leftClick.action.WasPressedThisFrame()) {
            mouseStartPos = uiInputModule.point.action.ReadValue<Vector2>();
            mouseDown = true;
        }

        if(mouseDown)
            mousePos = uiInputModule.point.action.ReadValue<Vector2>();

        if (mouseDown && !Dragging && Vector2.Distance(mousePos, mouseStartPos) > 0.02f)
            Dragging = true;
    }
}
