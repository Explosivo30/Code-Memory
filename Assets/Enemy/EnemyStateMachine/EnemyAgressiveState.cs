using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class EnemyAgressiveState : EnemyState
{
    EnemyFOV enemyFOV;
    [SerializeField] float speedToRotate = 1f;
    Coroutine lookCoroutine;

    bool chasePlayer = false;

    #region AttackPlayer
    [SerializeField] Transform fatherToRotate;

    [SerializeField] GameObject bullet;

    float timeToAttack;
    float timeResetAttack = 3f;

    #endregion

    NavMeshAgent navMeshAgent;

    [SerializeField] EnemyAlertState alertState;
    Vector3 lastPosPlayer;
    bool isInside = true;
    
    //MAKE IT FASTER

    //RANGE
    //
    //ATTACK

    private void Awake()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        enemyFOV = GetComponentInParent<EnemyFOV>();
        timeToAttack = timeResetAttack;
    }

    public override EnemyState RunCurrentState()
    {
        if (isInside == false)
        {
            return alertState;
        }
        else
        {
            LookAtPlayer();
            UpdateAgressive();
            
            return this;
        }
    }

    private void LookAtPlayer()
    {
        if(lookCoroutine != null)
        {
            StopCoroutine(lookCoroutine);
        }

        lookCoroutine = StartCoroutine(LookAt());
    }

    private IEnumerator LookAt()
    {
        Quaternion lookRotation = Quaternion.LookRotation(enemyFOV.GetPlayerTransform().position - transform.position);

        float time = 0;

        while(time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);

            time += Time.deltaTime * speedToRotate;
        }

        yield return null;

    }

    private void UpdateAgressive()
    {
        
        if (enemyFOV.GetPlayerInside() == true)
        {
            fatherToRotate.LookAt(enemyFOV.GetPlayerTransform().position);
            navMeshAgent.SetDestination(transform.position);
            lastPosPlayer = enemyFOV.GetPlayerTransform().position;
            timeToAttack -= Time.deltaTime;
            AttackPlayer();
        }
        else
        {

            timeToAttack = timeResetAttack;

            //Last time we saw the player, Save Vector3;
            if (Vector3.Distance(enemyFOV.transform.position, lastPosPlayer) < 5f)
            {
                navMeshAgent.SetDestination(lastPosPlayer);
            }
            else
            {
                SetIsInside(false);
            }
            
        }
    }

    private void AttackPlayer()
    {
        
        //Spawn gameobject with constant force until we have particles;
        if (timeToAttack < 0)
        {
            //playerAttack
            Instantiate(bullet,fatherToRotate.position, fatherToRotate.rotation);
            Debug.Log("Atacamos");
            timeToAttack = timeResetAttack;
        }
    }

    void SetIsInside(bool isInside)
    {
        this.isInside = isInside;  
    }

}
