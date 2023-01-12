using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class LogoEasing : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    Tween fadeTween;

    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        DOTween.SetTweensCapacity(500, 500);

        FadeIn(5f);
        
    }

    // Update is called once per frame
    void Update()
    {

        //logo.DOFade();


    }

   

    void FadeIn(float duration)
    {
        Fade(1f, duration, () =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        });
    }

    void FadeOut(float duration)
    {
        Fade(0f, duration, () =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = false;
        });
    }
    void Fade(float endValue, float duration, TweenCallback onEnd)
    {
        if(fadeTween != null)
        {
            fadeTween.Kill(false);
        }

        fadeTween = canvasGroup.DOFade(endValue, duration);
        fadeTween.onComplete += onEnd;

    }
}
