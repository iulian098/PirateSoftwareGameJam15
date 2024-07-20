using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_EnemyHealthBar : UI_HealthBar
{
    Transform targetPosition;

    public void SetTarget(Transform target) {
        targetPosition = target;
    }

    protected override void FixedUpdate() {
        base.FixedUpdate();

        if (targetPosition == null) return;

       // transform.position = Camera.main.WorldToScreenPoint(targetPosition.position);
    }
}
