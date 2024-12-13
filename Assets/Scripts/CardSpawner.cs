using System;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private int _gridWidth = 3;
    [SerializeField] private Vector2 _cardDistance = new Vector2(5f, 10f);
    [SerializeField] private Transform _cardContainer;
    [SerializeField] private CardHolder _cardHolderPrefab;
    
    private List<CardHolder> _cards = new List<CardHolder>();

    public List<CardHolder> SpawnCards(List<Card> cards, bool animateCardsIn)
    {
        int cardCount = cards.Count;
        if (cardCount <= 0)
        {
            Debug.LogWarning($"Card count must be positive; cardCount = {cardCount}");
            cardCount = 0;
        }

        if (cardCount % _gridWidth != 0)
        {
            int newCardCount = cardCount - cardCount % _gridWidth;
            Debug.LogWarning($"Card count must be multiple of {_gridWidth}; cardCount = {cardCount}, newCardCount = {newCardCount}");
            cardCount = newCardCount;
        }

        ClearCardList();

        var cardHolders = new List<CardHolder>();
        Vector2Int fieldSize = new Vector2Int(_gridWidth, cardCount / _gridWidth);
        var containerSize = new Vector2((fieldSize.x - 1) * _cardDistance.x, (fieldSize.y - 1) * _cardDistance.y);
        var pivot = (Vector2)_cardContainer.position - containerSize / 2f;
        for (var y = 0; y < fieldSize.y; y++)
        {
            for (var x = 0; x < fieldSize.x; x++)
            {
                var cardPosition = new Vector2(pivot.x + x * _cardDistance.x, pivot.y + y * _cardDistance.y);
                cardHolders.Add(SpawnCard(cardPosition, cards[x + y * fieldSize.x], animateCardsIn));
            }
        }
        return cardHolders;
    }

    public void DisappearAllCards()
    {
        foreach (var cardHolder in _cards)
        {
            cardHolder.Disappear();
        }
    }

    private void ClearCardList()
    {
        _cards.Clear();
    }

    public void DisableCards()
    {
        foreach (var card in _cards)
        {
            card.Disable();
        }
    }

    private CardHolder SpawnCard(Vector2 position, Card card, bool animateIn)
    {
        var cardHolder = Instantiate(_cardHolderPrefab, position, Quaternion.identity, _cardContainer);
        cardHolder.DisplayCard(card);
        cardHolder.Appear(immediate: !animateIn);
        _cards.Add(cardHolder);
        return cardHolder;
    }
}
