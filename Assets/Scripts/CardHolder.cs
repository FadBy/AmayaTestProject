using System;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SpriteRenderer _background;
    [SerializeField] private float _brightValue;
    [SerializeField] private CardHolderAnimation _animation;

    private bool _interactable;
    
    private Card _card;

    public Card Card => _card;

    public void DisplayCard(Card card)
    {
        _card = card;
        _spriteRenderer.sprite = _card.Sprite;
        _background.color = card.BackgroundColor * _brightValue;
        if (card.Rotated)
        {
            _spriteRenderer.transform.Rotate(0f, 0f, -90f);
        }
    }

    public event Action<CardHolder> CardSelected;

    private void OnMouseDown()
    {
        if (!_interactable) return;
        CardSelected?.Invoke(this);
    }

    public void Shake()
    {
        _animation.Shake();
    }

    public void Disappear()
    {
        Destroy(gameObject);
    }

    public void Appear(bool immediate = true)
    {
        _interactable = false;
        _animation.BounceIn(() => _interactable = true, immediate);
    }

    public void Disable()
    {
        _interactable = false;
    }
}
