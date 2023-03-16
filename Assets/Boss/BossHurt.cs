using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHurt : MonoBehaviour
{
    [SerializeField] float damageRecived = 15f;
    [SerializeField] float healthBoss = 150f;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Cuchillo"))
        {
            healthBoss -= damageRecived;
            if (healthBoss <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
