using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed = 30f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        rb.AddForce(transform.forward * speed);
    }


    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
