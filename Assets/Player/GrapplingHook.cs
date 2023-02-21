using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
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
    [SerializeField]KeyCode grappleKey = KeyCode.Mouse1;


    bool grappling = false;
    /*
    private void Awake()
    {
        movementPlayer = GetComponent<MovementPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(grappleKey)) { StartGrapple(); }

        if (grapplingCdTimer > 0) { grapplingCdTimer -= Time.deltaTime; }
            
    }
    
        private void LateUpdate()
        {
            if (grappling)
            {
                lr.SetPosition(0, gunTip.position);
            }
        }

        void StartGrapple()
        {
            if (grapplingCdTimer > 0) return;

            grappling = true;

            movementPlayer.freeze = true;

            RaycastHit hit;

            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, maxGrapplingDistance, whatIsGrappable))
            {
                grapplePoint = hit.point;

                Invoke(nameof(ExecuteGrapple), grappleDelayTime);
            }
            else
            {
                grapplePoint = cameraTransform.position + cameraTransform.forward * maxGrapplingDistance;

                Invoke(nameof(StopGrapple), grappleDelayTime);
            }

            lr.enabled = true;

            lr.SetPosition(1, grapplePoint);

        }


        void ExecuteGrapple()
        {
            movementPlayer.freeze = false;

            Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

            float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
            float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

            if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis;

            movementPlayer.JumpToPosition(grapplePoint, highestPointOnArc);

            Invoke(nameof(StopGrapple), 8f);

        }

        void StopGrapple()
        {
            movementPlayer.freeze = false;

            grappling = false;
            grapplingCdTimer = grapplingCd;
            lr.enabled = false;


            Invoke(nameof(ResetGrappleVelocity), 0.1f);
        }

        void ResetGrappleVelocity()
        {
            movementPlayer.ResetVelocity();
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Debug.Log("Le dimos algo con el collider hit");
            StopGrapple();
        }

        */
}
