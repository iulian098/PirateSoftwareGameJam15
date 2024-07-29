using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PluggableAI {
    [CreateAssetMenu(fileName = "Vampire_MoveAction", menuName = "PluggableAI/Actions/Vampire/Move Attack")]
    public class Vampire_MoveAction : Action {
        public override void Act(Enemy controller) {
            float upDist = controller.transform.position.y + Physics2D.Raycast(controller.transform.position, Vector2.up, 10, LayerMask.GetMask("Default")).distance;
            float downDist = controller.transform.position.y - Physics2D.Raycast(controller.transform.position, -Vector2.up, 10, LayerMask.GetMask("Default")).distance;
            float rightDist = controller.transform.position.x + Physics2D.Raycast(controller.transform.position, Vector2.right, 10, LayerMask.GetMask("Default")).distance;
            float leftDist = controller.transform.position.y - Physics2D.Raycast(controller.transform.position, Vector2.left, 10, LayerMask.GetMask("Default")).distance;

            controller.SetTargetLocation(new Vector2(Random.Range(leftDist, rightDist), Random.Range(downDist, upDist)));

            (controller as Vampire_BossEnemy).ChangePositionTimer = (controller as Vampire_BossEnemy).ChangePositionTime;
        }
    }
}
