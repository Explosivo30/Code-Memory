using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickMenuOptions : MonoBehaviour
{
    [SerializeField] SpriteRenderer opcionesGenerales;
    [SerializeField] SpriteRenderer controles;
    [SerializeField] SpriteRenderer sensibilidad;

    [SerializeField] GameObject generalBoton;
    [SerializeField] GameObject controlesBoton;
    [SerializeField] GameObject sensibilidadBoton;


    private void Awake()
    {
        OpcionesGenerales();   
    }

    public void OpcionesGenerales()
    {
        controles.enabled = false;
        sensibilidad.enabled= false;
        opcionesGenerales.enabled = true;

        
        controlesBoton.SetActive(false);
        sensibilidadBoton.SetActive(false);
        generalBoton.SetActive(true);

    }

    public void Sensibilidad()
    {
        opcionesGenerales.enabled = false;
        controles.enabled = false;
        sensibilidad.enabled = true;

        controlesBoton.SetActive(false);
        generalBoton.SetActive(false);
        sensibilidadBoton.SetActive(true);

    }

    public void Controles()
    {
        sensibilidad.enabled = false;
        opcionesGenerales.enabled = false;
        controles.enabled = true;

        

        generalBoton.SetActive(false);
        sensibilidadBoton.SetActive(false);
        controlesBoton.SetActive(true);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenuFinal");
    }

}
