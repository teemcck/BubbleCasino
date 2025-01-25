using System.Collections.Generic;
using UnityEngine;

public class CardHandler : MonoBehaviour
{
    [SerializeField] private DeckObject deck; // Reference to DeckObject.
    private List<CardObject> playerHand = new List<CardObject>();
    private List<CardObject> dealerHand = new List<CardObject>();

    private const int BLACKJACK = 21; // Maximum score in Blackjack.

    // Accessor methods for player and deal hands.
    public List<CardObject> GetPlayerHand() => playerHand;
    public List<CardObject> GetDealerHand() => dealerHand;

    public void StartGame()
    {
        // Initialize the deck.
        deck.InitializeDeck();

        // Deal two cards to the player.
        DealCard(playerHand);
        DealCard(playerHand);

        // Deal to cards to the dealer.
        DealCard(dealerHand);
        DealCard(dealerHand);

        DisplayHands();
    }

    private void DealCard(List<CardObject> hand)
    {
        // Get the top card from the deck.
        CardObject card = deck.DrawTopCard();
        if (card != null)
        {
            hand.Add(card);
            Debug.Log($"Dealt {card.ToString()}");
        }
        else
        {
            Debug.LogWarning("No more cards in the deck!");
        }
    }

    private int CalculateHandValue(List<CardObject> hand)
    {
        int totalValue = 0;
        int aceCount = 0;

        foreach (CardObject card in hand)
        {
            int value = card.CardValue;

            // Handle Ace: count as 11 initially, but reduce to 1 if the hand is over 21.
            if (value == 1)
            {
                aceCount++;
                value = 11; // Default value for Ace.
            }

            totalValue += value;
        }

        // Adjust for Aces if totalValue exceeds BLACKJACK.
        while (totalValue > BLACKJACK && aceCount > 0)
        {
            totalValue -= 10; // Reduce an Ace from 11 to 1.
            aceCount--;
        }

        return totalValue;
    }

    private void DisplayHands()
    {
        Debug.Log($"Player's Total: {CalculateHandValue(playerHand)}");
        Debug.Log($"Dealer's Total: {CalculateHandValue(dealerHand)}");
    }

    public void PlayerTurn(out bool gameConcluded)
    {
        // Example of player choosing to "Hit."
        DealCard(playerHand);

        // Check if the player busted.
        if (CalculateHandValue(playerHand) > BLACKJACK)
        {
            Debug.Log("Player Busted! Dealer Wins!");
            gameConcluded = true;
        }
        gameConcluded = false;
    }

    public void DealerTurn(out bool gameConcluded)
    {
        // Dealer's turn logic: must hit until reaching playerHand or higher.
        while (CalculateHandValue(dealerHand) < CalculateHandValue(playerHand))
        {
            DealCard(dealerHand);
        }

        // Check if the dealer busted.
        if (CalculateHandValue(dealerHand) > BLACKJACK)
        {
            Debug.Log("Dealer Busted! Player Wins!");
            gameConcluded = true;
        }
        gameConcluded = false;
    }

    private void DetermineWinner()
    {
        int playerScore = CalculateHandValue(playerHand);
        int dealerScore = CalculateHandValue(dealerHand);

        if (playerScore > dealerScore)
        {
            Debug.Log("Player Wins!");
        }
        else if (playerScore < dealerScore)
        {
            Debug.Log("Dealer Wins!");
        }
        else
        {
            Debug.Log("It's a Tie!");
        }
    }
}
