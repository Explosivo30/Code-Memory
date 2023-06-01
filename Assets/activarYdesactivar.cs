using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activarYdesactivar : MonoBehaviour
{
    [SerializeField] GameObject canvasActivar;
    [SerializeField] GameObject canvasDesactivar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvasActivar.SetActive(true);
            canvasDesactivar.SetActive(false);
        }
    }
}
