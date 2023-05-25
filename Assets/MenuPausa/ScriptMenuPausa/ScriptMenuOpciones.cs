using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScriptMenuOpciones : MonoBehaviour
{
    Tween fadeTween;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DeSelected()
    {
        Debug.Log("Deseleccionado");
        FadeOut(0.5f);
    }


    public void OnSelected()
    {
        Debug.Log("Seleccionado");
        FadeIn(0.5f);
    }


    void FadeIn(float duration)
    {
        Fade(255f, duration, () =>
        {
            //canvasGroup.interactable = true;
            //canvasGroup.blocksRaycasts = true;
        });
    }


    void FadeOut(float duration)
    {
        Fade(0f, duration, () =>
        {
            //canvasGroup.interactable = true;
            //canvasGroup.blocksRaycasts = false;
        });
    }


    void Fade(float endValue, float duration, TweenCallback onEnd)
    {
        if (fadeTween != null)
        {
            fadeTween.Kill(false);
        }

        fadeTween = spriteRenderer.material.DOBlendableColor(new Color(2,4,5,0), duration);
        fadeTween.onComplete += onEnd;

    }
}
