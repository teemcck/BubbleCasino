using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private CardHandler cardHandler; // Reference to CardHandler.
    [SerializeField] private GameObject cardPrefab;   // Reference to the card prefab.
    [SerializeField] private Transform playerCardsParent; // Parent for player cards.
    [SerializeField] private Transform dealerCardsParent; // Parent for dealer cards.
    [SerializeField] private GameObject hitAndStandButtons; // Reference to hit and stand buttons.

    public bool playerWantsToHit = false;
    private bool playerMadeChoice = true;

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
            cardPrefabScript.InitializeCard(spritePath);

            // Set the position of the card.
            cardObject.transform.localPosition = startPosition + new Vector3(250 * i, 0, 0);
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
        playerWantsToHit = true;
        playerMadeChoice = true;
    }

    public void OnStandButtonClicked()
    {
        playerWantsToHit = false;
        playerMadeChoice = true;
    }
}
