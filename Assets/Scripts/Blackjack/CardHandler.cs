using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHandler : MonoBehaviour
{
    [SerializeField] private DeckObject deck; // Reference to DeckObject.
    [SerializeField] private UIHandler uiHandler; // Reference to UIHandler.
    public List<CardObject> playerHand = new List<CardObject>();
    public List<CardObject> dealerHand = new List<CardObject>();
    public bool gameConcluded = false;

    // Sound effects
    [SerializeField] private AudioSource cardSound;

    private const int BLACKJACK = 21; // Maximum score in Blackjack.

    // Accessor methods for player and deal hands.
    public List<CardObject> GetPlayerHand() => playerHand;
    public List<CardObject> GetDealerHand() => dealerHand;

    public IEnumerator StartGame()
    {
        // Initialize the deck.
        deck.InitializeDeck();

        // Initially wait before dealing.
        yield return StartCoroutine(WaitSetTime(0.6f));

        // Deal two cards to the player with a delay.
        DealCard(playerHand);
        DisplayHands();
        yield return StartCoroutine(WaitSetTime(0.5f));

        DealCard(playerHand);
        DisplayHands();
        yield return StartCoroutine(WaitSetTime(0.5f));

        // Deal one card to the dealer with a delay.
        DealCard(dealerHand);
        DisplayHands();
        yield return StartCoroutine(WaitSetTime(0.5f));
    }

    public void ResetGame()
    {
        // Clear player and dealer hands.
        playerHand.Clear();
        dealerHand.Clear();

        // Reinitialize the deck to ensure it’s full.
        deck.InitializeDeck();

        // Update the UI to reflect the cleared hands.
        uiHandler.UpdateUI();
    }

    private void DealCard(List<CardObject> hand)
    {
        // Play the card sound.
        if (cardSound != null)
        {
            cardSound.Play();
        }
        else
        {
            Debug.LogWarning("Card sound is not assigned!");
        }

        // Get the top card from the deck.
        CardObject card = deck.DrawTopCard();
        if (card != null)
        {
            hand.Add(card);

            // Rerender cards after some player has drawn.
            uiHandler.UpdateUI();
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
            if (value == 11)
            {
                aceCount++;
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

    public void PlayerHit()
    {
        // Example of player choosing to "Hit."
        DealCard(playerHand);

        DisplayHands();

        // Check if the player busted.
        if (CalculateHandValue(playerHand) > BLACKJACK)
        {
            Debug.Log("Player Busted! Dealer Wins!");
            gameConcluded = true;
        }
        else
        {
            gameConcluded = false;
        }
    }

    public IEnumerator DealerTurn()
    {
        // Dealer's turn logic: must hit until reaching playerHand or higher.
        while (CalculateHandValue(dealerHand) < CalculateHandValue(playerHand))
        {
            DealCard(dealerHand);
            DisplayHands();
            yield return StartCoroutine(WaitSetTime(0.5f));
        }

        if (CalculateHandValue(dealerHand) == CalculateHandValue(playerHand) && CalculateHandValue(dealerHand) < 17)
        {
            DealCard(dealerHand);
            DisplayHands();
            yield return StartCoroutine(WaitSetTime(0.5f));
        }

        // Check if the dealer busted.
        if (CalculateHandValue(dealerHand) > BLACKJACK)
        {
            Debug.Log("Dealer Busted! Player Wins!");
            yield return StartCoroutine(WaitSetTime(0.5f));
            gameConcluded = true;
        }
        else
        {
            gameConcluded = false;
        }
    }

    private IEnumerator WaitSetTime(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public int DetermineWinner()
    {
        int playerScore = CalculateHandValue(playerHand);
        int dealerScore = CalculateHandValue(dealerHand);

        // Handle bust scenarios
        if (playerScore > 21)
            return -1; // Dealer wins if the player busts.
        if (dealerScore > 21)
            return 1; // Player wins if the dealer busts.

        // Compare scores
        if (playerScore > dealerScore)
            return 1; // Player wins with a higher score.
        if (dealerScore > playerScore)
            return -1; // Dealer wins with a higher score.

        return 0; // It's a tie.
    }
}
