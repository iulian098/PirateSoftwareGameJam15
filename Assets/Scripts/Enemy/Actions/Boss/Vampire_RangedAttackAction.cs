using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI {
    [CreateAssetMenu(fileName = "Vampire_RangedAttackAction", menuName = "PluggableAI/Actions/Vampire/Ranged Attack")]
    public class Vampire_RangedAttackAction : Action {
        public override void Act(Enemy controller) {
            if(controller.AttackTime < 0) {
                controller.Target.HealthComponent.ReceiveDamage(controller.Damage);
                //controller.Anim.Play(LowRangeAttackHash);
                controller.AttackTime = controller.AttackRate;
            }
        }
    }
}
