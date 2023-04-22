using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAround : MonoBehaviour
{
    [SerializeField] GameObject aika;
    [SerializeField] float velocidadTurnAround = 15f;

    void Update()
    {
        transform.RotateAround(aika.transform.position, Vector3.up, velocidadTurnAround * Time.deltaTime);
    }
}
