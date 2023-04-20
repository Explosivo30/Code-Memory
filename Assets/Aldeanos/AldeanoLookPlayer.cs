using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AldeanoLookPlayer : MonoBehaviour
{
    [SerializeField] AldeanoPlayerInside aldeanoPlayer;

    [SerializeField] Transform player;
    [SerializeField] Transform headAldeano;
    [SerializeField] Transform headAimTracker;

    Vector3 dirToPlayer;

    private void FixedUpdate()
    {
        //if(player is inside)
        //dirToPlayer = (player.position - headAldeano.position).normalized;
        //Debug.DrawRay(transform.position, dirToPlayer);
        headAimTracker.position = player.position;
    }

}
