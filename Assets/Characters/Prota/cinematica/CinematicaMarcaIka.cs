using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicaMarcaIka : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void ActivarCinematicaAika()
    {
        animator.SetBool("activarAnimacioncinematica", true);
    }
    public void DesactivarCinematicaAika()
    {
        animator.SetBool("activarAnimacioncinematica", false);
    }

}
