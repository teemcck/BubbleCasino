using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void StartGame()
    {
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
    public void LoadTitleScreen()
    {
        SceneManager.LoadScene("TitleScreen"); // Loads the scene named "CreditsScene"
    }

}
