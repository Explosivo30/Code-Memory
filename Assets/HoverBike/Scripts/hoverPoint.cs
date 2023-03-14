using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoverPoint : MonoBehaviour
{
    public float hoverPointStrength;
    public float hoverPointDistance;
    public Transform[] hoverPoints;
    public Rigidbody rigidbody;

    void FixedUpdate()
    {
        RaycastHit hit;
        foreach (Transform hoverPoint in hoverPoints)
        {
            Vector3 downForce;
            float distancePercentage;

            if (Physics.Raycast (hoverPoint.position, hoverPoint.up * -1, out hit, hoverPointDistance))
            {
                //Distance hover Points to the ground
                distancePercentage = 1 - (hit.distance / hoverPointDistance);

                //Calculate haw mach force to push
                downForce = transform.up * hoverPointStrength * distancePercentage;
                // Correct the force for the mas and deltatime
                downForce = downForce * Time.deltaTime * rigidbody.mass;

                //Apply the force where the hover point is:
                rigidbody.AddForceAtPosition(downForce, hoverPoint.position);

            }
        }
    }
}
