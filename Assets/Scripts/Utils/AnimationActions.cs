using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationActions : MonoBehaviour
{
    [SerializeField] UnityEvent[] actions;

    public void TriggerAction(int index) {
        actions[index].Invoke();
    }
}
