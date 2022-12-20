using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventForGame: MonoBehaviour
{
    public static EventForGame eventForGame;

    private void Awake()
    {
        if(eventForGame == null)
        {
            eventForGame = this;
        }else
        {
            Destroy(this);
        }
    }

    public UnityEvent hitmarker;
}
