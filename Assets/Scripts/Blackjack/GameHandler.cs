using UnityEngine;
using System.Collections;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private CardHandler cardHandler; // Reference to CardHandler.
    [SerializeField] private UIHandler uiHandler; // Reference to UIHandler.
    private bool playerWantsToHit = false;

    public bool PlayerWantsToHit => playerWantsToHit;

    private void Start()
    {
        StartCoroutine(PlayBlackjack());
    }

    private IEnumerator PlayBlackjack()
    {
        cardHandler.StartGame();

        // Player's turn.
        bool gameConcluded = false;
        while (!gameConcluded)
        {
            // Wait for the player to make a decision.
            yield return StartCoroutine(uiHandler.PlayerWantsToHit());

            // Now we check the player’s decision after they’ve confirmed it
            if (uiHandler.playerWantsToHit)
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

        // If neither busted, determine the winner.
        if (!gameConcluded)
        {
            cardHandler.DetermineWinner();
        }
    }
}