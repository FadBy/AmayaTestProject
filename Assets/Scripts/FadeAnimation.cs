using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeAnimation : MonoBehaviour
{
    [SerializeField] private float _inOutDuration = 1f;
    [SerializeField] private CanvasGroup _canvasGroup;
    
    private Tween _fadeTween;
    
    public void FadeIn(Action onComplete = null)
    {
        Fade(0, 1, onComplete);
    }
    
    public void FadeOut(Action onComplete = null)
    {
        Fade(1, 0,  onComplete);
    }

    private void Fade(float startValue, float endValue, Action onComplete = null)
    {
        _fadeTween.Kill();

        _canvasGroup.alpha = startValue;
        _canvasGroup.DOFade(endValue, _inOutDuration).OnComplete(() => onComplete?.Invoke());
    }
    
}
