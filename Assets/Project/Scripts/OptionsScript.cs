using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsScript : MonoBehaviour
{
    public HomeMenu homeMenu;
    public void CloseOptionsMenu()
    {
        if (homeMenu != null)
        {
            homeMenu.ToggleOptions();
        }
        Time.timeScale = GameManager.Instance.currentSpeed; // Resume time scale to current speed
        GameManager.Instance.isPaused = false; // Unpause the game
        this.gameObject.SetActive(false); // Hide options menu UI
    }


}
