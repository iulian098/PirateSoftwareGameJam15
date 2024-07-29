using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI {
    [CreateAssetMenu(fileName = "Vampire_LowRangeAttackAction", menuName = "PluggableAI/Actions/Vampire/Low Range Attack")]

    public class Vampire_LowRangeAttackAction : Action {
        readonly int LowRangeAttackHash = Animator.StringToHash("LowRangeAttack");
        public override void Act(Enemy controller) {
            if (controller.AttackTime <= 0) {
                controller.Target.HealthComponent.ReceiveDamage(controller.Damage);
                controller.Anim.Play(LowRangeAttackHash);
                controller.AttackTime = controller.AttackRate;
            }
        }
    }
}
