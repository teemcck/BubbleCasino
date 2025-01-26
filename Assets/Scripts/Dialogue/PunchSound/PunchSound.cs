using UnityEngine;

public class PunchSound : MonoBehaviour
{
   public AudioSource audioSource; // Assign an AudioSource in the Inspector
    public AudioClip punchSound;   // Assign the punch sound clip in the Inspector

    private void OnEnable()
    {
        if (audioSource != null && punchSound != null)
        {
            audioSource.PlayOneShot(punchSound);
        }
       
    }
}
