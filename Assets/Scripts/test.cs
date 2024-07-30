using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    [SerializeField] float dist;
    Vector3 targetPos;
    // Start is
    // called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 10, layer);
        targetPos = hit.point;
        dist = hit.distance;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, targetPos);
    }
}
