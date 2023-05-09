using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoverPoint : MonoBehaviour
{
    public float hoverPointStrength;
    public float hoverPointDistanceYouWant;
    public float hoverPointDistance;

    public float alturaMinStartHoverBike = 2f;
    public float alturaMaxhoverBike = 7f;

    private float timeToWaitStartMovements = 30f;
    private float cuerrentTimeToWait = 0f;

    public Transform[] hoverPoints;
    public Rigidbody rigidbody;
    [SerializeField] hoverBikeController hoverBikeController;

    private void Awake()
    {
        hoverBikeController = GetComponent<hoverBikeController>();

    }
    public void Start()
    {
        hoverPointDistance = alturaMinStartHoverBike;
    }
    /*public void Update()
    {
        ControlesEstablecerAltura();
    }*/
    void FixedUpdate()
    {
        HoverPoints();
        EstablecerAlturaHoverBikeRegulable();
        Debug.DrawRay(transform.position, Vector3.down, Color.green);
    }
    public void EstablecerAlturaHoverBikeRegulable()
    {
        if (hoverBikeController.inBike == true)
        {
            cuerrentTimeToWait += 1f;
            if (cuerrentTimeToWait >= timeToWaitStartMovements)
            {
                if (hoverPointDistance < hoverPointDistanceYouWant)
                {
                    hoverPointDistance += 0.02f;
                    //cuerrentTimeToWait = 0;
                }
                if (hoverPointDistance > hoverPointDistanceYouWant)
                {
                    hoverPointDistance -= 0.08f;
                    //cuerrentTimeToWait = 0;
                }
            }
        }
        if (hoverBikeController.inBike == false)
        {
            hoverPointDistance = alturaMinStartHoverBike;
            rigidbody.drag = 6f;
            cuerrentTimeToWait = 0;
        }
    }
    
    public void ControlesEstablecerAltura()
    {
        if (Input.GetKeyDown(KeyCode.P) && hoverPointDistance < alturaMaxhoverBike)
        {
            hoverPointDistance += 0.5f;
        }
        if (Input.GetKeyDown(KeyCode.O) && hoverPointDistance > alturaMinStartHoverBike)
        {
            hoverPointDistance -= 0.5f;
        }
    }
    private void HoverPoints()
    {
        RaycastHit hit;
        foreach (Transform hoverPoint in hoverPoints)
        {
            Vector3 downForce;
            float distancePercentage;

            if (Physics.Raycast(hoverPoint.position, hoverPoint.up * -1, out hit, hoverPointDistance))
            {
                //Distance hover Points to the ground
                distancePercentage = 1 - (hit.distance / hoverPointDistance);

                //Calculate haw mach force to push
                downForce = transform.up * hoverPointStrength * distancePercentage;
                // Correct the force for the mas and deltatime
                downForce = downForce * Time.deltaTime * rigidbody.mass;

                //Apply the force where the hover point is:
                rigidbody.AddForceAtPosition(downForce, hoverPoint.position);

                //Debug.DrawRay(transform.position, Vector3.down, Color.green);

            }
        }
    }
}
