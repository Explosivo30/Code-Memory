using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcitvarAnimacionesCinematica : MonoBehaviour
{
    [SerializeField] Animator animator; 

    public void ActivarCinematica()
    {
        animator.SetBool("activarCinematica", true);
    }
    public void DeactivarCinematica()
    {
        animator.SetBool("activarCinematica", false);
    }

}
