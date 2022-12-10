using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCalmState : EnemyState
{
    [SerializeField] bool seenDeadBody = false;
    [SerializeField] EnemyAlertState enemyAlert;
    [SerializeField] EnemyFOV enemyFOV;
    [SerializeField] EnemyAgressiveState enemyAgressive;
    NavMeshAgent navMeshAgent;
    bool pathEnded = false;

    float threshold = .3f;

    float currentTimer;

    [SerializeField] Transform pathPoints;
    int pathCount = 0;
    

    private void Awake()
    {
        enemyFOV = GetComponentInParent<EnemyFOV>();
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
    }
    public override EnemyState RunCurrentState()
    {
        if (enemyFOV.GetPlayerInside()== true)
        {
            navMeshAgent.isStopped = true;
            
            return enemyAgressive;
        }
        else if(seenDeadBody == true)
        {
            return enemyAlert;
        }
        else
        {
            EnemyCalmUpdate();
            return this;
        }
    }

    void EnemyCalmUpdate()
    {

        if (pathEnded == false)
        {
            PathPoints();
        }
        else
        {
            IdleEnemy();
        }
        
        
    }
    

    void PathPoints()
    {
        navMeshAgent.isStopped = false;
        if (Vector3.Distance(transform.position,pathPoints.GetChild(pathCount).transform.position) < threshold)
        {
            pathCount= Random.Range(0, pathPoints.childCount);
            Debug.Log("El nuevo pathcount es "+pathCount);

            if (pathCount > pathPoints.childCount)
            {
                pathCount = 0;
            }

        }
        navMeshAgent.SetDestination(pathPoints.GetChild(pathCount).transform.position);
    }

    void IdleEnemy()
    {
        navMeshAgent.isStopped = true;
        //despues de un tiempo volverlo a poner a false.
        pathEnded = false;
        
    }

}
