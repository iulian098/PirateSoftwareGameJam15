using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI {

    [CreateAssetMenu(fileName = "Vampire_RangedAttackDecision", menuName = "PluggableAI/Decisions/Vampire/Ranged Attack Decision")]
    public class Vampire_RangedAttackDecision : Decision {
        public override bool Decide(Enemy controller) {
            return controller.TargetDistance < (controller as Vampire_BossEnemy).RangedAttackRange && controller.TargetDistance > (controller as Vampire_BossEnemy).MeeleAttackRange;
        }
    }
}
