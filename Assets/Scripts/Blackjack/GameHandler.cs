using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private CardHandler cardHandler; // Reference to CardHandler.
    [SerializeField] private UIHandler uiHandler; // Reference to UIHandler.
    private bool gameConcluded = false;

    void Start()
    {
        PlayBlackjack();
    }

    private void PlayBlackjack()
    {
        cardHandler.StartGame();

        // Play player turns until player busts or stands.
        while (!gameConcluded && uiHandler.PlayerWantsToHit())
        {
            cardHandler.PlayerHit(out gameConcluded);
        }

        // Play dealers turn.
        cardHandler.DealerTurn(out gameConcluded);

        // Determine winner of the game.
        cardHandler.DetermineWinner();
    }
}