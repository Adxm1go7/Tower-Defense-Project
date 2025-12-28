using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcanaButtons : MonoBehaviour
{
    public void ReturnButton()
    {
        // NEED TO STORE PREVIOUS SCENE INDEX SOMEWHERE TO RETURN TO IT
        UnityEngine.SceneManagement.SceneManager.LoadScene(1); // Load home menu scene
    }

}
