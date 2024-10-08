using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;

    protected virtual void Awake() {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this as T;
    }
}
