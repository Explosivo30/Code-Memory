using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarMarcaPlayer : MonoBehaviour
{
    [SerializeField] GameObject Cinematica;
    [SerializeField] GameObject Marca;
    public GrapplingPower GrapplingPower;
    [SerializeField] bool marca = false;
    // Start is called before the first frame update
    void Start()
    {
        Marca.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Cinematica.activeInHierarchy || marca == true)
        {
            Invoke("ActivarMarca", 1f);
        }
    }
    public void ActivarMarca()
    {
        Marca.SetActive(true);

        GrapplingPower.gancho = true;
    }
}
