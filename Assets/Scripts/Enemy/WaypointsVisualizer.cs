using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsVisualizer : MonoBehaviour
{
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        for (int i = 0; i < transform.childCount; i++) {
            Gizmos.DrawWireSphere(transform.GetChild(i).position, 0.2f);
        }
    }
}
