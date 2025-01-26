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
    [SerializeField] private TMP_InputField betInputField; // TMP input field for player's bet.
    [SerializeField] private GameObject betPanel; // Panel for placing bets.
    [SerializeField] private GameObject continuePanel; // Panel for asking to continue.
    [SerializeField] private TMP_Text playerCashText; // TMP text to show player's current cash.

    // References for the win/loss images.
    [SerializeField] private GameObject youWinImage;  // "You Win" image.
    [SerializeField] private GameObject youLoseImage; // "You Lose" image.

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

        // Update Dealer UI
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

    public IEnumerator GetPlayerBet(int playerCash)
    {
        betPanel.SetActive(true);
        PlayerBet = 0;

        // Wait until a valid bet is placed and Enter is pressed.
        bool validBet = false;
        while (!validBet)
        {
            // Check if Enter key is pressed.
            if (Input.GetKeyDown(KeyCode.Return)) // "Return" is the Enter key.
            {
                if (int.TryParse(betInputField.text, out int bet) && bet > 0 && bet <= playerCash)
                {
                    PlayerBet = bet;
                    validBet = true;
                }
                else
                {
                    ShowInvalidBetMessage(); // Display a message if the bet is invalid.
                }
            }

            yield return null; // Wait for the next frame.
        }

        betPanel.SetActive(false);
    }

    public void ShowInvalidBetMessage()
    {
        // Show a message about the invalid bet.
        dayStatusText.text = "Invalid bet! Enter a number less than your cash.";
    }

    public void UpdatePlayerCashUI(int playerCash)
    {
        playerCashText.text = $"Cash: ${playerCash}";
    }

    public IEnumerator ShowRoundResult(int result, int reward)
    {
        // Hide both win/loss images by default.
        youWinImage.SetActive(false);
        youLoseImage.SetActive(false);

        GameObject imageToBlink = null;

        // Determine which image should blink based on the result.
        if (result == 1)
        {
            imageToBlink = youWinImage; // Set the "You Win" image to blink.
        }
        else if (result == -1)
        {
            imageToBlink = youLoseImage; // Set the "You Lose" image to blink.
        }
        else
        {
            // Handle push later.
        }
           
        if (imageToBlink != null)
        {
            // Blink the image for 1 second.
            float blinkDuration = .8f;
            float blinkInterval = 0.2f;
            float elapsedTime = 0f;

            while (elapsedTime < blinkDuration)
            {
                imageToBlink.SetActive(!imageToBlink.activeSelf); // Toggle visibility.
                yield return new WaitForSeconds(blinkInterval); // Wait for the blink interval.
                elapsedTime += blinkInterval;
            }

            // Ensure the image is not visible at the end of the blinking.
            imageToBlink.SetActive(false);
        }

        // Update the day status text with rewards.
        dayStatusText.text = $"{(result == 1 ? "You win!" : "You lose.")}\nYou earned: ${reward}";
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
