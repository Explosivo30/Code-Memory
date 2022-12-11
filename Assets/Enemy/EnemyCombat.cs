using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] Transform player;
    // Start is called before the first frame update
    void Start()
    {
        //AttackEvents.attackEvents.AssasinationEvent.AddListener(Attacked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Attacked()
    {
        Debug.Log("ESTOY INTENTANDO MATARLO");
       
    }

    public void GetAssasinated()
    {
        Destroy(gameObject);
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Cuchillo")
        {
            Destroy(gameObject);
        }
    }

    //Hola Edgar m'agraden los flors vermelles y els keebabs, espero que a tu també. Aquest codi es molt bonic, com els canelon beshamel.


}
