public class AnswerChecker
{
    private Card _correctCard;

    public Card CorrectCard
    {
        get => _correctCard;
        set => _correctCard = value;
    }
    
    public bool CheckIfCorrect(Card cardChosen)
    {
        return cardChosen.Definition == CorrectCard.Definition;
    }
}
