using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverSequence : MonoBehaviour
{
 //References
    public GameObject fadeScreenIn;
    [SerializeField] GameObject fadeScreenOut;
    [SerializeField] GameObject description;
    [SerializeField] GameObject exitButton;

    //Audio
    [SerializeField] AudioClip initialSong;

    //Next Button
    [SerializeField] GameObject returnGame;
    [SerializeField] int eventPos = 0;
 


    //SoundControl
    [SerializeField] AudioSource audioSource;
    private IEnumerator FadeOutMusic(float fadeDuration)
{
    float startVolume = audioSource.volume;

    while (audioSource.volume > 0)
    {
        audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
        yield return null;
    }

    audioSource.Stop();
    audioSource.volume = startVolume; // Reset volume for next use
}

    void Start()
    {
        StartCoroutine(EventStart());
        // Start the first song
        audioSource.clip = initialSong;
        audioSource.Play();
    }

    IEnumerator EventStart()
    {
        // Event 0
        yield return new WaitForSeconds(2f);
        description.SetActive(true);
        yield return new WaitForSeconds(2f);
        fadeScreenIn.SetActive(false);
        exitButton.SetActive(true);
        returnGame.SetActive(true); 

    }

    public void ReturnGameSelection() {

        StartCoroutine(ReturnToGameSelection());
    }

    public void ReturnToMenu() {

        StartCoroutine(ReturnToMainMenu());
    }




    IEnumerator ReturnToGameSelection() {


        fadeScreenOut.SetActive(true);
        returnGame.SetActive(false);
        // Fade out the current song
        yield return StartCoroutine(FadeOutMusic(2f));
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(2);
    }

    IEnumerator ReturnToMainMenu() {


        fadeScreenOut.SetActive(true);
        // Fade out the current song
        yield return StartCoroutine(FadeOutMusic(2f));
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(0);
    }



}
