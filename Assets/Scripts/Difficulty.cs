using UnityEngine;

[CreateAssetMenu(fileName = "New Difficulty", menuName = "Difficulty", order = 1)]
public class Difficulty : ScriptableObject
{
    [SerializeField] private int _cardCount;

    public int CardCount => _cardCount;
}
