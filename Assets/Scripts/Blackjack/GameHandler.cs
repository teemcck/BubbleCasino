using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private CardHandler cardHandler;
    private bool gameConcluded = false;

    void Start()
    {
        cardHandler.StartGame();

        while (!gameConcluded) 
        {
            // Player plays until stay or busts.
            // If bust:
            //      game ends.
            // If stay:
            //      If Dealer hand is <= player hand:
            //         Dealer hits.
            //         If bust:
            //         game ends
            //      Else:
            //         Dealer stays.
            // Decide winner.

            gameConcluded = true;
        }
    }

    private void playTurn()
    {
        cardHandler.PlayerTurn(out gameConcluded);
        cardHandler.DealerTurn(out gameConcluded);
    }
}