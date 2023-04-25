using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcitvarCInematica : MonoBehaviour
{
    public GameObject cinematica;
    public BossAgressive bossAgressive;

    void Start()
    {
        cinematica.SetActive(false);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            bossAgressive.desactivarScript = true;
            cinematica.SetActive(true);
         
        }
    }
}
