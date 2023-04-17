using UnityEngine;

public class PlayAudioPueblo : MonoBehaviour
{
    AudioSource aldeaAudio;


    private void Awake()
    {
       aldeaAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            aldeaAudio.Play();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            aldeaAudio.Stop();
        }
    }

}
