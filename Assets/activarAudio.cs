using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activarAudio : MonoBehaviour
{
    [SerializeField] AudioSource sound;
    void Awake()
    {
        sound.Play();
    }
}
