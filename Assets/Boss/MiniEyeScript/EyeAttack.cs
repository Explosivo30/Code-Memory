using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeAttack : MonoBehaviour
{

    [SerializeField] Transform player;
    float timeToAttack;
    [SerializeField]
    [Range(1f,2f)]float minTimeAttack = 1f;
    [SerializeField]
    [Range(2.5f, 5f)] float maxTimeAttack = 3f;

    [SerializeField] float timeResetAttack = 3f;

    [SerializeField] GameObject bullet;
    [SerializeField] bool detenerMovimiento = false;

    // Start is called before the first frame update
    void Start()
    {
        DecideTimeAttack();
    }

    // Update is called once per frame
    void Update()
    {
        if (detenerMovimiento == false)
        {
            transform.LookAt(player);
            AttackPlayer();
        }      
    }


    private void AttackPlayer()
    {
        timeToAttack -= Time.deltaTime;
        //Spawn gameobject with constant force until we have particles;
        if (timeToAttack < 0)
        {
            Debug.Log("AtacaMiniojos");
            //playerAttack
            Instantiate(bullet, transform.position,transform.rotation);
            DecideTimeAttack();
            timeToAttack = timeResetAttack;
            
        }
    }

    public void DetenerMovimneto()
    {
        detenerMovimiento = true;
    }

    void DecideTimeAttack()
    {
        timeToAttack = Random.Range(minTimeAttack, maxTimeAttack);
    }

}
