using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.UI;

public class EnemyAgressiveState : EnemyState
{
    [SerializeField] ParticleSystem disparo;
    [SerializeField] ParticleSystem carga;
    EnemyFOV enemyFOV;
    [SerializeField] float speedToRotate = 1f;
    Coroutine lookCoroutine;
    [SerializeField] Animator anim;
    bool chasePlayer = false;
    [SerializeField] AudioSource sound;
    private bool yaASonadoUnaVez = false;

    #region AttackPlayer
    [SerializeField] Transform fatherToRotate;

    [SerializeField] GameObject bullet;

    float timeToAttack;
    float timeResetAttack = 0.2f;

    #endregion

    NavMeshAgent navMeshAgent;

    [SerializeField] EnemyAlertState alertState;
    Vector3 lastPosPlayer;

    [SerializeField] Sprite visualizer;
    [SerializeField] Image image;

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
        if (enemyFOV.GetPlayerInside() == false)
        {
            anim.SetBool("isAnimPlayerInside", false);
            return alertState;
        }
        else
        {
            LookAtPlayer();
            UpdateAgressive();

            image.sprite = visualizer;


            return this;
        }
    }

    private void LookAtPlayer()
    {
        if(lookCoroutine != null)
        {
            StopCoroutine(lookCoroutine);
        }
        anim.SetBool("isAnimPlayerInside", true);

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
            //Instantiate(bullet,fatherToRotate.position, fatherToRotate.rotation);
            carga.Play();
            disparo.Play();

            if (disparo.isPlaying && yaASonadoUnaVez == false)
            {
                sound.Play();
                yaASonadoUnaVez = true;
            }
            Debug.Log("Atacamos");
            timeToAttack = timeResetAttack;
        }
    }

    void SetIsInside(bool isInside)
    {
        this.isInside = isInside;  
    }

}
