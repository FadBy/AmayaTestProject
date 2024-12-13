using TMPro;
using UnityEngine;

public class CorrectAnswerDisplay : MonoBehaviour
{
    [SerializeField] private string _prefix;
    [SerializeField] private TextMeshProUGUI _textDisplay;
    [SerializeField] private FadeAnimation _fadeAnimation;

    public void Display(string answer)
    {
        _textDisplay.text = _prefix + answer;
    }

    public void Appear()
    {
        _fadeAnimation.FadeIn();
    }

    public void Disappear()
    {
        _fadeAnimation.FadeOut();
    }
}
