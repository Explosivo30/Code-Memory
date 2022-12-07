using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMenuPause : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    bool menuPauseIsActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (menuPauseIsActive == false && Input.GetKeyDown(KeyCode.Escape))
        {
            menuPauseIsActive = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        
        if(pauseMenu.activeInHierarchy == false)
        {
            Debug.Log("DEVOLVEMOS EL TIEMPO");
            pauseMenu.SetActive(false);
            menuPauseIsActive = false;
        }
    }
}
