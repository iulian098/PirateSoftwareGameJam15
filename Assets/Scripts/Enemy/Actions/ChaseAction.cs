using UnityEngine;

namespace PluggableAI {
    [CreateAssetMenu(fileName = "ChaseAction", menuName = "PluggableAI/Actions/ChaseAction")]
    public class ChaseAction : Action {
        public override void Act(Enemy controller) {
            if(controller.Target != null && !controller.IsInAttackRange)
                controller.SetTargetLocation();
        }
    }
}
