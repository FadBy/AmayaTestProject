using System.Collections.Generic;
using VContainer;
using VContainer.Unity;

public class CardGameInitiator : IStartable
{
    private CardSpawner _cardSpawner;
    private RestartScreen _restartScreen;
    private CorrectAnswerDisplay _correctAnswerDisplay;

    private LevelList _levelList;

    private LifetimeScope _currentLevelScope;
    private LifetimeScope _currentGameScope;

    private List<Card> _usedCards = new List<Card>();
    
    public LevelList LevelList => _levelList;

    private int _currentLevelIndex = 0;

    public int CurrentLevelIndex => _currentLevelIndex;

    public LevelConfiguration CurrentLevel => LevelList.Levels[CurrentLevelIndex];


    public CardGameInitiator(LifetimeScope currentGameScope, CorrectAnswerDisplay correctAnswerDisplay, LevelList levelList, CardSpawner cardSpawner, RestartScreen restartScreen)
    {
        _cardSpawner = cardSpawner;
        _currentGameScope = currentGameScope;
        _correctAnswerDisplay = correctAnswerDisplay;
        _levelList = levelList;
        _currentGameScope = currentGameScope;
        _restartScreen = restartScreen;
    }

    private void OnLevelComplete()
    {

        if (IsLastLevel())
        {
            _restartScreen.Show();
            _restartScreen.FadeIn();
        }
        else
        {
            
            StartNextLevel();
        }
    }

    private void StartNextLevel()
    {
        _currentLevelIndex++;
        StartCurrentLevel();
    }

    public void StartCurrentLevel()
    {
        _currentLevelScope?.Dispose();

        _currentLevelScope = _currentGameScope.CreateChild(builder =>
        {
            builder.Register<LevelInitiator>(Lifetime.Scoped);
            
            builder.Register<AnswerChecker>(Lifetime.Scoped);
            builder.RegisterComponent(_cardSpawner);
            builder.RegisterComponent(_correctAnswerDisplay);
            builder.RegisterInstance(new LevelData(CurrentLevel, CurrentLevelIndex == 0, CurrentLevelIndex == LevelList.Levels.Count - 1, _usedCards));
            
            builder.RegisterBuildCallback(container =>
            {
                var levelInitiator = container.Resolve<LevelInitiator>();
                
                levelInitiator.StartLevel();
                levelInitiator.LevelCompleted += OnLevelComplete;
            });
            
            builder.RegisterDisposeCallback(container =>
            {
                var levelInitiator = container.Resolve<LevelInitiator>();
                if (_usedCards.Contains(levelInitiator.CorrectCard))
                {
                    _usedCards.Clear();
                }
                _usedCards.Add(levelInitiator.CorrectCard);
                var spawner = container.Resolve<CardSpawner>();
                spawner.DisappearAllCards();
            });
        });
    }

    private bool IsLastLevel()
    {
        return CurrentLevelIndex == LevelList.Levels.Count - 1;
    }

    public void Start()
    {
        _restartScreen.SubscribeToRestartButton(StartFirstLevel);
        _correctAnswerDisplay.Appear();
        StartCurrentLevel();
    }

    public void StartFirstLevel()
    {
        _restartScreen.FadeOut(() =>
        {
            _currentLevelIndex = 0;
            StartCurrentLevel();
            _correctAnswerDisplay.Appear();

        });
        
    }
}
