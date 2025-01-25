using UnityEngine;

public class DeckObject : MonoBehaviour
{
    // Declare deck length constant.
    private static int deckLength = 52;
    private int cardsDrawnFromDeck = 0;

    // Array to store all cards in the deck.
    private CardObject[] deck = new CardObject[deckLength];

    public void InitializeDeck()
    {
        RandomizeDeck();
    }

    private void RandomizeDeck()
    {
        // Create an array of card IDs (0-51).
        int[] cardIDs = new int[deckLength];
        for (int i = 0; i < deckLength; i++)
        {
            cardIDs[i] = i;
        }

        // Shuffle the card IDs using the Fisher-Yates algorithm.
        for (int i = cardIDs.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = cardIDs[i];
            cardIDs[i] = cardIDs[randomIndex];
            cardIDs[randomIndex] = temp;
        }

        // Create a CardObject for each shuffled ID and store it in the deck.
        for (int i = 0; i < deck.Length; i++)
        {
            deck[i] = new CardObject(cardIDs[i]);
        }
    }

    public CardObject DrawTopCard()
    {
        if (cardsDrawnFromDeck >= deckLength)
        {
            Debug.LogError("No more cards to draw!");
            return null;
        }

        // Get the next card from the top of the deck.
        CardObject topCard = deck[(deckLength - 1) - cardsDrawnFromDeck];
        cardsDrawnFromDeck++;
        return topCard;
    }

    public CardObject[] GetDeck()
    {
        return deck;
    }
}
