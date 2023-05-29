using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivarPelo : MonoBehaviour
{
    [SerializeField] GameObject pelo;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pelo.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pelo.SetActive(false);
        }
    }
}
