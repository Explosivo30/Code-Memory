using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcitvarCinematicaLanzador : MonoBehaviour
{
    public GameObject cinematica;

    void Start()
    {
        cinematica.SetActive(false);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            cinematica.SetActive(true);
         
        }
    }
}
