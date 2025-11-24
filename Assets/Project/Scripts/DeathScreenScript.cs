using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DeathScreenScript : MonoBehaviour
{
    public void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void SelectStage()
    {
        SceneManager.LoadScene(2); // Loads level selector
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
