using PluggableAI;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] State currentState;
    [SerializeField] State remainInState;


    private void FixedUpdate() {
        if (currentState != null)
            currentState.UpdateState(enemy);
    }

    public void TransitionToState(State nextState) {
        if(nextState != remainInState)
            currentState = nextState;
    }
}
