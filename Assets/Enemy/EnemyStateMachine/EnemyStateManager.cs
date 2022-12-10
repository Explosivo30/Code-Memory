using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    [SerializeField]EnemyState currentState;
    // Update is called once per frame
    void Update()
    {
        RunEnemyStateMachine();
    }

    private void RunEnemyStateMachine()
    {
        EnemyState nextState = currentState ?.RunCurrentState();
        if(nextState != null)
        {
            SwitchToTheNextState(nextState);
        }
    }

    void SwitchToTheNextState(EnemyState nextState)
    {
        currentState = nextState;
    }

}
