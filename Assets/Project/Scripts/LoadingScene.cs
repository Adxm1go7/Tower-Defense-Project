using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public float minLoadingTime = 5f;
    public Slider loadingBar;

    public Button EasyDifficulty;
    public Button MediumDifficulty;
    public Button HardDifficulty;
    public Button ImpossibleDifficulty;

    private DifficultyStats currentDifficulty;

    private AsyncOperation loadingOperation;

    void Start()
    {
        currentDifficulty = GameManager.Instance.currentDifficulty;
        disableDifficultyButton();
    }

    public void disableDifficultyButton()
    {
        enableDifficultyButtons();
        if (currentDifficulty.name == "Easy")
        {
            EasyDifficulty.interactable = false;
        } 
        else if (currentDifficulty.name == "Medium")
        {
            MediumDifficulty.interactable = false;
        }
        else if (currentDifficulty.name == "Hard")
        {
            HardDifficulty.interactable = false;
        }
        else if (currentDifficulty.name == "Impossible")
        {
            ImpossibleDifficulty.interactable = false;
        }
    }

    public void enableDifficultyButtons()
    {
        EasyDifficulty.interactable=true;
        MediumDifficulty.interactable=true;
        HardDifficulty.interactable=true;
        ImpossibleDifficulty.interactable=true;
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadSceneAsync(sceneIndex));
    }

    private IEnumerator LoadSceneAsync(int sceneIndex)
    {
        float elapsedTime = 0f;

        if (loadingBar != null)
        {
            loadingBar.value = 0f;
        }

        loadingOperation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingOperation.allowSceneActivation = false;

        while (!loadingOperation.isDone)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(loadingOperation.progress / 0.9f);
            loadingBar.value = progress;

            if (loadingOperation.progress >= 0.9f && elapsedTime >= minLoadingTime)
            {
                loadingOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    public void SetDifficulty(DifficultyStats difficulty)
    { 
        Debug.Log("Changing difficulty to " + difficulty.name);
        GameManager.Instance.SetDifficulty(difficulty);
        currentDifficulty = GameManager.Instance.currentDifficulty;
        disableDifficultyButton();
    }




}
