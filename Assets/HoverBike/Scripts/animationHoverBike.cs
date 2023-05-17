using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationHoverBike : MonoBehaviour
{
    public Animator animator;
    public hoverBikeController hoverbike;
    
    // Start is called before the first frame update
    void Start()
    {
        hoverbike.GetComponent<hoverBikeController>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (hoverbike.activarAnimacionsubidaYBajada == true)
        {
            animator.SetBool("Conducir", true);
        }
        if (hoverbike.activarAnimacionsubidaYBajada == false)
        {
            animator.SetBool("Conducir", false);
        }
        if (hoverbike.activarAnimacionturnLeft == true)
        {
            animator.SetBool("TurnLeft", true);
        }
        if (hoverbike.activarAnimacionturnRigth == true)
        {
            animator.SetBool("TurnRigth", true);
        }
        if (hoverbike.activarAnimacionturnRigth == false && hoverbike.activarAnimacionturnLeft == false)
        {
            animator.SetBool("TurnRigth", false);
            animator.SetBool("TurnLeft", false);
        }
    }
}
