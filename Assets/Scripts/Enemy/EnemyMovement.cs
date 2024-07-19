using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform targetDestination;
    void FixedUpdate()
    {
        agent.SetDestination(targetDestination.position);
    }
}
