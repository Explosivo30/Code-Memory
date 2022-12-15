using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGameDesign : MonoBehaviour
{

    [SerializeField] GameObject teleportPoints;
    [SerializeField] Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerTransform.position = teleportPoints.transform.GetChild(0).position; 
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerTransform.position = teleportPoints.transform.GetChild(1).position;
        }

    }

    


}
