using System.Collections.Generic;

public class LevelData
{
    private LevelConfiguration _level;
    private bool _isFirstLevel;
    private bool _isLastLevel;
    private List<Card> _usedCards;

    public LevelData(LevelConfiguration level, bool isFirstLevel, bool isLastLevel, List<Card> usedCards)
    {
        _level = level;
        _isFirstLevel = isFirstLevel;
        _isLastLevel = isLastLevel;
        _usedCards = usedCards;
    }

    public bool IsLastLevel => _isLastLevel;

    public LevelConfiguration Level => _level;

    public bool IsFirstLevel => _isFirstLevel;

    public List<Card> UsedCards => _usedCards;
}