using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public AudioClip clickSound;   // Importa el archivo de sonido que deseas reproducir en el Inspector
    public AudioSource audioSource;

    public void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();   // Crea una instancia de AudioSource
        audioSource.clip = clickSound;   // Asigna el archivo de sonido al componente AudioSource
    }

    public void OnPlay()
    {
        audioSource.Play();   
        SceneManager.LoadScene("Level1");
    }
    
    public void OnOpciones()
    {
        audioSource.Play();

        SceneManager.LoadScene("Opciones Main Menu");
    }
    
    public void OnExit()
    {
        audioSource.Play();

        Application.Quit();
    }

    public void OnReturnToMainMenu()
    {
        audioSource.Play();
        SceneManager.LoadScene("MainMenu");
    }

}
