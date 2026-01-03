using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] public int health = 50;
    [SerializeField] public int numRounds = 50;
    [SerializeField] public int currentRound = 1;
    [SerializeField] public int coins = 150;
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI RoundText;
    public GameObject EnemyEndNode;

    public Enemy enemyScript;

    public GameObject upgradePanel; // Reference to the upgrade panel UI

    public GameObject towerNameObject; // Reference to the tower name UI object

    public GameObject damageDealtObject; // Reference to the damage dealt UI object 
    public TextMeshPro nameOfTower; //Name of tower text object
    public TextMeshPro DDTower; //Damage dealt by tower text object
    private TowerScript selectedTower;

    public GameObject SpeedControlButton; // Reference to the speed control button
    public float normalSpeed = 1f;
    public float fastSpeed = 2f;
    public float ultraFastSpeed = 3f;
    public float currentSpeed = 1f;

    public GameObject pauseMenuUI; // Reference to the pause menu UI

    // Start is called before the first frame update
    void Awake()
    {
        GameManager.Instance.levelManager = this;
        GameManager.Instance.StartLevel();
        
        EnemyEndNode.GetComponent<Renderer>().enabled = false;
    }

    void Update()
    {
        HealthText.text = "Health : " + health.ToString();
        CoinText.text = "Coins : " + coins.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.isPaused)
            {
                pauseMenuUI.SetActive(false);
                Time.timeScale = GameManager.Instance.currentSpeed; // Resume game at current speed
                GameManager.Instance.isPaused = false;
            }
            else
            {
                pauseMenuUI.SetActive(true);
                Time.timeScale = 0f; // Pause game
                GameManager.Instance.isPaused = true;
            }
        } 
    }

    public void HomeButton()
    {
        Time.timeScale = 1f; // Reset time scale to normal
        UnityEngine.SceneManagement.SceneManager.LoadScene(0); // Load home menu scene
    }

    public void ResumeButton()
    {
        Time.timeScale = GameManager.Instance.currentSpeed; // Resume time scale to current speed
        GameManager.Instance.isPaused = false; // Unpause the game
        pauseMenuUI.SetActive(false); // Hide pause menu UI
    }

    public void RestartButton()
    {
        Time.timeScale = 1f; // Reset time scale to normal
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex); // Reload current scene
        GameManager.Instance.isPaused = false; // Unpause the game
        pauseMenuUI.SetActive(false); // Hide pause menu UI
    }

    public void OpenArcana()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(4); // Load arcana scene
    }

     public void SetGameSpeed(float speed)
    {
        GameManager.Instance.currentSpeed = speed;
        Time.timeScale = speed;
        Debug.Log("Game speed set to: " + speed + "x");
    }

     public void CycleGameSpeed()
    {
        if (!GameManager.Instance.isPaused){
            if (GameManager.Instance.currentSpeed == normalSpeed)
            {
                SetGameSpeed(fastSpeed);
                SpeedControlButton.GetComponentInChildren<TextMeshProUGUI>().text = "2x";
            }
            else if (GameManager.Instance.currentSpeed == fastSpeed)
            {
                SetGameSpeed(ultraFastSpeed);
                SpeedControlButton.GetComponentInChildren<TextMeshProUGUI>().text = "3x";
            }
            else
            {
                SetGameSpeed(normalSpeed);
                SpeedControlButton.GetComponentInChildren<TextMeshProUGUI>().text = "1x";
            }
        }
    }

    // Dynamic UI changing button colors depending on money available for building towers

    public void TowerButtonColor(GameObject towerButton, int towerCost)
    {
        if (coins >= towerCost)
        {
            towerButton.GetComponent<UnityEngine.UI.Image>().color = Color.white; // Affordable - normal color
        }
        else
        {
            towerButton.GetComponent<UnityEngine.UI.Image>().color = Color.red; // Not affordable - red color
        }
    }


}
