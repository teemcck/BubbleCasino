using UnityEngine;
using System.Collections;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private CardHandler cardHandler; // Reference to CardHandler.
    [SerializeField] private UIHandler uiHandler; // Reference to UIHandler.
    [SerializeField] private BlackjackEvents eventHandler; // Reference to BlackjackEvents
    [SerializeField] private int playerCash = 100; // Define starting cash.
    [SerializeField] private int dailyThreshold = 200; // Initial threshold.
    [SerializeField] private int thresholdIncrement = 50; // Threshold increment per day.

    private bool playerWantsToHit = false;
    private int currentBet = 0; // Player's bet for the current round.

    public bool PlayerWantsToHit => playerWantsToHit;

    private IEnumerator Start()
    {
        // Run the first coroutine and wait until it completes
        yield return StartCoroutine(eventHandler.EventStart());

        // Once the first coroutine finishes, start the second coroutine
        yield return StartCoroutine(PlayBlackjack());
    }

    private IEnumerator PlayBlackjack()
    {
        int day = 1;

        while (playerCash > 0 && playerCash < dailyThreshold)
        {
            // Show the current day's status.
            uiHandler.ShowDayStatus(day, playerCash, dailyThreshold);

            // Let the player place a bet.
            yield return StartCoroutine(uiHandler.GetPlayerBet());
            currentBet = uiHandler.PlayerBet;

            // Ensure the bet is valid.
            if (currentBet <= 0 || currentBet > playerCash)
            {
                uiHandler.ShowInvalidBetMessage();
                continue;
            }

            // Deduct the bet amount from player's cash temporarily.
            playerCash -= currentBet;

            // Start the game round.
            yield return StartCoroutine(PlayRound());

            // Determine the outcome of the round.
            int result = cardHandler.DetermineWinner(); // Returns 1 for win, -1 for loss, 0 for tie.
            if (result == 1)
            {
                playerCash += currentBet * 2; // Player wins double their bet.
                uiHandler.ShowRoundResult("You won the round!", currentBet * 2);
            }
            else if (result == -1)
            {
                uiHandler.ShowRoundResult("You lost the round!", 0);
            }
            else
            {
                playerCash += currentBet; // Refund the bet in case of a tie.
                uiHandler.ShowRoundResult("It's a tie!", currentBet);
            }

            // Check if the player wants to continue.
            yield return StartCoroutine(uiHandler.AskPlayerToContinue());
            if (!uiHandler.PlayerWantsToContinue)
            {
                break;
            }

            // Increment the day and update the threshold.
            day++;
            dailyThreshold += thresholdIncrement;
        }

        // End game conditions.
        if (playerCash <= 0)
        {
            uiHandler.ShowGameOver("You ran out of money! Game over.");
        }
        else if (playerCash >= dailyThreshold)
        {
            uiHandler.ShowVictory($"Congratulations! You reached the daily threshold of ${dailyThreshold}!");
        }
    }

    private IEnumerator PlayRound()
    {
        // Deal out cards.
        yield return StartCoroutine(cardHandler.StartGame());

        // Player's turn.
        bool gameConcluded = false;
        while (!gameConcluded)
        {
            // Wait for the player to make a decision.
            yield return StartCoroutine(uiHandler.PlayerWantsToHit());

            // Check the player’s decision after they’ve confirmed it.
            if (uiHandler.playerHitting)
            {
                // Player chooses to "Hit."
                cardHandler.PlayerHit(out gameConcluded);
            }
            else
            {
                // Player chooses to "Stand."
                break;
            }
        }

        // Dealer's turn.
        if (!gameConcluded)
        {
            cardHandler.DealerTurn(out gameConcluded);
        }
    }
}
