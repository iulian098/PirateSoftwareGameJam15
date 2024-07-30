using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character {
    [SerializeField] protected EnemyData enemyData;
    [SerializeField] protected EnemyState enemyState;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected SpriteRenderer characterSprite;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D collider;
    [SerializeField] float aggroRange;
    [SerializeField] float attackRange;
    [SerializeField] Vector3 healthBarOffset;
    [SerializeField] LayerMask playerLayerMask;
    [SerializeField] Transform[] waypoints;
    [SerializeField] bool useRandomWaypointPosition;
    [SerializeField] float waypointChangeTime;

    Collider2D[] detectedColliders = new Collider2D[1];
    UI_EnemyHealthBar healthBar;
    protected Player target;
    int waypointIndex;
    float waypointTimer;
    float attackTime;
    protected float lastX;
    protected float spriteXScale;
    protected bool isDead;
    bool waypointReached;
    Vector3 targetScale;
    Vector3 targetMovePosition;

    public EnemyData EnemyData => enemyData;
    public EnemyState EnemyState => enemyState;
    public Player Target => target;
    public Transform[] Waypoints => waypoints;
    public int WaypointIndex => waypointIndex;
    public float AttackTime { get => attackTime; set => attackTime = value; }
    public float AttackRate => enemyData.AttackRate;
    public int Damage => enemyData.Damage;
    public bool IsDead => isDead;
    public bool IsInAttackRange => Target != null && Vector2.Distance(target.transform.position, transform.position) < attackRange;
    public float TargetDistance => Target == null ? float.MaxValue : Vector2.Distance(target.transform.position, transform.position);

    public Animator Anim => anim;

    protected override void Start() {
        healthComponent.OnDied += OnDied;
        healthComponent.OnDamageReceived += OnDamageReceived;
        lastX = transform.position.x;
        spriteXScale = characterSprite.transform.localScale.x;

        if (waypoints.Length > 0) {
            agent.SetDestination(waypoints[waypointIndex].position);
        }

        healthComponent.MaxHealth = enemyData.Health;
        targetMovePosition = transform.position;
        waypointReached = true;
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

    protected virtual void FixedUpdate() {
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

        if ((waypoints.Length > 0 || useRandomWaypointPosition) && Vector2.Distance(targetMovePosition, transform.position) < 0.01f && !waypointReached) {
            waypointReached = true;
            waypointTimer = waypointChangeTime;
        }

        if(waypointTimer > 0)
            waypointTimer -= Time.deltaTime;


        if(healthBar != null)
            healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + healthBarOffset);



        ChangeSpriteDirection();

        anim.SetBool("Run", agent.velocity.magnitude > 0.1f);
    }

    protected void ChangeSpriteDirection() {
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

        if (useRandomWaypointPosition) {
            LayerMask layer = LayerMask.GetMask("Default");
            float upDist = Physics2D.Raycast(transform.position, Vector2.up, 10, layer).distance - 0.5f;
            float downDist = Physics2D.Raycast(transform.position, Vector2.down, 10, layer).distance - 0.5f;
            float rightDist = Physics2D.Raycast(transform.position, Vector2.right, 10, layer).distance - 0.5f;
            float leftDist = Physics2D.Raycast(transform.position, Vector2.left, 10, layer).distance - 0.5f;
            waypointReached = false;
            targetMovePosition = transform.position + new Vector3(UnityEngine.Random.Range(-leftDist, rightDist), UnityEngine.Random.Range(-downDist, upDist));
        }
        else {
            waypointIndex++;
            if (waypointIndex >= waypoints.Length)
                waypointIndex = 0;
            targetMovePosition = waypoints[waypointIndex].position;
            waypointReached = false;
        }
        agent.SetDestination(targetMovePosition);
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

    public void SetTargetLocation(Vector2 position) {
        agent.SetDestination(position);
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

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, targetMovePosition);
    }
}
