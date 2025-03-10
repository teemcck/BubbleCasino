using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private CardHandler cardHandler; // Reference to CardHandler.
    [SerializeField] private UIHandler uiHandler; // Reference to UIHandler.
    [SerializeField] private BlackjackEvents eventHandler; // Reference to BlackjackEvents.
    private int playerCash = 1600; // Player starts with $1600.
    private int dailyThreshold = 3200; // Starting threshold is $3200.
    [SerializeField] private int FailureSceneIndex;
    [SerializeField] private int SuccessSceneIndex;

    // Sound effects.
    [SerializeField] private AudioSource moneySound;
    [SerializeField] private AudioSource loseRoundSound;

    private int currentBet = 0; // Player's bet for the current round.

    private IEnumerator Start()
    {
        // Start the EventStart coroutine.
        StartCoroutine(eventHandler.EventStart());

        // Wait until dialogFinished is true.
        while (!eventHandler.dialogFinished)
        {
            yield return null; // Wait for the next frame.
        }

        // Once the dialog is done, start the blackjack game.
        yield return StartCoroutine(PlayBlackjack());
    }

    private IEnumerator PlayBlackjack()
    {
        int day = 1;

        // Continue playing as long as player has cash and hasn't met the daily threshold.
        while (playerCash > 0 && day <= 3)
        {
            if (playerCash >= dailyThreshold) // If the player reaches the daily threshold, move to the next day.
            {
                if (day == 3)
                {
                    // Day three is complete; transition to success scene.
                    uiHandler.ShowVictory($"Congratulations! You successfully completed day {day}!");
                    yield return new WaitForSeconds(2f);
                    SceneManager.LoadScene(SuccessSceneIndex); // Transition to success scene.
                    yield break; // End the coroutine.
                }

                day++; // Increment day.
                dailyThreshold *= 2; // Double the threshold.
                playerCash = Mathf.Max(playerCash, 1600); // Ensure player has at least starting cash for the next day.
                yield return StartCoroutine(uiHandler.HandleDayNightTransition()); // Transition to night.
            }

            // Show the current day's status.
            uiHandler.ShowDayStatus(day, playerCash, dailyThreshold);

            // Let the player place a bet.
            yield return StartCoroutine(uiHandler.GetPlayerBet(playerCash));
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
                // Player wins.
                playerCash += currentBet * 2; // Player wins double their bet.
                if (moneySound != null) moneySound.Play(); // Play the success sound.
                yield return StartCoroutine(uiHandler.ShowRoundResult(1, currentBet * 2));
            }
            else if (result == -1)
            {
                // Player loses.
                if (loseRoundSound != null) loseRoundSound.Play(); // Play the failure sound.
                yield return StartCoroutine(uiHandler.ShowRoundResult(-1, 0));
            }
            else
            {
                // It's a tie.
                playerCash += currentBet; // Refund the bet in case of a tie.
                uiHandler.ShowRoundResult(0, currentBet);
            }

            // Reset the game before continuing.
            cardHandler.ResetGame();
        }

        // End game conditions.
        if (playerCash <= 0)
        {
            // Player runs out of money.
            uiHandler.ShowGameOver("You ran out of money! Game over.");
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(FailureSceneIndex); // Go to failure scene.
        }
    }

    private IEnumerator PlayRound()
    {
        // Deal out cards.
        yield return StartCoroutine(cardHandler.StartGame());

        while (!cardHandler.gameConcluded)
        {
            // Wait for the player to make a decision.
            yield return StartCoroutine(uiHandler.PlayerWantsToHit());

            // Check the player�s decision after they�ve confirmed it.
            if (uiHandler.playerHitting)
            {
                // Player chooses to "Hit."
                cardHandler.PlayerHit();
            }
            else
            {
                // Player chooses to "Stand."
                break;
            }
        }

        // Dealer's turn.
        if (!cardHandler.gameConcluded)
        {
            yield return new WaitForSeconds(.5f);
            cardHandler.DealerTurn();
        }
    }
}
