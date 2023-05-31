using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
   
    public void OnPlay()
    {
        SceneManager.LoadScene("Level1");
    }



    public void OnOpciones()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void OnQuit()
    {
        Application.Quit();
    }


}
