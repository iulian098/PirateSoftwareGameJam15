using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{
    [SerializeField] string vfxName;
    [SerializeField] float duration;

    Vector3 defaultScale;

    public Vector3 DefaultScale => defaultScale;
    public string VFXName => vfxName;

    Action<VFX> EndCallback;

    private void Awake() {
        defaultScale = transform.localScale;
    }

    public void Show(Action<VFX> endCallback) {
        EndCallback = endCallback;
        if (duration > 0) {
            Invoke(nameof(Hide), duration);
        }
    }

    public void Hide() {
        gameObject.SetActive(false);
        EndCallback?.Invoke(this);
    }
}
