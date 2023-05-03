using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boostspeed : MonoBehaviour
{
    [SerializeField] Rigidbody Rb;
    public float addSpeed = 10000f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boost"))
        {
            Rb.AddForce(transform.forward * addSpeed);
        }
    }

}
