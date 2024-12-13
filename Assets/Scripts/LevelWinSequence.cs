using System;
using System.Collections;
using UnityEngine;
using VContainer;

public class LevelWinSequence : MonoBehaviour
{
    [SerializeField] private ParticleSystem _winParticleSystem;
    [SerializeField] private RestartScreen _restartScreen;
    [SerializeField] private int _winDuration;
    [SerializeField] private int _cardDisappearDuration;
    
    [Inject]
    private CardSpawner CardSpawner { get; set; }
    
    public void StartWinSequence(CardHolder correctCardHolder, bool isLastLevel, Action onComplete)
    {
        StartCoroutine(WinSequenceCoroutine(correctCardHolder, isLastLevel, onComplete));
    }

    private IEnumerator WinSequenceCoroutine(CardHolder correctCardHolder, bool isLastLevel, Action onComplete)
    {
        _winParticleSystem.transform.position = correctCardHolder.transform.position;
        _winParticleSystem.Play();
        yield return new WaitForSeconds(_winDuration);
        onComplete?.Invoke();
    }
}
