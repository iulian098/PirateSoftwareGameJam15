using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire_BossEnemy : BossEnemy
{
    [SerializeField] float meeleAttackRange;
    [SerializeField] float rangedAttackRange;
    [SerializeField] float changePositionTime;

    float changePositionTimer;
    bool changingPosition;

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

        changingPosition = agent.isStopped;
        ChangeSpriteDirection();
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
