using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI {
    [CreateAssetMenu(fileName = "Vampire_MoveDecision", menuName = "PluggableAI/Decisions/Vampire/Move Decision")]
    public class Vampire_MoveDecision : Decision {
        public override bool Decide(Enemy controller) {
            return (controller as Vampire_BossEnemy).CanChangePosition;
        }
    }
}
