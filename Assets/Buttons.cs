using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void OnPlay()
    {
        SceneManager.LoadScene("Level1");
    }
    
    public void OnOpciones()
    {
        SceneManager.LoadScene("Opciones Main Menu");
    }
    
    public void OnExit()
    {
        Application.Quit();
    }

    public void OnReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
