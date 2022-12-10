using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABehaviour : MonoBehaviour
{
    [Header("Points Travel")]
    [SerializeField] Transform points;
    UnityEngine.AI.NavMeshAgent navMeshAgent;
    int count = 0;
    [SerializeField] float threshold = 0.2f;
    [Header("AttackPlayer")]
    [SerializeField] Transform target;
    EnemyFOV efov;
    
    
    //[SerializeField] float enemyDamage = 5;
    [SerializeField] float attackCooldown = 2f;
    [SerializeField] float enemyHealth = 20;

    
    
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //inside = GetComponentInChildren<PlayerInside>();
        navMeshAgent.SetDestination(points.GetChild(count).transform.position);

        efov = GetComponent<EnemyFOV>();

    }

    private void FixedUpdate()
    {
        if (efov.playerInside == false)
        {
            MoveToPoints();
        }
        else
        {
            AttackRange();
        }
    }

    // Update is called once per frame
    void Update()
    {
     
        attackCooldown = attackCooldown - Time.deltaTime;

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }

    }

    
    void AttackRange()
    {
        if (efov.playerInside == false)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.destination = target.position;
        }
        else if (efov.playerInside == true)
        {
            navMeshAgent.isStopped = true;
            if (attackCooldown <= 0f)
            {
                Debug.Log("He atacado");
                Attack();
            }

        }
    }
    void Attack()
    {
        transform.LookAt(target);
        //anim.SetTrigger("Attacks");
        //pCombat.TakeDamage(enemyDamage);
        attackCooldown = 2f;
        Debug.Log("Ataque");
    }
    

    public void EnemyTakeDamage(float damage)
    {
        enemyHealth -= damage;
        Debug.Log("El enemigo tiene " + enemyHealth);
    }


    public void MoveToPoints()
    {
        navMeshAgent.isStopped = false;
        Debug.Log("He empezado el MovetoPoints");
        if (Vector3.Distance(transform.position, points.GetChild(count).transform.position) < threshold)
        {
            count++;
            if (count >= points.childCount)
            {
                count = 0;
            }
            Debug.Log("Estoy buscando el numero de movetopoints");
        }
        Debug.Log("He acabado el moveToPoints");
        navMeshAgent.SetDestination(points.GetChild(count).transform.position);
        Debug.Log("The count is " + count);
    }

    private void OnParticleCollision(GameObject particles)
    {

        Debug.Log("Particula Tocada");
        if (particles.tag == "Tornado")
        {
            //enemyHealth = enemyHealth - pCombat.tornadoDamage;
            Debug.Log("Con el tornado tengo " + enemyHealth);
        }
    }
}
