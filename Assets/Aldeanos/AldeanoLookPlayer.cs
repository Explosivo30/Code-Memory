using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AldeanoLookPlayer : MonoBehaviour
{
    [SerializeField] AldeanoPlayerInside aldeanoPlayer;

    [SerializeField] Transform player;
    [SerializeField] Transform headAldeano;
    [SerializeField] Transform headAimTracker;

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

        if (aldeanoPlayer.GetIsInsidePlayer())
        {
            Debug.Log(player.position.z - headAldeano.position.z);
            if (player.position.z - headAldeano.position.z >= 0f)
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
        headAimTracker.position = headAimTracker.parent.position;
    }


}
