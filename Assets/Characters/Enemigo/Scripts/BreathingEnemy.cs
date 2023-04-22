using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathingEnemy : MonoBehaviour
{
    [SerializeField] Material mat;
    [SerializeField] Renderer rendererEnemy;
    [SerializeField] float maxIntensity;
    [SerializeField] float minIntensity;
    [SerializeField] float extraTime = 0.5f;
    [SerializeField] float intensity;
    bool isPositive = true;

    //float intensity;



    void Start()
    {
        mat = rendererEnemy.GetComponent<Renderer>().sharedMaterial;
    }

    //[HDR] _EmissiveColor("EmissiveColor", Color) = (0, 0, 0)
    //    // Used only to serialize the LDR and HDR emissive color in the material UI,
    //    // in the shader only the _EmissiveColor should be used
    //    [HideInInspector] _EmissiveColorLDR("EmissiveColor LDR", Color) = (0, 0, 0)
    //    _EmissiveColorMap("EmissiveColorMap", 2D) = "white" {}
    //    [ToggleUI] _AlbedoAffectEmissive("Albedo Affect Emissive", Float) = 0.0
    //    _EmissiveIntensityUnit("Emissive Mode", Int) = 0
    //    [ToggleUI] _UseEmissiveIntensity("Use Emissive Intensity", Int) = 0
    //    _EmissiveIntensity("Emissive Intensity", Float) = 1
    void Update()
    {
        //intensity = Mathf.Sin(Time.time * extraTime * defaultIntensity);
        //intensity = Mathf.Lerp(intensity, maxIntensity, extraTime * Time.deltaTime);



        
        
        if (isPositive)
        {
            
            intensity = Mathf.Lerp(intensity, maxIntensity, extraTime * Time.deltaTime);
            if(intensity >= maxIntensity - 1f)
            {
                isPositive = false;
            }
        }

        if (!isPositive)
        {
            
            intensity = Mathf.Lerp(intensity, minIntensity, extraTime * Time.deltaTime);

            if (intensity <= minIntensity + 1f)
            {
                isPositive = true;
            }

        }

        
        //mat.SetColor("_EmissionColor", Color.blue * intensity);     // No barrufa
        //mat.SetFloat("_EmissiveIntensity", intensity);            // No barrufa
        //mat.SetColor("_EmissiveColorLDR", Color.blue * intensity);            // No barrufa

        //mat.SetFloat("_EmissiveExposureWeight", intensity);         // Hace algo

        mat.SetColor("_EmissiveColor", new Color(255, 153, 153) * intensity);
    }
}
