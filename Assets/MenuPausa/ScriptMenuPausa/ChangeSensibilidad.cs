using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSensibilidad : MonoBehaviour
{
    [SerializeField] CameraMove camMove;
    [SerializeField] Slider vertical;
    [SerializeField] Slider horizontal;

    public void SetHorizontalValue()
    {
        camMove.sensitivityX = horizontal.value;
    }

    public void SetVerticalValue()
    {
        camMove.sensitivityY = vertical.value;
    }

}
