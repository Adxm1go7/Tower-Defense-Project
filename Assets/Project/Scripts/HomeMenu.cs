using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(2); // Loads level selector
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
