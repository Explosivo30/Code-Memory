using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HasBeenAttacked : MonoBehaviour
{
    Transform parentTrans;
    EventForGame eventForGame;

    private void Awake()
    {
        parentTrans = GetComponentInParent<Transform>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.transform.tag == "Cuchillo")
        {
            EventForGame.instance.hitmarker.Invoke();
            Debug.Log("Cuchillo desaparecido");
            Destroy(parentTrans.parent.gameObject);
        }
    }

}
