using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Android;

public class Enemy : Character {
    [SerializeField] EnemyData enemyData;
    [SerializeField] EnemyState enemyState;
    [SerializeField] Animator anim;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] SpriteRenderer characterSprite;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D collider;
    [SerializeField] float aggroRange;
    [SerializeField] float attackRange;
    [SerializeField] Vector3 healthBarOffset;
    [SerializeField] LayerMask playerLayerMask;

    Collider2D[] detectedColliders = new Collider2D[1];
    UI_EnemyHealthBar healthBar;
    Player target;
    float attackTime;
    float lastX;
    float spriteXScale;
    bool isDead;

    public EnemyData EnemyData => enemyData;
    public EnemyState EnemyState => enemyState;
    public Player Target => target;
    public float AttackTime { get => attackTime; set => attackTime = value; }
    public float AttackRate => enemyData.AttackRate;
    public int Damage => enemyData.Damage;
    public bool IsDead => isDead;
    public bool IsInAttackRange => Vector2.Distance(target.transform.position, transform.position) < attackRange;
    public float TargetDistance => Vector2.Distance(target.transform.position, transform.position);

    private void Start() {
        healthComponent.OnDied += OnDied;
        healthComponent.OnDamageReceived += OnDamageReceived;
        lastX = transform.position.x;
        spriteXScale = characterSprite.transform.localScale.x;
    }

    private void OnDamageReceived(int dmg) {
        if (healthBar == null && HealthComponent.Health != HealthComponent.MaxHealth) {
            healthBar = UIManager.Instance.EnemyHealthBarManager.GetHealthBar();
            healthBar.SetTarget(transform);
            healthBar.Init(healthComponent.MaxHealth);
            HealthComponent.OnHealthChanged += healthBar.SetValue;
            healthBar.SetValue(healthComponent.MaxHealth, healthComponent.Health);
        }

        UIManager.Instance.DamageNumberManager.ShowText($"-{dmg}",
            false,
            Camera.main.WorldToScreenPoint(transform.position + healthBarOffset)
            );
    }

    private void OnDied() {
        isDead = true;
        enemyState.enabled = false;
        agent.enabled = false;
        anim.SetBool("Dead", true);
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        collider.isTrigger = true;
        healthComponent.enabled = false;
        if (healthBar != null) {
            UIManager.Instance.EnemyHealthBarManager.FreeHealthBar(healthBar);
            healthBar = null;
        }
    }

    void FixedUpdate() {
        if (isDead) return;

        Physics2D.OverlapCircle(transform.position, aggroRange, new ContactFilter2D() { layerMask = playerLayerMask, useLayerMask = true }, detectedColliders);
        if (target == null && detectedColliders != null && detectedColliders.Length > 0)
            if (detectedColliders[0].TryGetComponent<Player>(out var player))
                target = player;

        if(target != null && TargetDistance > aggroRange + 2) {
            target = null;
            UIManager.Instance.EnemyHealthBarManager.FreeHealthBar(healthBar);
        }

        if (attackTime > 0)
            attackTime -= Time.deltaTime;

        if(healthBar != null)
            healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + healthBarOffset);

        ChangeSpriteDirection();
    }

    void ChangeSpriteDirection() {
        if(transform.position.x < lastX && characterSprite.transform.localScale.x != -spriteXScale) {
            Vector3 scale = characterSprite.transform.localScale;
            scale.x = -spriteXScale;
            characterSprite.transform.localScale = scale;
        }
        else if (transform.position.x > lastX && characterSprite.transform.localScale.x != spriteXScale) {
            Vector3 scale = characterSprite.transform.localScale;
            scale.x = spriteXScale;
            characterSprite.transform.localScale = scale;
        }
        lastX = transform.position.x;
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

    public void Clear() {
        isDead = false;
        enemyState.enabled = true;
        agent.enabled = true;
        anim.SetBool("Dead", false);
        rb.isKinematic = false;
        collider.isTrigger = false;
        healthComponent.enabled = true;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
