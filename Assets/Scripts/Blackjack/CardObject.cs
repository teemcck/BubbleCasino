using UnityEngine;

[System.Serializable]
public class CardObject
{
    [SerializeField] private int cardID;  // ID of the card (0-51)
    [SerializeField] private int cardValue; // The calculated value of the card (2-11)

    public int CardID => cardID;  // Public getter for CardID
    public int CardValue => cardValue;  // Public getter for CardValue

    // Constructor to set the card ID and compute the card value.
    public CardObject(int cardID)
    {
        this.cardID = cardID;

        // Calculate the card value.
        int cardNumber = cardID % 13;  // Cards in each suit range from 0 to 12 (2, 3, ..., 10, Jack, Queen, King, Ace)

        // For card values 0-8, set the value to 2-10 (cardNumber + 2)
        if (cardNumber >= 0 && cardNumber <= 8)
        {
            cardValue = cardNumber + 2;
        }
        // For face cards (Jack, Queen, King), set the value to 10.
        else if (cardNumber >= 9 && cardNumber <= 11)
        {
            cardValue = 10;
        }
        // For Ace, set the value to 11.
        else if (cardNumber == 12)
        {
            cardValue = 11;
        }
    }
}
