using UnityEngine;

public class CardPrefab : MonoBehaviour
{
    private RectTransform rectTransform;

    private Vector3 startPosition; // The initial position of the card.
    private Vector3 targetPosition; // The target position of the card.
    private float animationTime = 0.1f; // The duration of the animation.
    private float elapsedTime = 0f; // Timer to track animation progress.
    private bool isAnimating = true; // Whether the card is animating.

    // Start is called once before the first execution of Update after the MonoBehaviour is created.
    private void Start()
    {
        // Get the RectTransform component.
        rectTransform = GetComponent<RectTransform>();

        if (rectTransform != null)
        {
            // Double the size of the card by modifying sizeDelta.
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x * 2f, rectTransform.sizeDelta.y * 2f);

            // Set the starting and target positions for the animation
            startPosition = rectTransform.localPosition + new Vector3(0, 50, 0); // Start off-screen above.
            targetPosition = rectTransform.localPosition; // End at the original position.
            rectTransform.localPosition = startPosition; // Start at the off-screen position.
        }
        else
        {
            Debug.LogWarning("No RectTransform found on the card prefab!");
        }
    }

    private void Update()
    {
        if (isAnimating)
        {
            // Animate the card's position using Lerp.
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / animationTime; // Normalize time (0 to 1).

            if (progress >= 1f)
            {
                progress = 1f;
                isAnimating = false; // Animation is complete.
            }

            // Ease down effect using Lerp
            rectTransform.localPosition = Vector3.Lerp(startPosition, targetPosition, progress);
        }
    }

    public void InitializeCard(string spritePath)
    {
        // Load the sprite dynamically from the Resources folder.
        Sprite cardSprite = Resources.Load<Sprite>(spritePath);

        if (cardSprite != null)
        {
            // Set the sprite on the Image component.
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
