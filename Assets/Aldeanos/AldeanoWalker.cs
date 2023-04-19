using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AldeanoWalker : MonoBehaviour
{
    [SerializeField] Transform points;
    NavMeshAgent navMeshAgent;
    int count = 0;
    [SerializeField]float threshold = .5f;

    AldeanoLookPlayer aldeano;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        ResetPath();
        aldeano = GetComponent<AldeanoLookPlayer>();
    }

    private void FixedUpdate()
    {
        MoveToPoints();
    }

    public void MoveToPoints()
    {
       
        //navMeshAgent.isStopped = false;
        //Debug.Log("He empezado el MovetoPoints");
        if (Vector3.Distance(transform.position, points.GetChild(count).transform.position) < threshold)
        {
            ResetPath();
            /*
            if (count >= points.childCount)
            {
                count = 0;
            }
            */
            //Debug.Log("Estoy buscando el numero de movetopoints");
        }
        //Debug.Log("He acabado el moveToPoints");
        navMeshAgent.SetDestination(points.GetChild(count).transform.position);
        //Debug.Log("The count is " + count);
    }

    void ResetPath()
    {
        count = Random.Range(0, points.childCount);
    }

}
