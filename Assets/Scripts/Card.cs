using System;
using UnityEngine;

[Serializable]
public class Card
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _definition;
    [SerializeField] private bool _rotated;
    [SerializeField] private Color _backgroundColor;

    public bool Rotated => _rotated;

    public Sprite Sprite => _sprite;

    public string Definition => _definition;

    public Color BackgroundColor => _backgroundColor;
}