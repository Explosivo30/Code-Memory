using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;


public class AttackPlayer : MonoBehaviour
{
    bool isAiming = false;

    [SerializeField] AudioSource knifeShoot;
    #region Disparar Cuchillo
    [Tooltip("Cooldown entre cuchillo y cuchillo")]
    [SerializeField] float cooldownAttack = 2f;
    float currentCooldownAttack;
    [SerializeField] GameObject cuchillo;
    [SerializeField] Transform cameraPlayer;
    [SerializeField] Transform gunPoint; 

    #endregion
    [Tooltip("LayerMask que tienen los enemigos")]
    [SerializeField] LayerMask enemies;
    [SerializeField] float rangeBackKill = 3.5f;

    #region SlowTime
    float slowedTimeScale = 0.2f;
    bool timeIsSlowed = false;

    #endregion


    private void Awake()
    {
       
    }

    void Start()
    {
        currentCooldownAttack = cooldownAttack;
        EventForGame.instance.hitmarker.AddListener(Hitmarker);
    }

    // Update is called once per frame
    void Update()
    {
        CooldownAttack();

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit ray;
            if(Physics.Raycast(transform.position,transform.forward,out ray, rangeBackKill, enemies))
            {
                Vector3 dirEnemyToPlayer = (transform.position - ray.transform.position).normalized;

                float dot = Vector3.Dot(ray.transform.forward, dirEnemyToPlayer);

                float backstabOffset = .2f;
                if (dot < -1f + backstabOffset)
                {
                    Debug.Log("Backstab");
                    EnemyCombat enemyCombat = ray.transform.GetComponentInParent<EnemyCombat>();
                    enemyCombat.GetAssasinated();
                }
            }
            
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (currentCooldownAttack <= 0f)
            {
                ShootKnife();
                knifeShoot.Play();
                currentCooldownAttack = cooldownAttack;
            }
           
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            timeIsSlowed = true;
            Time.timeScale = slowedTimeScale;

        }
        else
        {
            Time.timeScale = 1f;
            timeIsSlowed = false;
            
        }

    }

    private void CooldownAttack()
    {
        currentCooldownAttack -= Time.deltaTime;
    }

    void ShootKnife()
    {
        Instantiate(cuchillo,gunPoint.position,cameraPlayer.rotation);
    }

    void Hitmarker()
    {
        Debug.Log("HITMARKER SPAWN");
    }


   


}