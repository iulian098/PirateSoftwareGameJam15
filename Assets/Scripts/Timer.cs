using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    float time;

    Action OnEnded;

    public Timer(int time, Action onEnded = null) {
        this.time = time;
        OnEnded = onEnded;
    }

    public void Tick() {
        time -= Time.deltaTime;
        if (time <= 0)
            OnEnded?.Invoke();
    }
}
