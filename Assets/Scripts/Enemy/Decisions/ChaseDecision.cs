using UnityEngine;

namespace PluggableAI {

    [CreateAssetMenu(fileName = "ChaseDecision", menuName = "PluggableAI/Decisions/ChaseDecision")]
    public class ChaseDecision : Decision {
        public override bool Decide(Enemy controller) {
            if (!controller.PlayerDetected())
                controller.SetTargetLocation(false);
            return controller.PlayerDetected();
        }
    }

}
