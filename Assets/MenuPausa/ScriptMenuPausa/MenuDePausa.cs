using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDePausa : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    
    public void OnContinue()
    {
        HidePauseMenu();
        ReturnGameTime();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HidePauseMenu();
            ReturnGameTime();
        }
    }

    void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
    }

    void ReturnGameTime()
    {
        Time.timeScale = 1.0f;
    }


    

}
