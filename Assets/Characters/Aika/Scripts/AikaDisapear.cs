using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AikaDisapear : MonoBehaviour
{
    [SerializeField] List<Renderer> meshies;
    [SerializeField] bool startDisapear;
    float disappear = 0;
    float appear = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startDisapear == true)
        {
            foreach (Renderer meshes in meshies)
            {
                Material mat = meshes.sharedMaterial;
                //mat.SetFloat("_Disolve_Amount", disappear);
                Debug.Log("sumo");
                WaitToDisappear(disappear);
            }
        }
        else
        {
            foreach (Renderer meshes in meshies)
            {
                Material mat = meshes.sharedMaterial;
                //mat.SetFloat("_Disolve_Amount", appear);
                Debug.Log("sumo");
                WaitToDisappear(appear);
                
            }
        }
    }

    IEnumerator WaitToDisappear(float origin)
    {
        float counter;
        if(origin > 50)
        {
            counter = 0f;
            counter += 30f * Time.deltaTime;
            Debug.Log("sumo");
            if(counter >= 99f)
            {
                yield return origin;
            }
        }
        else
        {
            counter = 100f;
            counter -= 30f * Time.deltaTime;
            Debug.Log("Resto");
            if (counter >= 99f)
            {
                yield return origin;
            }
        }

        

    }

}
