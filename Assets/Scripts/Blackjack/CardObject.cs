[System.Serializable]
public class CardObject
{
    public int CardID { get; private set; }
    public int CardValue { get; private set; }

    public CardObject(int cardID)
    {
        CardID = cardID;
        CardValue = (cardID % 13) + 1; // Assign values from 1 to 13 (Ace through King).
    }
}
