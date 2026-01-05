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

    bool optionsPanelVisibility;
    public GameObject optionsPanel;
    
    void Awake(){
        difficultyPanelVisibility = false;
        difficultyPanel.SetActive(difficultyPanelVisibility);

        optionsPanelVisibility = false;
        optionsPanel.SetActive(optionsPanelVisibility);
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2); // Loads level selector
    }

    public void ToggleDifficultyPanel()
    {
        if (!difficultyPanelVisibility)
        {
            // Close options panel if it's open
            optionsPanelVisibility = false;
            optionsPanel.SetActive(optionsPanelVisibility);
        }
        difficultyPanelVisibility = !difficultyPanelVisibility;
        difficultyPanel.SetActive(difficultyPanelVisibility); 
    }

    public void openArcana()
    {
        SceneManager.LoadScene(4); // Loads Arcana scene
    }

    public void ToggleOptions()
    {
        if (!optionsPanelVisibility)
        {
            // Close difficulty panel if it's open
            difficultyPanelVisibility = false;
            difficultyPanel.SetActive(difficultyPanelVisibility);
        }

        optionsPanelVisibility = !optionsPanelVisibility;
        optionsPanel.SetActive(optionsPanelVisibility);
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
