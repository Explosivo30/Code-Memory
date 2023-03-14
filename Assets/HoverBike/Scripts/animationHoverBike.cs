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
        if (hoverbike.activarAnimacion == true)
        {
            animator.SetBool("Conducir", true);
        }
        if (hoverbike.activarAnimacion == false)
        {
            animator.SetBool("Conducir", false);
        }
    }
}
