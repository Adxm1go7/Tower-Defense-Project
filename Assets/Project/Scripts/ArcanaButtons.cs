using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcanaButtons : MonoBehaviour
{
    SceneStackManager sceneStackManager = SceneStackManager.Instance;
    public void ReturnButton()
    {
        sceneStackManager.ReturnToPreviousScene();
    }

    public void EnemiesButton()
    {
        Debug.Log("Enemies Button Pressed");
        UnityEngine.SceneManagement.SceneManager.LoadScene(4); // Load enemy selection scene
    }

    public void TowersButton()
    {
        Debug.Log("Towers Button Pressed");
        UnityEngine.SceneManagement.SceneManager.LoadScene(5); // Load tower selection scene
    }

}
