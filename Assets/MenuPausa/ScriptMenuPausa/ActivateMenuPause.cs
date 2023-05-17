using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMenuPause : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    bool menuPauseIsActive = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            if(menuPauseIsActive)
            {
                Resume();
            }
            else
            {
                Pausa();
            }
    }


    public void Pausa()
    {
        
        pauseMenu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0.001f;
        menuPauseIsActive = true;
    }


    public void Resume()
    {
        
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        menuPauseIsActive = false;
    }

}
