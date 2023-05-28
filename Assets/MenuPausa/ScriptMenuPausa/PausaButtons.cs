using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausaButtons : MonoBehaviour
{
    [SerializeField] GameObject controles;
    [SerializeField] GameObject sensibilidad;
    [SerializeField] GameObject menu;


    public void OnControles()
    {
        menu.SetActive(false);
        sensibilidad.SetActive(false);
        controles.SetActive(true);

    }

    public void OnSensibilidad()
    {
        menu.SetActive(false);
        
        controles.SetActive(false);

        sensibilidad.SetActive(true);
    }

    public void OnAtras()
    {
        controles.SetActive(false);
        sensibilidad.SetActive(false);
        menu.SetActive(true);
    }
}
