using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    [SerializeField] float detectionRadious = 10f;
    [SerializeField] float detectionAngle = 90f;
    [SerializeField] float chaseRadious = 20f;
    
    [SerializeField] Transform playerSeen;
    public bool playerInside = false;
    SphereCollider sphereCollider;

    bool playerInChaseRange = false;


    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();

        sphereCollider.radius = chaseRadious;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        LookForPlayer();
    }

    private void LookForPlayer()
    {
        Vector3 enemyPos = transform.position;
        Vector3 toPlayer = playerSeen.transform.position - enemyPos;
        toPlayer.y = 0;
        if(toPlayer.magnitude <= detectionRadious)
        {
            //Debug.Log("Player is Nearby");
            //Lo siguiente significa"Esta dentro del triangulo de vision"
            if (Vector3.Dot(toPlayer.normalized, transform.forward) >
                     Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad))
            {
                //Debug.Log("Player detected inside");
                playerInside = true;
            }
            else
            {
                playerInside = false;
                //ia.MoveToPoints();
                //Debug.Log("Esta fuera de rango");
            }
        }else
        {
            playerInside = false;
            //ia.MoveToPoints();
            //Debug.Log("Ya no lo veo");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Color c = new Color(0.2f, 0.9f, 0.9f, 0.4f);
        UnityEditor.Handles.color = c;

        Vector3 rotatedFordward = Quaternion.Euler(0, -detectionAngle * 0.5f, 0) * transform.forward;

        UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, rotatedFordward, detectionAngle, detectionRadious);

        Color b = new Color(0.9f, 0.7f, 1f, 0.3f);
        UnityEditor.Handles.color = b;

        UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, rotatedFordward, 360f, chaseRadious);

    }


    public bool GetPlayerInside()
    {
        return playerInside;
    }

    public Transform GetPlayerTransform()
    {
        return playerSeen;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            playerInChaseRange = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            playerInChaseRange = false;
        }
    }

}
