using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGrapplingHook : MonoBehaviour
{
    [Header("References")]
    MovementPlayer movementPlayer;
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform gunTip;
    [SerializeField] LayerMask whatIsGrappable;
    [SerializeField] LineRenderer lr;

    [Header("Grappling")]
    [SerializeField] float maxGrapplingDistance;
    [SerializeField] float grappleDelayTime;
    [SerializeField] float overshootYAxis;

    Vector3 grapplePoint;

    [Header("Cooldown")]
    [SerializeField] float grapplingCd;
    float grapplingCdTimer;

    [Header("Input")]
    [SerializeField] KeyCode grappleKey = KeyCode.Mouse1;


    private void Awake()
    {
        movementPlayer = GetComponent<MovementPlayer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(grappleKey))
        {
            StartGrapple();
        }
    }



    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, maxGrapplingDistance, whatIsGrappable))
        {
            grapplePoint = hit.point;
        }
        ExecuteGrapple();
    }

    private void ExecuteGrapple()
    {
        
    }



}
