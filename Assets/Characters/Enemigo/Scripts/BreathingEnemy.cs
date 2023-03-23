using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathingEnemy : MonoBehaviour
{
    [SerializeField] Material mat;
    [SerializeField] Renderer rendererEnemy;
    [SerializeField] float defaultIntensity;

    float intensity;



    void Start()
    {
        mat = rendererEnemy.GetComponent<Renderer>().material;
    }

    void Update()
    {

        intensity = Mathf.Sin(Time.time * defaultIntensity) * Time.deltaTime;
        mat.SetColor("_EmissionColor", Color.blue * intensity);
    }
}
