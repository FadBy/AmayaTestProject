using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CardBundle", menuName = "CardBundle", order = 1)]
public class CardBundle : ScriptableObject
{
    [SerializeField] private List<Card> _cards;

    public IEnumerable<Card> Cards => _cards;
}
