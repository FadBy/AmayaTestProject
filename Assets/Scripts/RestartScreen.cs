using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RestartScreen : MonoBehaviour
{
    [SerializeField] private Transform _restartScreenLayout;
    [SerializeField] private Button _restartButton;
    [SerializeField] private FadeAnimation _fadeAnimation;

    public void Show()
    {
        _restartScreenLayout.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _restartScreenLayout.gameObject.SetActive(false);
    }

    public void FadeIn()
    {
        _fadeAnimation.FadeIn();
    }

    public void FadeOut(Action onComplete = null)
    {
        _fadeAnimation.FadeOut(() =>
        {
            onComplete?.Invoke();
            Hide();
        });
    }

    public void SubscribeToRestartButton(UnityAction onClick)
    {
        _restartButton.onClick.AddListener(onClick);
    }
}
