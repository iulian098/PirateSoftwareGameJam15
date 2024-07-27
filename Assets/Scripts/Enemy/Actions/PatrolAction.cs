using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI {
    [CreateAssetMenu(fileName = "PatrolAction", menuName = "PluggableAI/Actions/PatrolAction")]
    public class PatrolAction : Action {
        public override void Act(Enemy controller) {
            controller.GoToNextWaypoint();
        }
    }

}