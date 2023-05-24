using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;


public class boostspeed : MonoBehaviour
{
    [SerializeField] Rigidbody Rb;
    [SerializeField] GameObject particulas;
    public float addSpeed = 10000f;
    public float LowSpeed = 5000f;
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField] float fovToChange = 90;
    [SerializeField] float startFov = 60;
    public float currentFov;
    [SerializeField] AudioSource TurboSound;


    private void Awake()
    {
        currentFov = startFov;
        DOTween.Init();

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Boost"))
        {
            Rb.AddForce(transform.forward * addSpeed);
            ChangeFovValues(fovToChange);
            TurboSound.Play();
            particulas.SetActive(true);
            
        }
        if (other.CompareTag("Boost2"))
        {
            Rb.AddForce(transform.forward * LowSpeed);
            ChangeFovValues(fovToChange);
            TurboSound.Play();
            particulas.SetActive(true);
            
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Boost"))
        {
            Rb.AddForce(Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized * addSpeed);
            Invoke("TimeToWaitFov", 1.5f);

        }
        if (other.CompareTag("Boost2"))
        {
            Rb.AddForce(Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized * addSpeed);
            Invoke("TimeToWaitFov", 1.5f);
        }
    }
    void TimeToWaitFov()
    {
        ChangeFovValues(startFov);

        particulas.SetActive(false);
    }

    void ChangeFovValues(float desiredFov)
    {
        DOTween.To(
            () => cam.m_Lens.FieldOfView,
            (x) => cam.m_Lens.FieldOfView = x,
            desiredFov,
            1f);
    }

}
