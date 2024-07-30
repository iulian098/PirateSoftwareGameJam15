using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    Dictionary<string, object> customData = new Dictionary<string, object>();
    List<Timer> timers = new List<Timer>();
    bool activated;

    public bool Activated { get => activated; set => activated = value; }

    protected override void Start() {
        base.Start();
    }

    protected override void FixedUpdate() {
        if (IsDead || Target == null) return;

        base.FixedUpdate();
        foreach (var timer in timers)
            timer.Tick();
    }

    public object GetCustomData(string key) {
        if (customData.TryGetValue(key, out object result))
            return result;
        return null;
    }

    public void SetCustomData(string key, object val) {
        if(customData.ContainsKey(key))
            customData[key] = val;
        else
            customData.Add(key, val);
    }

    public void SetPlayerTarget(Player target) {
        this.target = target;
    }
}
