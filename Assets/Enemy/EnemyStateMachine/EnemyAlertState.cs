using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlertState : EnemyState
{
    [SerializeField] EnemyCalmState enemyCalm;
    [SerializeField] EnemyAgressiveState enemyAgressive;
    [SerializeField] bool playerFound = false;
    public override EnemyState RunCurrentState()
    {
        if(playerFound == true)
        {
            playerFound = false;
            return enemyAgressive;
        }
        else
        {
            return this;
        }

        
        
    }

}
