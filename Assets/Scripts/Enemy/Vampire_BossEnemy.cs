using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire_BossEnemy : BossEnemy
{
    [SerializeField] float meeleAttackRange;
    [SerializeField] float rangedAttackRange;
    [SerializeField] float changePositionTime;
    [SerializeField] RangeWeaponData batWeapon;
    [SerializeField] Transform aimDirTransform;

    float changePositionTimer;
    bool changingPosition;
    Vector2 aimDirection;

    public float MeeleAttackRange => meeleAttackRange;
    public float RangedAttackRange => rangedAttackRange;
    public bool CanChangePosition => !changingPosition && changePositionTimer <= 0;
    public float ChangePositionTime => changePositionTime;
    public float ChangePositionTimer {get => changePositionTimer; set => changePositionTimer = value; }

    protected override void Start() {
        base.Start();
        SetTarget(InGameManager.Instance.Player);
    }

    protected override void FixedUpdate() {
        if (IsDead || Target == null) return;

        if(changePositionTimer > 0)
            changePositionTimer -= Time.deltaTime;

        if (AttackTime > 0)
            AttackTime -= Time.deltaTime;

        changingPosition = agent.isStopped;
        ChangeSpriteDirection();
    }

    public IEnumerator SpawnBats(int amount) {
        for (int i = 0; i < amount; i++) {
            yield return new WaitForSeconds(0.2f);
            aimDirection = (target.transform.position - transform.position).normalized;
            aimDirTransform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 1), aimDirection);

            Projectile proj = Instantiate(batWeapon.Projectile, aimDirTransform.position, aimDirTransform.rotation);
            Debug.Log("Spawned at pos " + aimDirTransform.position);
            float dotProduct = Vector3.Dot(Vector2.right, aimDirection);

            if (dotProduct < 0) {
                Vector3 scale = proj.transform.localScale;
                scale.y = -1;
                proj.transform.localScale = scale;
            }

            proj.Init(gameObject, aimDirTransform.up, batWeapon, batWeapon.Damage);


        }
    }

    public void SetTarget(Player player) {
        target = player;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangedAttackRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meeleAttackRange);
    }
}
