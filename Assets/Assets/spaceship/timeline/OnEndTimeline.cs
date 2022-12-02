using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class OnEndTimeline : MonoBehaviour
{
    [SerializeField] GameObject Camera;
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PlayableDirector>().state != PlayState.Playing)
        {
            Camera.SetActive(false);
        }
    }
}
