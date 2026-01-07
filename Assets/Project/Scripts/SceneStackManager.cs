using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStackManager : MonoBehaviour
{
    public static SceneStackManager Instance;
    
    private int[] SceneBuildIndices = new int[50];
    private int SceneIndexPointer = 0;
    private bool isReturning = false; // Flag to prevent auto-pushing when returning

    void Awake() 
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        // Subscribe to scene loading events
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        // Push initial scene
        PushScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Don't auto-push when we're returning to a previous scene
        if (!isReturning)
        {
            OnSceneChanged();
        }
        else
        {
            isReturning = false; // Reset flag
            DisplayStack();
        }
    }

    public void OnSceneChanged()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Only push if it's different from the top of the stack
        if (SceneIndexPointer == 0 || Peek() != currentSceneIndex)
        {
            PushScene(currentSceneIndex);
        }
        
        DisplayStack();
    }

    public void PushScene(int sceneIndex)
    {
        if (SceneIndexPointer < SceneBuildIndices.Length)
        {
            SceneBuildIndices[SceneIndexPointer] = sceneIndex;
            SceneIndexPointer++;
        }
        else
        {
            Debug.LogWarning("Scene stack is full!");
        }
    }

    public int Peek()
    {
        if (SceneIndexPointer > 0)
        {
            return SceneBuildIndices[SceneIndexPointer - 1];
        }
        return -1;
    }

    public int Pop()
    {
        if (SceneIndexPointer > 0)
        {
            SceneIndexPointer--;
            int poppedScene = SceneBuildIndices[SceneIndexPointer];
            return poppedScene;
        }
        return -1;
    }

    public void ReturnToPreviousScene()
    {
        // Pop the current scene off the stack
        Pop();
        
        // Get the previous scene (now at top of stack)
        int previousSceneIndex = Peek();
        
        if (previousSceneIndex != -1)
        {
            isReturning = true; // Set flag so we don't push again
            SceneManager.LoadScene(previousSceneIndex);
        }
        else
        {
            Debug.LogWarning("No previous scene to return to!");
        }
    }

    private void DisplayStack()
    {
        string stackContents = "Stack: [";
        for (int i = 0; i < SceneIndexPointer; i++)
        {
            stackContents += SceneBuildIndices[i];
            if (i < SceneIndexPointer - 1)
            {
                stackContents += ", ";
            }
        }
        stackContents += "]";
    }
    
    void OnDestroy()
    {
        // Unsubscribe from events
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}