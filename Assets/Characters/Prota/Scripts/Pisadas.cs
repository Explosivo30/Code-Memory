using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pisadas : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] LayerMask groundLayerMask = Physics.DefaultRaycastLayers;

    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & groundLayerMask) != 0)
            { audioSource.Play();
            Debug.Log("Pasos");
            }
    }

}
