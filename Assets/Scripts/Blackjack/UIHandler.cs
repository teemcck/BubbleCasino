using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private CardHandler cardHandler;

    public void UpdatePlayerUI()
    {
        List<CardObject> dealerHand = cardHandler.GetDealerHand();

        // Define the starting position for the cards.
        Vector3 startPosition = new Vector3(-300, 300, 0);

        // Loop through each card and create an Image for it in the UI.
        for (int i = 0; i < dealerHand.Count; i++)
        {
            // Create a new GameObject for the card.
            GameObject cardObject = new GameObject($"Card_{dealerHand[i].CardID}");
            cardObject.transform.SetParent(canvas.transform);  // Set it as a child of the canvas.

            Image cardImage = cardObject.AddComponent<Image>();  // Add an Image component.

            // Load the sprite based on the card's ID.
            Sprite cardSprite = LoadCardSprite(dealerHand[i].CardID);

            // Assign the sprite to the Image component.
            cardImage.sprite = cardSprite;

            // Position the card based on its index in the deck (for example, offset horizontally).
            cardObject.transform.localPosition = startPosition + new Vector3(100 * i, 0, 0);

            // You can also scale the cards if needed, to ensure they fit well on the screen.
            cardImage.rectTransform.sizeDelta = new Vector2(70, 100);
        }
    }

    public void UpdateDealerUI()
    {
        List<CardObject> playerHand = cardHandler.GetPlayerHand();

        // Define the starting position for the cards.
        Vector3 startPosition = new Vector3(-300, 0, 0);

        // Loop through each card and create an Image for it in the UI.
        for (int i = 0; i < playerHand.Count; i++)
        {
            // Create a new GameObject for the card.
            GameObject cardObject = new GameObject($"Card_{playerHand[i].CardID}");
            cardObject.transform.SetParent(canvas.transform);  // Set it as a child of the canvas.

            Image cardImage = cardObject.AddComponent<Image>();  // Add an Image component.

            // Load the sprite based on the card's ID.
            Sprite cardSprite = LoadCardSprite(playerHand[i].CardID);

            // Assign the sprite to the Image component.
            cardImage.sprite = cardSprite;

            // Position the card based on its index in the deck (for example, offset horizontally).
            cardObject.transform.localPosition = startPosition + new Vector3(100 * i, 0, 0);

            // You can also scale the cards if needed, to ensure they fit well on the screen.
            cardImage.rectTransform.sizeDelta = new Vector2(70, 100);
        }
    }

    // Load the card sprite from the Resources folder based on the card ID.
    private Sprite LoadCardSprite(int cardID)
    {
        string spritePath = "CardSprites/" + cardID;  // Build the path to the sprite file (e.g., "CardSprites/1").
        return Resources.Load<Sprite>(spritePath);    // Load the sprite dynamically from Resources.
    }
}
