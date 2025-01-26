using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private CardHandler cardHandler; // Reference to CardHandler.
    [SerializeField] private UIHandler uiHandler; // Reference to UIHandler.
    [SerializeField] private BlackjackEvents eventHandler; // Reference to BlackjackEvents
    private int playerCash = 1600; // Player starts with $1600.
    private int dailyThreshold = 3200; // Starting threshold is $3200.

    private int currentBet = 0; // Player's bet for the current round.

    private IEnumerator Start()
    {
        // Start the EventStart coroutine
        StartCoroutine(eventHandler.EventStart());

        // Wait until dialogFinished is true
        while (!eventHandler.dialogFinished)
        {
            yield return null; // Wait for the next frame
        }

        // Once the dialog is done, start the blackjack game
        yield return StartCoroutine(PlayBlackjack());
    }

    private IEnumerator PlayBlackjack()
    {
        int day = 1;

        // Continue playing as long as player has cash and hasn't met the daily threshold
        while (playerCash > 0)
        {
            if (playerCash >= dailyThreshold) // If the player reaches the daily threshold, move to the next day
            {
                day++; // Increment day
                dailyThreshold *= 2; // Double the threshold
                playerCash = Mathf.Max(playerCash, 1600); // Ensure player has at least starting cash for the next day
            }

            // Show the current day's status
            uiHandler.ShowDayStatus(day, playerCash, dailyThreshold);

            // Let the player place a bet
            yield return StartCoroutine(uiHandler.GetPlayerBet(playerCash));
            currentBet = uiHandler.PlayerBet;

            // Ensure the bet is valid
            if (currentBet <= 0 || currentBet > playerCash)
            {
                uiHandler.ShowInvalidBetMessage();
                continue;
            }

            // Deduct the bet amount from player's cash temporarily
            playerCash -= currentBet;

            // Start the game round
            yield return StartCoroutine(PlayRound());

            // Determine the outcome of the round
            int result = cardHandler.DetermineWinner(); // Returns 1 for win, -1 for loss, 0 for tie
            if (result == 1)
            {
                playerCash += currentBet * 2; // Player wins double their bet
                yield return StartCoroutine(uiHandler.ShowRoundResult(1, currentBet * 2));
            }
            else if (result == -1)
            {
                yield return StartCoroutine(uiHandler.ShowRoundResult(-1, 0));
            }
            else
            {
                playerCash += currentBet; // Refund the bet in case of a tie
                uiHandler.ShowRoundResult(0, currentBet);
            }

            // Reset the game before continuing
            cardHandler.ResetGame();
        }

        // End game conditions
        if (playerCash <= 0)
        {
            uiHandler.ShowGameOver("You ran out of money! Game over.");
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(0); // Go back to scene 0 (main menu)
        }
        else if (playerCash >= dailyThreshold)
        {
            uiHandler.ShowVictory($"Congratulations! You reached the daily threshold of ${dailyThreshold}!");
        }
    }

    private IEnumerator PlayRound()
    {
        // Deal out cards
        yield return StartCoroutine(cardHandler.StartGame());

        // Player's turn
        bool gameConcluded = false;
        while (!gameConcluded)
        {
            // Wait for the player to make a decision
            yield return StartCoroutine(uiHandler.PlayerWantsToHit());

            // Check the player’s decision after they’ve confirmed it
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

        // Dealer's turn
        if (!gameConcluded)
        {
            cardHandler.DealerTurn(out gameConcluded);
        }
    }
}
