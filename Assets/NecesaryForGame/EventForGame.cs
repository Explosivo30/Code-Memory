using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EventForGame: MonoBehaviour
{
    public static EventForGame instance;

    private void OnEnable()
    {
        if(instance)
        {
            Destroy(this.transform.parent.gameObject);
        }else
        {
            instance = this;
        }
    }

    public UnityEvent hitmarker;

    public UnityEvent deadWhip = new UnityEvent();

    public UnityEvent activarManillar = new UnityEvent();

    public UnityEvent desactivarManillar = new UnityEvent();


    public UnityEvent bossHit = new UnityEvent();

    



}
