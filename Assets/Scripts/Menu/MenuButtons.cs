using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] AudioClip themeSong;
    [SerializeField] GameObject fadeOut;
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

    void Start() {

        // Start the first song
        audioSource.clip = themeSong;
        audioSource.Play();

    }

    public void StartGame()
    {   
        StartCoroutine(StartingGame()); 
    }

    IEnumerator StartingGame() {

        fadeOut.SetActive(true);
        // Fade out the current song
        yield return StartCoroutine(FadeOutMusic(2f));


        SceneManager.LoadScene(1); // Loads the scene with build index 1
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit(); // Quits the application
    }

    public void LoadCreditsScene()
    {
        SceneManager.LoadScene("CreditsScene"); // Loads the scene named "CreditsScene"
    }

    public void LoadObjectiveScene()
    {
        SceneManager.LoadScene("ObjectiveScene"); // Loads the scene named "ObjectiveScene"
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadScene("TitleScreen"); // Loads the scene named "TitleScreen"
    }
}
