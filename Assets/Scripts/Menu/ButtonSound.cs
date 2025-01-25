using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource

    public void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play(); // Play the assigned sound
        }
    }
}
