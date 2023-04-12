using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAgressive : EnemyState
{
    // Start is called before the first frame update
    EnemyFOV enemyFOV;
    [SerializeField] float speedToRotate = 1f;
    Coroutine lookCoroutine;

    bool chasePlayer = false;

    #region AttackPlayer
    [SerializeField] Transform fatherToRotate;
    [SerializeField] Transform player;
    [SerializeField] GameObject bullet;

    float timeToAttack;
    float timeResetAttack = 3f;

    #endregion

    //NavMeshAgent navMeshAgent;

    [SerializeField] EnemyAlertState alertState;

    bool isInside = true;

    [SerializeField] float ySpeed;
    Vector3 followUpPlayer;
    float startingYPlayer;

    //MAKE IT FASTER

    //RANGE
    //
    //ATTACK

    private void Awake()
    {
        //navMeshAgent = GetComponentInParent<NavMeshAgent>();
        enemyFOV = GetComponentInParent<EnemyFOV>();
        timeToAttack = timeResetAttack;
        startingYPlayer = player.position.y;
    }

    public override EnemyState RunCurrentState()
    {
        //fatherToRotate.LookAt(player);
        //LookAtPlayer();
        UpdateAgressive();
        UpdateYPosition();
        return this;
    }

    private void UpdateYPosition()
    {
        followUpPlayer.y = ySpeed;
        float yPosDistance = transform.position.y - player.transform.position.y;
        Debug.Log(yPosDistance);
        if ((yPosDistance) > 0.5f)
        {
            //Debug.Log(transform.position.y - player.transform.position.y);
            enemyFOV.transform.position -= followUpPlayer;

        }
        else if((yPosDistance) < -0.5f)
        {
            enemyFOV.transform.position += followUpPlayer;
        }


        
    }

    

    private void LookAtPlayer()
    {
        if (lookCoroutine != null)
        {
            StopCoroutine(lookCoroutine);
        }

        lookCoroutine = StartCoroutine(LookAt());
    }

    private IEnumerator LookAt()
    {
        Quaternion lookRotation = Quaternion.LookRotation(enemyFOV.GetPlayerTransform().position - transform.position);

        float time = 0;

        while (time < 1)
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
            //navMeshAgent.SetDestination(transform.position);
            timeToAttack -= Time.deltaTime;
            AttackPlayer();
        }
        else
        {

            timeToAttack = timeResetAttack;

            //Last time we saw the player, Save Vector3;
            //navMeshAgent.SetDestination()
        }
    }

    private void AttackPlayer()
    {

        //Spawn gameobject with constant force until we have particles;
        if (timeToAttack < 0)
        {
            //playerAttack
            //Instantiate(bullet, fatherToRotate.position, fatherToRotate.rotation);
            Debug.Log("Atacamos");
            timeToAttack = timeResetAttack;
        }
    }


    void SetIsInside(bool isInside)
    {
        this.isInside = isInside;
    }
}
