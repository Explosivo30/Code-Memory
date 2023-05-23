using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDePausa : MonoBehaviour
{
    [SerializeField] ActivateMenuPause pauseMenu;
    [SerializeField] CameraMove camMove;

    public void OnContinue()
    {
        ReturnGameTime();
    }

    void ReturnGameTime()
    {
        pauseMenu.Resume();
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
