using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniEyes : MonoBehaviour
{
    [SerializeField] Transform parentTransform;
    [SerializeField] float speedRotate = 20f;
    void Start()
    {
        
    }
    void Update()
    {
        transform.RotateAround(parentTransform.position, Vector3.up, speedRotate * Time.deltaTime);
    }
}
