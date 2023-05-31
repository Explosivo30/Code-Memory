using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAlertState : EnemyState
{
    [SerializeField] EnemyCalmState enemyCalm;
    [SerializeField] EnemyAgressiveState enemyAgressive;
    [SerializeField] EnemyFOV enemyFOV;
    [SerializeField] float maxTimeAlertState = 5f;
    float counterLeftAlertState;
    bool deadBody = false;
    [SerializeField] Sprite visualizer;
    [SerializeField] Image image;

    public override EnemyState RunCurrentState()
    {
        if (enemyFOV.GetPlayerInside() == true)
        {
            counterLeftAlertState = maxTimeAlertState;
            return enemyAgressive;
        }
        else if(deadBody == true)
        {
            counterLeftAlertState = maxTimeAlertState;
            return this;
        }
        else
        {
            image.sprite = visualizer;
            counterLeftAlertState -= Time.deltaTime;
            if (counterLeftAlertState < 0)
            {
                return enemyCalm;
            }
            else
            {
                return this;
            }
            
            //¡¡¡Mucho animo teton!!! (Lo siento no puedo ayudar no hablo virgen)

        }

        
        
    }

}
