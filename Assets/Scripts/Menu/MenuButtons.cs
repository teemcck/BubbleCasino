using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void QuitGame() {

        Debug.Log("QUIT!");
        Application.Quit();
    }
    
}
