using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HelperMovement : MonoBehaviour
{

    public LayerMask hiddableLayers;
    public HelperIA lineOfSightChecker;
    public NavMeshAgent agent;

    [Range(-1, 1)]
    [Tooltip("Mas bajo es un mejor escondite")]

    public float hideSensitivity = 0;

    Coroutine movementCoroutine;
    Collider[] colliders = new Collider[10];



    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        lineOfSightChecker.onGainSight += HandleGainSight;
        lineOfSightChecker.onLoseSight += HandleGainSight;
    }



    private void HandleGainSight(Transform target)
    {
        if(movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
        }
    }

    IEnumerator Hide (Transform target)
    {
        while (true)
        {
            int hits = Physics.OverlapSphereNonAlloc(agent.transform.position, lineOfSightChecker.collider.radius, colliders, hiddableLayers);

            for(int i = 0; i < hits; i++)
            {
                if(NavMesh.SamplePosition(colliders[i].transform.position, out NavMeshHit hit, 2f, agent.areaMask))
                {
                    if (!NavMesh.FindClosestEdge(hit.position, out hit, agent.areaMask))
                    {
                        Debug.Log($"Unable to find edge clost to {hit.position}");
                    }

                    if(Vector3.Dot(hit.normal, (target.position-hit.position).normalized)< hideSensitivity)
                    {
                        agent.SetDestination(hit.position);
                        break;
                    }
                    else
                    {
                        if (NavMesh.SamplePosition(colliders[i].transform.position - (target.position - hit.position), out NavMeshHit hit2, 2f, agent.areaMask))
                        {
                            if (!NavMesh.FindClosestEdge(hit2.position, out hit2, agent.areaMask))
                            {
                                Debug.Log($"Unable to find edge clost to {hit2.position} (second attempt)");
                            }

                            if (Vector3.Dot(hit2.normal, (target.position - hit2.position).normalized) < hideSensitivity)
                            {
                                agent.SetDestination(hit2.position);
                                break;
                            }

                        }
                    }

                }
                else
                {
                    Debug.Log($"Unable to find NavMesh near Object to {colliders[i].name}");
                }
            }
            yield return null;
        }
    }

}
