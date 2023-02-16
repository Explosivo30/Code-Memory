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


    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKey(KeyCode.H))
            {
                SceneManager.LoadScene("Level2");
            }
        }
    }
}
