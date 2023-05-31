using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] Transform myTransform;


 
    void Attacked()
    {
        Debug.Log("ESTOY INTENTANDO MATARLO");
       
    }

    public void GetAssasinated()
    {
        Destroy(gameObject);
    }
    //Hola Edgar m'agraden los flors vermelles y els keebabs, espero que a tu també. Aquest codi es molt bonic, com els canelon beshamel.
}
