using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer.Unity;
using Random = UnityEngine.Random;

public class LevelInitiator
{
    private AnswerChecker _answerChecker;
    private CardSpawner _cardSpawner;
    private CorrectAnswerDisplay _correctAnswerDisplay;
    private LevelWinSequence _levelWinSequence;

    private LevelData _currentLevelData;

    private List<Card> _gameCardList;

    public Card CorrectCard => _answerChecker.CorrectCard;

    public LevelConfiguration CurrentLevel => _currentLevelData.Level;

    public event Action LevelCompleted;

    public LevelInitiator(AnswerChecker answerChecker, CardSpawner cardSpawner, CorrectAnswerDisplay correctAnswerDisplay, LevelData levelData, LevelWinSequence levelWinSequence)
    {
        _answerChecker = answerChecker;
        _cardSpawner = cardSpawner;
        _correctAnswerDisplay = correctAnswerDisplay;
        _currentLevelData = levelData;
        _levelWinSequence = levelWinSequence;
    }

    public void StartLevel()
    {
        _answerChecker.CorrectCard = GetNextCard(CurrentLevel.CardBundle.Cards.ToList());
        
        _gameCardList = GenerateGameCards();

        _correctAnswerDisplay.Display(_answerChecker.CorrectCard.Definition);

        var cardHolders = _cardSpawner.SpawnCards(_gameCardList, animateCardsIn: _currentLevelData.IsFirstLevel);
        foreach (var cardHolder in cardHolders)
        {
            cardHolder.CardSelected += OnCardSelect;
        }
    }

    private List<Card> GenerateGameCards()
    {
        var listWithoutCorrect = CurrentLevel.CardBundle.Cards.ToList();
        listWithoutCorrect.Remove(CorrectCard);

        var shuffledList = GenerateShuffledList(listWithoutCorrect);
        ClampList(shuffledList, CurrentLevel.Difficulty.CardCount - 1);
        shuffledList.Add(CorrectCard);
        
        
        return GenerateShuffledList(shuffledList);
    }

    private void OnCardSelect(CardHolder cardHolder)
    {
        bool isAnswerCorrect = _answerChecker.CheckIfCorrect(cardHolder.Card);
        
        if (isAnswerCorrect)
        {
            _levelWinSequence.StartWinSequence(cardHolder, _currentLevelData.IsLastLevel, () => LevelCompleted?.Invoke());
            _cardSpawner.DisableCards();
        }
        else
        {
            cardHolder.Shake();
        }
    }

    private Card GetNextCard(List<Card> gameCards)
    {
        gameCards = GenerateShuffledList(gameCards);
        int nextCardIndex = 0;
        while (_currentLevelData.UsedCards.Contains(gameCards[nextCardIndex]) && nextCardIndex != gameCards.Count - 1)
        {
            nextCardIndex++;
        }

        if (nextCardIndex == gameCards.Count - 1) 
            nextCardIndex = GetRandomCardIndex(gameCards.Count);
        return gameCards[nextCardIndex];
    }

    private int GetRandomCardIndex(int max)
    {
        return Random.Range(0, max);
    }

    private static List<Card> GenerateShuffledList(IEnumerable<Card> list)
    {
        var newList = new List<Card>(list);
        int n = newList.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            (newList[k], newList[n]) = (newList[n], newList[k]);
        }

        return newList;
    }

    private static void ClampList(List<Card> list, int maxLength)
    {
        list.RemoveRange(maxLength, list.Count - maxLength);
    }
}
