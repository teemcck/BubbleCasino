using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private CardHandler cardHandler; // Reference to CardHandler.
    [SerializeField] private GameObject cardPrefab;   // Reference to the card prefab.
    [SerializeField] private Transform playerCardsParent; // Parent for player cards.
    [SerializeField] private Transform dealerCardsParent; // Parent for dealer cards.
    [SerializeField] private GameObject hitAndStandButtons; // Reference to hit and stand buttons.
    [SerializeField] private TMP_Text dayStatusText; // TMP text to show day status.
    [SerializeField] private TMP_Text playerCashText; // TMP text to show player cash.
    [SerializeField] private TMP_InputField betInputField; // TMP input field for player's bet.
    [SerializeField] private GameObject betPanel; // Panel for placing bets.
    [SerializeField] private GameObject continuePanel; // Panel for asking to continue.
    [SerializeField] private TMP_Text resultText; // TMP text to show round results.

    public bool playerHitting = false;
    private bool playerMadeChoice = true;
    public bool PlayerWantsToContinue { get; private set; } = true;
    public int PlayerBet { get; private set; } = 0;

    public void UpdateUI()
    {
        // Clear old cards from the parent GameObjects.
        ClearCards(playerCardsParent);
        ClearCards(dealerCardsParent);

        // Update Player UI.
        List<CardObject> playerHand = cardHandler.GetPlayerHand();
        Vector3 playerStartPosition = new Vector3(-300, 0, 0);
        RenderCards(playerHand, playerCardsParent, playerStartPosition);

        // Update Dealer UI.
        List<CardObject> dealerHand = cardHandler.GetDealerHand();
        Vector3 dealerStartPosition = new Vector3(-300, 300, 0);
        RenderCards(dealerHand, dealerCardsParent, dealerStartPosition);
    }

    private void ClearCards(Transform parent)
    {
        // Destroy all child GameObjects under the parent.
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }

    private void RenderCards(List<CardObject> hand, Transform parent, Vector3 startPosition)
    {
        for (int i = 0; i < hand.Count; i++)
        {
            // Instantiate a new card prefab.
            GameObject cardObject = Instantiate(cardPrefab, parent);

            // Initialize the card prefab with the sprite.
            CardPrefab cardPrefabScript = cardObject.GetComponent<CardPrefab>();
            string spritePath = "CardSprites/" + hand[i].CardID; // Path based on Card ID.

            // Set the position of the card.
            cardObject.transform.localPosition = startPosition + new Vector3(250 * i, 0, 0);

            // Initialize the card.
            cardPrefabScript.InitializeCard(spritePath);
        }
    }

    public IEnumerator PlayerWantsToHit()
    {
        // Enable buttons for player input.
        hitAndStandButtons.SetActive(true);

        playerMadeChoice = false;

        // Wait until the player makes a choice.
        while (!playerMadeChoice)
        {
            yield return null; // Wait for the next frame.
        }

        // Disable buttons after the choice.
        hitAndStandButtons.SetActive(false);
    }

    public void OnHitButtonClicked()
    {
        playerHitting = true;
        playerMadeChoice = true;
    }

    public void OnStandButtonClicked()
    {
        playerHitting = false;
        playerMadeChoice = true;
    }

    public void ShowDayStatus(int day, int playerCash, int threshold)
    {
        dayStatusText.text = $"Day {day}\nCash: ${playerCash}\nThreshold: ${threshold}";
    }

    public IEnumerator GetPlayerBet()
    {
        betPanel.SetActive(true);
        PlayerBet = 0;

        // Wait until a valid bet is placed.
        bool validBet = false;
        while (!validBet)
        {
            if (int.TryParse(betInputField.text, out int bet) && bet > 0)
            {
                PlayerBet = bet;
                validBet = true;
            }
            else
            {
                // Optionally: Display a warning about invalid bet input.
            }

            yield return null; // Wait for the next frame.
        }

        betPanel.SetActive(false);
    }

    public void ShowInvalidBetMessage()
    {
        resultText.text = "Invalid bet! Try again.";
        resultText.gameObject.SetActive(true);
    }

    public void ShowRoundResult(string message, int reward)
    {
        resultText.text = $"{message}\nYou earned: ${reward}";
        resultText.gameObject.SetActive(true);
    }

    public IEnumerator AskPlayerToContinue()
    {
        continuePanel.SetActive(true);
        PlayerWantsToContinue = false;

        // Wait for player input to continue or exit.
        while (!PlayerWantsToContinue)
        {
            yield return null; // Wait for the next frame.
        }

        continuePanel.SetActive(false);
    }

    public void OnContinueButtonClicked()
    {
        PlayerWantsToContinue = true;
    }

    public void OnQuitButtonClicked()
    {
        PlayerWantsToContinue = false;
    }

    public void ShowGameOver(string message)
    {
        dayStatusText.text = message;
    }

    public void ShowVictory(string message)
    {
        dayStatusText.text = message;
    }
}
