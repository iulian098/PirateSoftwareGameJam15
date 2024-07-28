using UnityEngine;

namespace PluggableAI {
    [CreateAssetMenu(fileName = "AttackAction", menuName = "PluggableAI/Actions/AttackAction")]
    public class AttackAction : Action {

        public override void Act(Enemy controller) {
            if (controller.Target != null && controller.IsInAttackRange && controller.AttackTime <= 0) {
                controller.Target.HealthComponent.ReceiveDamage(controller.Damage);
                controller.AttackTime = controller.AttackRate;
                controller.Animator.SetTrigger("Attack");
                if(controller.EnemyData.AttackSoundData.clip.Length > 0)
                    SoundManager.PlaySound(controller.transform.position, controller.EnemyData.AttackSoundData);
            }
        }
    }

}
