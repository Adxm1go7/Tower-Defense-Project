using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] public int health = 200;
    [SerializeField] public int numRounds = 25;
    [SerializeField] public int currentRound = 1;
    [SerializeField] public int coins = 0;
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

    public Slider GameSpeedSlider; // Reference to the speed control Slider
    public float normalSpeed = 1f;
    public float fastSpeed = 2f;
    public float ultraFastSpeed = 3f;
    public float currentSpeed = 1f;

    public GameObject pauseMenuUI; // Reference to the pause menu UI

    public GameObject GameLevel;
    public GameObject TheArcana;
    public GameObject ArcanaTowers;
    public GameObject ArcanaEnemies;

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
        CloseAllPanels();
        TheArcana.SetActive(true);
    }

    public void CloseArcana()
    {
        CloseAllPanels();
        GameLevel.SetActive(true);
    }

    public void ArcanaTowersInfo()
    {
        CloseAllPanels();
        ArcanaTowers.SetActive(true);

    }

    public void ArcanaEnemiesInfo()
    {
        CloseAllPanels();
        ArcanaEnemies.SetActive(true);
    }


    public void CloseAllPanels()
    {
        ArcanaEnemies.SetActive(false);
        ArcanaTowers.SetActive(false);
        TheArcana.SetActive(false);
        GameLevel.SetActive(true);
    }

    public void SetGameSpeed()
    {
        Debug.Log("Game Speed Slider Value: " + GameSpeedSlider.value);
        GameSpeedSlider.onValueChanged.AddListener(delegate { UpdateGameSpeed(GameSpeedSlider.value); });
    }

    public void UpdateGameSpeed(float value)
    {
        if (value == 1)
        {
            currentSpeed = normalSpeed;
        }
        else if (value == 2)
        {
            currentSpeed = fastSpeed;
        }
        else if (value == 3)
        {
            currentSpeed = ultraFastSpeed;
        }

        GameManager.Instance.currentSpeed = currentSpeed;
    }




}
