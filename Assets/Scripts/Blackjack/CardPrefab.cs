using UnityEngine;

public class CardPrefab : MonoBehaviour
{
    private RectTransform rectTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        // Get the RectTransform component
        rectTransform = GetComponent<RectTransform>();

        if (rectTransform != null)
        {
            // Double the size of the card by modifying sizeDelta
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x * 2f, rectTransform.sizeDelta.y * 2f);
        }
        else
        {
            Debug.LogWarning("No RectTransform found on the card prefab!");
        }
    }

    public void InitializeCard(string spritePath)
    {
        // Load the sprite dynamically from the Resources folder
        Sprite cardSprite = Resources.Load<Sprite>(spritePath);

        if (cardSprite != null)
        {
            // Set the sprite on the Image component
            var image = GetComponent<UnityEngine.UI.Image>();
            if (image != null)
            {
                image.sprite = cardSprite;
            }
            else
            {
                Debug.LogWarning("No Image component found on the card prefab!");
            }
        }
        else
        {
            Debug.LogWarning($"Sprite not found at path: {spritePath}");
        }
    }
}
