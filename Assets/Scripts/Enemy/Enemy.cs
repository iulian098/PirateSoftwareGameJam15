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
    [SerializeField] Vector3 healthBarOffset;
    [SerializeField] LayerMask playerLayerMask;

    Collider2D[] detectedColliders = new Collider2D[1];
    UI_EnemyHealthBar healthBar;
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
        healthComponent.OnDamageReceived += OnDamageReceived;
        if (healthBar == null) {
            healthBar = UIManager.Instance.EnemyHealthBarManager.GetHealthBar();
            healthBar.SetTarget(transform);
            healthBar.Init(healthComponent.MaxHealth);
            HealthComponent.OnHealthChanged += healthBar.SetValue;
        }
    }

    private void OnDamageReceived(int dmg) {
        UIManager.Instance.DamageNumberManager.ShowText($"-{dmg}",
            false,
            Camera.main.WorldToScreenPoint(transform.position + healthBarOffset)
            );
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

        if(healthBar != null) {
            healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + healthBarOffset);
        }
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
