using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AldeanoLookPlayer : MonoBehaviour
{
    [SerializeField] AldeanoPlayerInside aldeanoPlayer;

    [SerializeField] Transform player;
    [SerializeField] Transform headAldeano;
    [SerializeField] Transform headAimTracker;
    [SerializeField] Transform headAim;

    Vector3 posHeadAldeano;

    Vector3 dirToPlayer;

    private void Awake()
    {
        posHeadAldeano = headAimTracker.position;
    }

    private void FixedUpdate()
    {
        //if(player is inside)
        //dirToPlayer = (player.position - headAldeano.position).normalized;
        //Debug.DrawRay(transform.position, dirToPlayer);
        //Debug.Log(player.position - headAldeano.forward);
        
        if (aldeanoPlayer.GetIsInsidePlayer())
        {
            //Debug.Log(player.position.z - headAldeano.position.z);
            Vector3 positions = player.position - headAim.position;
            float direction = Vector3.Dot(positions, headAim.forward);
            Debug.Log(direction);
            if (direction > -0.5f)
            {
                TrackPlayer();
            }
            else
            {
                ResetTrackPlayer();
            }
        }
        else
        {
            ResetTrackPlayer();
        }
        
        
        
    }


    void TrackPlayer()
    {
        headAimTracker.position = player.position;
    }


    void ResetTrackPlayer()
    {
        headAimTracker.position = headAim.position;
    }


}
