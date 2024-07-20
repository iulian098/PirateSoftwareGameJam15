using UnityEngine;

namespace PluggableAI {
    [CreateAssetMenu(fileName = "AttackDecision", menuName = "PluggableAI/Decisions/AttackDecision")]
    public class AttackDecision : Decision {
        public override bool Decide(Enemy controller) {
            return controller.IsInAttackRange;
        }
    }
}
