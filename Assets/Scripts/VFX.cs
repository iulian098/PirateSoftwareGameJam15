using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{
    [SerializeField] string vfxName;
    [SerializeField] float duration;

    public string VFXName => vfxName;

    Action<VFX> EndCallback;

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
