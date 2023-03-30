using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] GameObject teleportCheckPoint;


    private void OnTriggerEnter(Collider other)
    {
        EventForGame.instance.deadWhip.Invoke();
        other.transform.position = teleportCheckPoint.transform.position;
    }
}
