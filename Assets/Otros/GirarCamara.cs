using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirarCamara : MonoBehaviour
{
    [SerializeField] Transform objeto;
    [SerializeField] float velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(objeto.position,Vector3.up, velocity);
    }
}
