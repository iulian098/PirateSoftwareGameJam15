using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthComponent
{
    [SerializeField] GameObject dieParticles;

    public override void ReceiveDamage(int dmg) {
        base.ReceiveDamage(dmg);

        //Show damage number
    }

    protected override void Die() {
        if (isDead) return;
        base.Die();
        
        Instantiate(dieParticles, transform.position, Quaternion.identity);
    }
}
