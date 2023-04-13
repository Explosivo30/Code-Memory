using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDePausa : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] CameraMove camMove;

    private void Awake()
    {
        
    }

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

    public void SetHorizontalValue(float sensH)
    {
        camMove.sensitivityX = sensH;
    }

    public void SetVerticalValue(float sensV)
    {
        camMove.sensitivityY = sensV;
    }



}
