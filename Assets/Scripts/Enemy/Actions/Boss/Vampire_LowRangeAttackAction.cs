using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI {
    [CreateAssetMenu(fileName = "Vampire_LowRangeAttackAction", menuName = "PluggableAI/Actions/Vampire/Low Range Attack")]

    public class Vampire_LowRangeAttackAction : Action {
        readonly int LowRangeAttackHash = Animator.StringToHash("Melee");
        public override void Act(Enemy controller) {
            if (controller.AttackTime <= 0 && controller.TargetDistance < (controller as Vampire_BossEnemy).MeeleAttackRange) {
                controller.Target.HealthComponent.ReceiveDamage(controller.Damage);
                controller.Anim.Play(LowRangeAttackHash);
                controller.AttackTime = controller.AttackRate;
                SoundManager.Instance.PlaySound(controller.transform.position, "MeleeSwordAttack");
            }
        }
    }
}
