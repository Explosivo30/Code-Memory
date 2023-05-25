using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniEyes : MonoBehaviour
{
    [SerializeField] Transform parentTransform;
    [SerializeField] float speedRotate = 20f;
    [SerializeField] bool detenerMovimiento = false;
    void Start()
    {
        
    }
    void Update()
    {
        if (detenerMovimiento == false)
        {
            transform.RotateAround(parentTransform.position, Vector3.up, speedRotate * Time.deltaTime);
        }
    }
    public void DetenerMovimneto()
    {
        detenerMovimiento = true;
    }
}
