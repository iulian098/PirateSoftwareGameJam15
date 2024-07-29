using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI {
    [CreateAssetMenu(fileName = "Vampire_MeleeAttackDecision", menuName = "PluggableAI/Decisions/Vampire/Melee Attack Decision")]

    public class Vampire_MeleeAttackDecision : Decision {
        public override bool Decide(Enemy controller) {
            return controller.TargetDistance < (controller as Vampire_BossEnemy).MeeleAttackRange;
        }
    }
}
