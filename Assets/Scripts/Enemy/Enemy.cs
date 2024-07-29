using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character {
    [SerializeField] EnemyData enemyData;
    [SerializeField] EnemyState enemyState;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] SpriteRenderer characterSprite;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D collider;
    [SerializeField] float aggroRange;
    [SerializeField] float attackRange;
    [SerializeField] Vector3 healthBarOffset;
    [SerializeField] LayerMask playerLayerMask;
    [SerializeField] Transform[] waypoints;
    [SerializeField] float waypointChangeTime;

    Collider2D[] detectedColliders = new Collider2D[1];
    UI_EnemyHealthBar healthBar;
    Player target;
    int waypointIndex;
    float waypointTimer;
    float attackTime;
    float lastX;
    float spriteXScale;
    bool isDead;
    bool waypointReached;
    Vector3 targetScale;

    public EnemyData EnemyData => enemyData;
    public EnemyState EnemyState => enemyState;
    public Player Target => target;
    public Transform[] Waypoints => waypoints;
    public int WaypointIndex => waypointIndex;
    public float AttackTime { get => attackTime; set => attackTime = value; }
    public float AttackRate => enemyData.AttackRate;
    public int Damage => enemyData.Damage;
    public bool IsDead => isDead;
    public bool IsInAttackRange => Vector2.Distance(target.transform.position, transform.position) < attackRange;
    public float TargetDistance => Vector2.Distance(target.transform.position, transform.position);

    public Animator Anim => anim;

    private void Start() {
        healthComponent.OnDied += OnDied;
        healthComponent.OnDamageReceived += OnDamageReceived;
        lastX = transform.position.x;
        spriteXScale = characterSprite.transform.localScale.x;

        if (waypoints.Length > 0) {
            agent.SetDestination(waypoints[waypointIndex].position);
        }

        healthComponent.MaxHealth = enemyData.Health;
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
        collider.enabled = false;
        characterSprite.gameObject.SetActive(false);

        Instantiate(InGameManager.Instance.InGameData.DeathVFX, transform.position, Quaternion.identity);

        List<DropData> droppedItems = new List<DropData>();
        foreach (var item in enemyData.Drops) {
            int drop = UnityEngine.Random.Range(0, 101);
            if (drop <= item.chance) {
                droppedItems.Add(item);
            }
        }

        if(droppedItems.Count > 0) {
            ItemDrop dropObj = Instantiate(InGameManager.Instance.InGameData.DropPrefab, transform.position, Quaternion.identity);
            dropObj.Init(droppedItems);
        }

        if (healthBar != null) {
            UIManager.Instance.EnemyHealthBarManager.FreeHealthBar(healthBar);
            healthBar = null;
        }

        Destroy(gameObject);
    }

    void FixedUpdate() {
        if (isDead) return;

        int detected = Physics2D.OverlapCircle(transform.position, aggroRange, new ContactFilter2D() { layerMask = InGameManager.Instance.InGameData.PlayerMask, useLayerMask = true }, detectedColliders);
        if (detected > 0 && target == null && detectedColliders != null && detectedColliders.Length > 0)
            if (detectedColliders[0].TryGetComponent<Player>(out var player))
                target = player;

        if(target != null && TargetDistance > aggroRange + 2) {
            target = null;
            if(healthBar != null)
                UIManager.Instance.EnemyHealthBarManager.FreeHealthBar(healthBar);
        }

        if (attackTime > 0)
            attackTime -= Time.deltaTime;

        if (waypoints.Length > 0 && Vector2.Distance(waypoints[waypointIndex].position, transform.position) < 0.01f && !waypointReached) {
            waypointReached = true;
            waypointTimer = waypointChangeTime;
        }

        if(waypointTimer > 0)
            waypointTimer -= Time.deltaTime;


        if(healthBar != null)
            healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + healthBarOffset);



        ChangeSpriteDirection();

        anim.SetBool("Run", agent.speed > 0.1f);
    }

    void ChangeSpriteDirection() {
        if(transform.position.x < lastX - 0.01f && characterSprite.transform.localScale.x != -spriteXScale) {
            targetScale = characterSprite.transform.localScale;
            targetScale.x = -spriteXScale;
            characterSprite.transform.localScale = targetScale;
        }
        else if (transform.position.x > lastX + 0.01f && characterSprite.transform.localScale.x != spriteXScale) {
            targetScale = characterSprite.transform.localScale;
            targetScale.x = spriteXScale;
            characterSprite.transform.localScale = targetScale;
        }
        if (transform.position.x > lastX + 0.01f || transform.position.x < lastX - 0.01f)
            lastX = transform.position.x;
    }

    public void GoToNextWaypoint() {
        if (waypointTimer > 0 || !waypointReached) return;
        waypointIndex++;
        if (waypointIndex >= waypoints.Length)
            waypointIndex = 0;

        waypointReached = false;

        agent.SetDestination(waypoints[waypointIndex].position);
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

    public override void ReceiveDamage(WeaponData weaponData) {

        int tempDamage = weaponData.Damage;

        foreach(var weak in enemyData.Weakness) {
            if(weak.type == weaponData.ApplyStatusEffect)
                tempDamage += (int)(weaponData.Damage * weak.value);
        }

        foreach (var strong in enemyData.Strong) {
            if (strong.type == weaponData.ApplyStatusEffect)
                tempDamage -= (int)(weaponData.Damage * strong.value);
        }

        if (tempDamage < 0) tempDamage = 0;

        healthComponent.ReceiveDamage(tempDamage);
    }

    public void Clear() {
        isDead = false;
        enemyState.enabled = true;
        agent.enabled = true;
        anim.SetBool("Dead", false);
        rb.isKinematic = false;
        collider.isTrigger = false;
        healthComponent.enabled = true;
        characterSprite.gameObject.SetActive(true);
        collider.enabled = true;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
