using UnityEngine;

[CreateAssetMenu(fileName = "New LevelConfiguration", menuName = "LevelConfiguration", order = 1)]
public class LevelConfiguration : ScriptableObject
{
    [SerializeField] private CardBundle _cardBundle;
    [SerializeField] private Difficulty _difficulty;

    public CardBundle CardBundle => _cardBundle;

    public Difficulty Difficulty => _difficulty;
}
