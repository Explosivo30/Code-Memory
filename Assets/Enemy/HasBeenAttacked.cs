using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasBeenAttacked : MonoBehaviour
{
    [SerializeField]Transform parentTrans;

    private void Start()
    {
        
    }


    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("ALGO NOS DIO + " + collision.transform.name);
        if (collision.transform.tag == "Cuchillo")
        {
            Destroy(parentTrans.gameObject);
        }
    }

}
