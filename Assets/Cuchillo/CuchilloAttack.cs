using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuchilloAttack : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float velocity = 25f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(transform.forward * velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }


}
