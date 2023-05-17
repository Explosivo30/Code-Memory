using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMenuPause : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    bool menuPauseIsActive = false;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            menuPauseIsActive = true;
            pauseMenu.SetActive(true);
            
        }
        
        /*
        if(pauseMenu.activeInHierarchy == false)
        {
            //Debug.Log("DEVOLVEMOS EL TIEMPO");
            pauseMenu.SetActive(false);
            menuPauseIsActive = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        */
    }
}
