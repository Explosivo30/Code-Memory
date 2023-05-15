using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarBrazaletePlayer : MonoBehaviour
{
    [SerializeField] GameObject Cinematica;
    [SerializeField] GameObject Brazalete;
    public AttackPlayer attackPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Brazalete.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Cinematica.activeInHierarchy)
        {
            Invoke("ActivarLanzador", 1f);
        }
    }
    void ActivarLanzador()
    {
        Brazalete.SetActive(true);

        attackPlayer.lanzadorActivado = true;
    }
}
