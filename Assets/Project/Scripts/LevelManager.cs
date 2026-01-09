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
    public TextMeshProUGUI nameOfTower; //Name of tower text object
    public TextMeshProUGUI DDTower; //Damage dealt by tower text object
    public TextMeshProUGUI SpecialAtk; //Special attack text object
    private TowerScript selectedTower;
    public GameObject SidePanel;

    [SerializeField]public Camera mainCamera;

    public Slider GameSpeedSlider; // Reference to the speed control Slider
    public float normalSpeed = 1f;
    public float fastSpeed = 2f;
    public float ultraFastSpeed = 3f;
    public float currentSpeed = 1f;

    public GameObject pauseMenuUI; // Reference to the pause menu UI
    public GameObject UpgradePanelUI; // Reference to the upgrade panel UI

    public GameObject GameLevel;
    public GameObject TheArcana;
    public GameObject ArcanaTowers;
    public GameObject ArcanaEnemies;

    public GameObject DeathScreen;
    public GameObject WinScreen;
    public bool allRoundsPlayed;
    public bool noEnemiesAlive;

    // Start is called before the first frame update
    void Awake()
    {
        GameManager.Instance.levelManager = this;
        GameManager.Instance.StartLevel();
        
        EnemyEndNode.GetComponent<Renderer>().enabled = false;
    }

    void Start()
    {
        CloseAllPanels();
        GameLevel.SetActive(true);
        pauseMenuUI.SetActive(false);
        UpgradePanelUI.SetActive(false);
        allRoundsPlayed = false;
        noEnemiesAlive = false;

    }

    void Update()
    {
        HealthText.text = health.ToString();
        CoinText.text = coins.ToString();

        checkForVictory();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.isPaused)
            {
                pauseMenuUI.SetActive(false);
                ContinueGame();
            }
            else
            {
                pauseMenuUI.SetActive(true);
                PauseGame();
            }
        } 
    }

    public void ContinueGame()
    {
        Time.timeScale = GameManager.Instance.currentSpeed; // Resume game at current speed
        GameManager.Instance.isPaused = false;
    }
    public void PauseGame()
    {
        Time.timeScale = 0f; // Pause game
        GameManager.Instance.isPaused = true;
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
    public void StagesButton()
    {
        SceneManager.LoadScene(2);
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
        DeathScreen.SetActive(false);
        WinScreen.SetActive(false);
    }

    public void CloseSidePanel()
    {
        SidePanel.SetActive(false);
    }

    public void OpenSidePanel()
    {
        SidePanel.SetActive(true);
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

    public void checkForVictory()
    {
        if (allRoundsPlayed && noEnemiesAlive)
        {
            SidePanel.SetActive(false);
            WinScreen.SetActive(true);
            PauseGame();
        }
    }

    public void GameOver()
    {
        health = 0;
        SidePanel.SetActive(false);
        DeathScreen.SetActive(true);
        PauseGame();
    }
}
