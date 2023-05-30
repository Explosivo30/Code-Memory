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
        if (collision.gameObject.CompareTag("Player"))
        {
            EventForGame.instance.bossHit.Invoke();          
        }
    Invoke("Detroy", 0.1f);
    }
    void Detroy()
    {
        Destroy(gameObject);

    }
}
