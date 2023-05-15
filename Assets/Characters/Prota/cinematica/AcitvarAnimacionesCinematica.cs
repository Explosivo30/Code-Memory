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

    public void ActivarCinematicaLanzador()
    {
        animator.SetBool("colacarLanzador", true);
    }
    public void DeactivarCinematicaLanzador()
    {
        animator.SetBool("colacarLanzador", false);
    }

    public void ActivarCinematicaAikaMano()
    {
        animator.SetBool("activarAikaMano", true);
        //Canviar el valor de false a true de la activacion de las particulas
    }
    public void DeactivarCinematicaAikaMano()
    {
        animator.SetBool("activarAikaMano", false);
    }

}
