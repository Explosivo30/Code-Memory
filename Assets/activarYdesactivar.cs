using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activarYdesactivar : MonoBehaviour
{
    [SerializeField] GameObject canvasActivar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvasActivar.SetActive(true);
            Invoke("DesactivarTutorial", 20f);
            
        }
    }
    void DesactivarTutorial()
    {
        canvasActivar.SetActive(false);
    }
}
