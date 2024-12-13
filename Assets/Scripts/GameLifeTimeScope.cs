using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifeTimeScope : LifetimeScope
{
    [SerializeField] private LevelList _levelList;
    [SerializeField] private CardSpawner _cardSpawner;
    [SerializeField] private RestartScreen _restartScreen;
    [SerializeField] private LevelWinSequence _levelWinSequence;
    [SerializeField] private CorrectAnswerDisplay _correctAnswerDisplay;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<CardGameInitiator>();

        builder.RegisterInstance(_levelList);
        builder.RegisterComponent(_cardSpawner);
        builder.RegisterComponent(_correctAnswerDisplay);
        builder.RegisterComponent(_restartScreen);
        builder.RegisterComponent(_levelWinSequence);
    }
}
