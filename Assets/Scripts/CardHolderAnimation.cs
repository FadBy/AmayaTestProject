using System;
using DG.Tweening;
using UnityEngine;

public class CardHolderAnimation : MonoBehaviour
{
    [SerializeField] private float _inOutDuration = 1f;
    [SerializeField] private float _shakeDuration = 1f;
    [SerializeField] private Transform _objectInsideCard;
    
    private Tween _bounceTween;
    private Tween _shakeTween;

    public void BounceIn(Action onComplete = null, bool immediate = false)
    {
        float duration = immediate ? 0f : _inOutDuration;
        Bounce(0, 1, duration, Ease.OutElastic, onComplete);
    }

    private void Bounce(float startValue, float endValue, float duration, Ease ease, Action onComplete = null)
    {
        _bounceTween.Kill();
        
        transform.localScale = Vector3.one * startValue;
        _bounceTween = transform.DOScale(endValue, duration).SetEase(ease).OnComplete(() => onComplete?.Invoke());
    }
    
    public void Shake()
    {
        _shakeTween.Kill();
        _objectInsideCard.localPosition = Vector3.zero;
        _shakeTween = _objectInsideCard.DOLocalMoveX(_objectInsideCard.localPosition.x + 0.15f, _shakeDuration / 2)
            .SetEase(Ease.InBounce)
            .SetLoops(2, LoopType.Yoyo);  ;
        // var sequence = DOTween.Sequence();
        // sequence.Append(_objectInsideCard.DOLocalMoveX(_objectInsideCard.localPosition.x + 0.15f, _shakeDuration / 2).SetEase(Ease.InBounce));
        // sequence.Append(_objectInsideCard.DOLocalMoveX(_objectInsideCard.localPosition.x - 0.15f, _shakeDuration / 2).SetEase(Ease.InBounce));
    }
}
