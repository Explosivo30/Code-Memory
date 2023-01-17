using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LogoEasing : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    Tween fadeTween;

    
    private void Awake()
    {
       
    }
    void Start()
    {
        
        DOTween.Init();
        DOTween.SetTweensCapacity(500, 500);

        FadeIn(5f);
        
    }

    private void Update()
    {
        fadeTween.OnComplete(OnEnter);
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

    void OnEnter()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
