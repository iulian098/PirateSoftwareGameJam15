using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character {
    [SerializeField] EnemyData enemyData;
    [SerializeField] EnemyState enemyState;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float aggroRange;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask playerLayerMask;

    Collider2D[] detectedColliders = new Collider2D[1];
    Player target;
    float attackTime;
    bool isDead;

    public EnemyData EnemyData => enemyData;
    public EnemyState EnemyState => enemyState;
    public Player Target => target;
    public float AttackTime { get => attackTime; set => attackTime = value; }
    public float AttackRate => enemyData.AttackRate;
    public int Damage => enemyData.Damage;
    public bool IsInAttackRange => Vector2.Distance(target.transform.position, transform.position) < attackRange;
    public float TargetDistance => Vector2.Distance(target.transform.position, transform.position);

    private void Start() {
        healthComponent.OnDied += OnDied;
    }

    private void OnDied() {
        isDead = true;
    }

    void FixedUpdate() {
        if (isDead) return;

        Physics2D.OverlapCircle(transform.position, aggroRange, new ContactFilter2D() { layerMask = playerLayerMask, useLayerMask = true }, detectedColliders);
        if (target == null && detectedColliders != null && detectedColliders.Length > 0)
            if (detectedColliders[0].TryGetComponent<Player>(out var player))
                target = player;

        if(attackTime > 0)
            attackTime -= Time.deltaTime;
    }

    public bool PlayerDetected() {
        if (target != null && detectedColliders != null && detectedColliders.Length > 0)
            return true;

        return false;
    }

    public void SetTargetLocation(bool chase = true) {
        if(chase)
            agent.SetDestination(target.transform.position);
        else 
            agent.SetDestination(transform.position);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
