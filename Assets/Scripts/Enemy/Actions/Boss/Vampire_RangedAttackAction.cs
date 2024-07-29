using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI {
    [CreateAssetMenu(fileName = "Vampire_RangedAttackAction", menuName = "PluggableAI/Actions/Vampire/Ranged Attack")]
    public class Vampire_RangedAttackAction : Action {
        
        readonly int RangegAttackHash = Animator.StringToHash("Ranged");

        public override void Act(Enemy controller) {
            if(controller.AttackTime <= 0 && controller.TargetDistance > (controller as Vampire_BossEnemy).MeeleAttackRange && controller.TargetDistance < (controller as Vampire_BossEnemy).RangedAttackRange) {
                //controller.Target.HealthComponent.ReceiveDamage(controller.Damage);
                controller.StartCoroutine((controller as Vampire_BossEnemy).SpawnBats(5));
                controller.Anim.Play(RangegAttackHash);
                controller.AttackTime = controller.AttackRate;
            }
        }
    }
}
