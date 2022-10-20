using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HelperIA : MonoBehaviour
{
    [SerializeField] Transform target;
    
    public SphereCollider collider;
    public float FieldOfView = 90f;
    public LayerMask LineOfSightLayers;

    public delegate void GainSightEvent(Transform target);
    public GainSightEvent onGainSight;
    public delegate void LoseSightEvent(Transform target);
    public GainSightEvent onLoseSight;

    Coroutine checkForLineOfSightCoroutine;


    
    // Start is called before the first frame update

    private void Awake()
    {
        
        collider = GetComponent<SphereCollider>();
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (!CheckLineOfSight(other.transform))
        {
            checkForLineOfSightCoroutine = StartCoroutine(CheckForLineOfSight(other.transform));
        }
    }


    private void OnTriggerExit(Collider other)
    {
        onLoseSight?.Invoke(other.transform);
        if(checkForLineOfSightCoroutine != null)
        {
            StopCoroutine(checkForLineOfSightCoroutine);
        }
    }



    private bool CheckLineOfSight(Transform target)
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        float dotProduct = Vector3.Dot(transform.forward, direction);

        if(dotProduct >= Mathf.Cos(FieldOfView))
        {
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, collider.radius, LineOfSightLayers))
            {
                onGainSight?.Invoke(target);
                return true;
            }
        }
        return false;
    }

    private IEnumerator CheckForLineOfSight (Transform target)
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);

        while (!CheckLineOfSight(target))
        {
            yield return wait;
        }

    }

}
