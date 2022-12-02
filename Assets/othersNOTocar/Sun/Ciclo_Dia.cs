using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ciclo_Dia : MonoBehaviour
{
    public GameObject Sun;
    [Header("Estado Actual")]
    public bool night = false;
    [Space(5)]
    //Velocidad del Sol
    public float speed = 1f;

    //Rotacion que lleva el sol
    public float currentRotation = 0f;

    [Header("Velocidad de Cambio de Temperatura")]
    //Change speed Temperature
    public float changeTemperature = 1000f;

    //Rotacion * DeltaTime
    private float rotation = 0f;
    // Rotacion en la cual se transformara la rotacion para pasar a ser de noche
    private float maxRotation = 190f;

    private Light lt;
    // Start is called before the first frame update
    void Start()
    {
        lt = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        SunRotation();
        ChangeColor();
    }
    public void SunRotation()
    {
        rotation += speed;
        rotation *= Time.deltaTime;
        Sun.transform.Rotate(rotation, 0f, 0.0f, Space.Self);

        currentRotation += rotation;
        if (currentRotation >= maxRotation)
        {
            Debug.Log("TocaNoche");
            Sun.transform.Rotate(-190f, 0f, 0.0f, Space.Self);
            currentRotation = 0.0f;
            if (night == true)
            {
                night = false;
            }
            else if (night == false)
            {
                night = true;
            }
        }
    }
    public void ChangeColor()
    {
        if (night == false)
        {
            lt.colorTemperature = 5336f;
            lt.intensity = 82229.3f;
        }
        if (night == true)
        {
            lt.colorTemperature = 15000f;
            lt.intensity = 1f;
        }
        if (currentRotation >= 90)
        {
            //lt.colorTemperature -= changeTemperature * Time.deltaTime;
            //lt.intensity -= 1000f * Time.deltaTime;
        }
        else if (currentRotation <= 90)
        {
            //lt.colorTemperature += changeTemperature * Time.deltaTime;
            //lt.intensity += 10000f * Time.deltaTime;
        }
    }
}
