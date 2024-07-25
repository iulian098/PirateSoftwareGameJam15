using UnityEngine;

namespace PluggableAI {
    [CreateAssetMenu(fileName = "AttackAction", menuName = "PluggableAI/Actions/AttackAction")]
    public class AttackAction : Action {

        public override void Act(Enemy controller) {
            if (controller.IsInAttackRange && controller.AttackTime <= 0) {
                controller.Target.HealthComponent.ReceiveDamage(controller.Damage);
                controller.AttackTime = controller.AttackRate;
                controller.Animator.SetTrigger("Attack");

            }
        }
    }

}
