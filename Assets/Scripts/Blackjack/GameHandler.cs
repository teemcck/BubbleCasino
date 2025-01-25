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
            gameConcluded = true;
        }
    }

    private void playTurn()
    {
        cardHandler.PlayerTurn();
        cardHandler.DealerTurn();
    }
}