using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activarQuest : MonoBehaviour
{
    [SerializeField] GameObject mision1;
    [SerializeField] GameObject mision2;
    [SerializeField] bool yaHasEntrado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && yaHasEntrado == false)
        {
            mision1.SetActive(false);
            mision2.SetActive(true);
            yaHasEntrado = true;
        }
    }
}
