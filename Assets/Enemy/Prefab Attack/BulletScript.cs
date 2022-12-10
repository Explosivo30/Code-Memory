using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        rb.AddForce(transform.forward * 20f);
    }


    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
