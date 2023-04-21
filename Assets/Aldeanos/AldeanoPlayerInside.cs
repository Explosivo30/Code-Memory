using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AldeanoPlayerInside : MonoBehaviour
{
    public bool isPlayerInside = false;

    Transform playermove;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerInside = true;
            playermove = GetComponent<Transform>();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }


    public bool GetIsInsidePlayer()
    {
        return isPlayerInside;
    }


}
