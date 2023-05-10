using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class AikaDisapear : MonoBehaviour
{
    [SerializeField] List<Renderer> meshies;
    [SerializeField] bool startDisapear;
    float disappear = 0;
    float appear = 1;
    bool disapearDone = false;
    bool appearDone = false;
    IEnumerator coroutine;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startDisapear == true && disapearDone == false)
        {
            disapearDone = true;
            foreach (Renderer meshes in meshies)
            {
                Material mat = meshes.sharedMaterial;
                //mat.SetFloat("_Disolve_Amount", disappear);
                Debug.Log("Disappear");
                coroutine = WaitToDisappear(disappear, mat);
                StartCoroutine(coroutine);
            }
            appearDone=false;
        }
        else if(startDisapear == false && appearDone == false)
        {
            appearDone = true;
            foreach (Renderer meshes in meshies)
            {
                Material mat = meshes.sharedMaterial;
                //mat.SetFloat("_Disolve_Amount", appear);
                Debug.Log("Appear");
                coroutine = WaitToAppear(appear, mat);
                StartCoroutine(coroutine);
            }
            disapearDone=false;
        }
    }

    IEnumerator WaitToDisappear(float origin, Material mat)
    {
        float counter = 1f;
        Debug.Log("disdeb");
        while(origin < 1f)
        {
            origin += counter * Time.deltaTime;
            Debug.Log(origin + " es el origin");
            mat.SetFloat("_Disolve_Amount", origin);
            Debug.Log("Disapear");
            yield return new WaitForSeconds(0.07f);
        }
        
        

    }

    IEnumerator WaitToAppear(float origin, Material mat)
    {
        float counter = 1f;
        Debug.Log("apdeb");
        while (counter > 0.1f)
        {
            origin -= counter * Time.deltaTime;
            mat.SetFloat("_Disolve_Amount", origin);
            yield return new WaitForSeconds(0.07f);
        }

        
    }

}
