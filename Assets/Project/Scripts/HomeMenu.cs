using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeMenu : MonoBehaviour
{
    bool difficultyPanelVisibility;
    public GameObject difficultyPanel;
    public DifficultyStats easyDifficulty;
    public DifficultyStats mediumDifficulty;
    public DifficultyStats hardDifficulty;
    public DifficultyStats impossibleDifficulty;
    
    void Awake(){
        difficultyPanelVisibility = false;
        difficultyPanel.SetActive(difficultyPanelVisibility);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2); // Loads level selector
    }

    public void ToggleDifficultyPanel()
    {
        difficultyPanelVisibility = !difficultyPanelVisibility;
        difficultyPanel.SetActive(difficultyPanelVisibility); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetEasyDifficulty(){
        GameManager.Instance.SetDifficulty(easyDifficulty);
        ToggleDifficultyPanel();
    }
    public void SetMediumDifficulty(){
        GameManager.Instance.SetDifficulty(mediumDifficulty);
        ToggleDifficultyPanel();
    }
    public void SetHardDifficulty(){
        GameManager.Instance.SetDifficulty(hardDifficulty);
        ToggleDifficultyPanel();
    }
    public void SetImpossibleDifficulty(){
        GameManager.Instance.SetDifficulty(impossibleDifficulty);
        ToggleDifficultyPanel();
    }
}
