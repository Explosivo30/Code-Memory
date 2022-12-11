using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPrediction : MonoBehaviour
{
    public GameObject goTrackingPlayer;
    public GameObject goIndicator;
    public Vector3 v3AverageVelocity;
    public Vector3 v3AverageAcceleration;

    Vector3 v3PreviewVel;
    Vector3 v3PrevAccel;
    Vector3 v3PrevPos;



    void Start()
    {

    }

    private void LateUpdate()
    {
        StartCoroutine(Check());
    }



    IEnumerator Check()
    {
        yield return new WaitForEndOfFrame();

        Vector3 v3Velocity = (goTrackingPlayer.transform.position - v3PrevPos) / Time.deltaTime;
        Vector3 v3Accel = v3Velocity - v3PreviewVel;

        v3AverageVelocity = v3Velocity;
        v3AverageAcceleration = v3Accel;

        GetProJectedPosition(0.5f);

        v3PrevPos = goTrackingPlayer.transform.position;
        v3PreviewVel = v3Velocity;
        v3PrevAccel = v3Accel;


        
    }

    public Vector3 GetProJectedPosition(float fTime)
    {
        Vector3 v3Ret = new Vector3();
        //La equacion es:
        // X0 + V0 * t + 1/2 * Accel * t^2
        //Explicacion:
        //La current position + (la current velocidad * Time.deltatime * (tiempoque quieres ver en futuro "fTime" / Time.deltaTime)) +  (0.5f * Aceleracion * time.deltaTime * Mathf.pow(fTime / Time.deltaTime, 2))

        v3Ret = goTrackingPlayer.transform.position + (v3AverageVelocity * Time.deltaTime * (fTime / Time.deltaTime)) + (0.5f * v3AverageAcceleration * Time.deltaTime * Mathf.Pow(fTime / Time.deltaTime, 2));
        goIndicator.transform.position = v3Ret;

        return v3Ret;
    }

}
