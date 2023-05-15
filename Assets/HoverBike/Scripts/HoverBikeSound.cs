using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverBikeSound : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] AudioSource audioSource;

    [SerializeField] float minPitch = 0.1f;
    [SerializeField] float maxPitch = 2f;
    public float maxAngularVelocity = 100f;
    [SerializeField] float pitchAgumento;
    [SerializeField] float pitchBajada;

    [SerializeField] KeyCode keyForward = KeyCode.W;
    [SerializeField] KeyCode keyBack = KeyCode.S;
    [SerializeField] KeyCode keyRigth = KeyCode.D;
    [SerializeField] KeyCode keyLeft = KeyCode.A;

    [SerializeField] float bucle = 0f;

    // Update is called once per frame
    private void Start()
    {
        minPitch = 0.1f;
    }
    void Update()
    {
        if (Input.GetKey(keyForward) || Input.GetKey(keyBack) || Input.GetKey(keyRigth) || Input.GetKey(keyLeft))
        {
            if (rb.velocity.magnitude > 2f)
            {
                float t = Mathf.Abs(rb.velocity.magnitude) / maxAngularVelocity;
                t = Mathf.Clamp01(t);
                pitchAgumento = Mathf.Lerp(minPitch, maxPitch, t);
                if (pitchAgumento > 1.1f)
                {
                    pitchAgumento = 1.1f;
                }
                audioSource.pitch = pitchAgumento;
                bucle = 0f;
                pitchBajada = pitchAgumento;
            }
        }
        else
        {
            if (bucle < bucle + 1)
            {
                pitchBajada -= 0.01f;
                audioSource.pitch = pitchBajada;
                bucle += 1;
                if (pitchBajada <= minPitch)
                {
                    pitchBajada = minPitch;
                }
            }
            pitchAgumento = pitchBajada;
        }
    }
}
