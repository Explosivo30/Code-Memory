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
        
    }

    private void Start()
    {
        parentTrans = GetComponentInParent<Transform>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.transform.tag == "Cuchillo")
        {
            EventForGame.instance.hitmarker.Invoke();
            Destroy(parentTrans.parent.gameObject);
        }
    }

}
