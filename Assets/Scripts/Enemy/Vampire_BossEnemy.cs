using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire_BossEnemy : BossEnemy
{
    [SerializeField] float meeleAttackRange;
    [SerializeField] float rangedAttackRange;
    [SerializeField] float changePositionTime;
    [SerializeField] float rangeAttackTime;
    [SerializeField] RangeWeaponData batWeapon;
    [SerializeField] Transform aimDirTransform;

    float changePositionTimer;
    float rangeAttackTimer;
    bool changingPosition;
    Vector2 aimDirection;

    public float MeeleAttackRange => meeleAttackRange;
    public float RangedAttackRange => rangedAttackRange;
    public bool CanChangePosition => !changingPosition && changePositionTimer <= 0;
    public float ChangePositionTime => changePositionTime;
    public float ChangePositionTimer {get => changePositionTimer; set => changePositionTimer = value; }
    public float RangeAttackTime => rangeAttackTime;
    public float RangeAttackTimer { get => rangeAttackTimer; set => rangeAttackTimer = value; }

    protected override void Start() {
        base.Start();
        changePositionTimer = changePositionTime;
        rangeAttackTimer = rangeAttackTime;
        HealthComponent.OnDied += OnDied;
    }

    protected override void FixedUpdate() {
        if (IsDead || Target == null || !Activated) return;

        if(changePositionTimer > 0)
            changePositionTimer -= Time.deltaTime;

        if(rangeAttackTimer > 0)
            rangeAttackTimer -= Time.deltaTime;

        if (AttackTime > 0)
            AttackTime -= Time.deltaTime;

        changingPosition = agent.isStopped;
        ChangeSpriteDirection();
        anim.SetBool("Run", agent.speed > 0.1f);
    }

    public IEnumerator SpawnBats(int amount) {
        for (int i = 0; i < amount; i++) {
            yield return new WaitForSeconds(0.1f);
            aimDirection = (target.transform.position - transform.position).normalized;
            aimDirTransform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 1), aimDirection);

            Projectile proj = Instantiate(batWeapon.Projectile, aimDirTransform.position + (Vector3.up * Random.Range(-0.25f, 0.25f)), aimDirTransform.rotation);
            float dotProduct = Vector3.Dot(Vector2.right, aimDirection);

            if (dotProduct < 0) {
                Vector3 scale = proj.transform.localScale;
                scale.y = -1;
                proj.transform.localScale = scale;
            }

            proj.Init(gameObject, aimDirTransform.up, batWeapon, batWeapon.Damage);


        }
    }

    private void OnDied() {
        isDead = true;

        Instantiate(InGameManager.Instance.InGameData.DeathVFX, transform.position, Quaternion.identity);

        List<DropData> droppedItems = new List<DropData>();
        foreach (var item in enemyData.Drops) {
            int drop = UnityEngine.Random.Range(0, 101);
            if (drop <= item.chance) {
                droppedItems.Add(item);
            }
        }

        if (droppedItems.Count > 0) {
            ItemDrop dropObj = Instantiate(InGameManager.Instance.InGameData.DropPrefab, transform.position, Quaternion.identity);
            dropObj.Init(droppedItems);
        }

        if (bossHealthBar != null) {
            bossHealthBar.gameObject.SetActive(false);
        }

        Destroy(gameObject);

    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangedAttackRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meeleAttackRange);
    }
}
