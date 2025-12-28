using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int health;
    private int numRounds;
    private int currentRound;
    private int coins;
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI RoundText;
    public GameObject EnemyEndNode;
    public static GameManager Instance;

    private Enemy enemyScript;

        
    public GameObject upgradePanel; // Reference to the upgrade panel UI

    public GameObject towerNameObject; // Reference to the tower name UI object

    public GameObject damageDealtObject; // Reference to the damage dealt UI object 
    public TextMeshPro nameOfTower; //Name of tower text object
    public TextMeshPro DDTower; //Damage dealt by tower text object


    public GameObject SpeedControlButton; // Reference to the speed control button
    public float normalSpeed = 1f;
    public float fastSpeed = 2f;
    public float ultraSpeed = 3f;
    private float currentSpeed = 1f;

    public GameObject pauseMenuUI; // Reference to the pause menu UI
    public bool isPaused = false; // boolean to track if the game is paused

    

    // Start is called before the first frame update
    void Start()
    {
        health = 50;
        numRounds = 50;
        currentRound = 0;
        coins = 120;
        setCurrentRound();
        EnemyEndNode.GetComponent<Renderer>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        HealthText.text = "Health : " + health.ToString();
        CoinText.text = "Coins : " + coins.ToString();
        setCurrentRound();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                pauseMenuUI.SetActive(false);
                Time.timeScale = currentSpeed; // Resume game at current speed
                isPaused = false;
            }
            else
            {
                pauseMenuUI.SetActive(true);
                Time.timeScale = 0f; // Pause game
                isPaused = true;
            }
        }
    }

    void Awake()
    {
        Instance = this;   
    }

    public void setCurrentRound()
    {
        RoundText.text = "Rounds : " + currentRound.ToString() + " / " + numRounds.ToString();
    }

    public void enemySuccess(Collider enemyCollider)
    {

        enemyScript = enemyCollider.GetComponentInParent<Enemy>(); // Gets Enemy.cs from passed enemy
        int deductLives = enemyScript.getLivesWorth();

        if (health - deductLives <= 0)
        {
            health = 0;
            Debug.Log("Game Over!");
            SceneManager.LoadScene(3); // Loads Death screen
        }
        else
        {
            health -= deductLives;
        }
    }

    public void addCoins(int amount)
    {
        coins += amount;
    }

    public bool canPlaceTower(int cost)
    {
        return coins >= cost;
    }
    
    public void deductCoins(int amount)
    {
        coins -= amount;
    }


    public void ShowTowerInfo(TowerScript tower)
    {
        // Activate the upgrade panel
        upgradePanel.SetActive(true);
        towerNameObject.SetActive(true);
        damageDealtObject.SetActive(true);

        nameOfTower.text = tower.towerName;
        nameOfTower.transform.position = tower.transform.position + new Vector3(0.5f, 2f, 0);
        DDTower.text = "Δ" + tower.towerDamage.ToString();
        DDTower.transform.position = tower.transform.position + new Vector3(3f, 0, 0);
    }

    public void SetGameSpeed(float speed)
    {
        currentSpeed = speed;
        Time.timeScale = speed;
        Debug.Log("Game speed set to: " + speed + "x");
    }

     public void CycleGameSpeed()
    {
        if (!isPaused){
            if (currentSpeed == normalSpeed)
            {
                SetGameSpeed(fastSpeed);
                SpeedControlButton.GetComponentInChildren<TextMeshProUGUI>().text = "2x";
            }
            else if (currentSpeed == fastSpeed)
            {
                SetGameSpeed(ultraSpeed);
                SpeedControlButton.GetComponentInChildren<TextMeshProUGUI>().text = "3x";
            }
            else
            {
                SetGameSpeed(normalSpeed);
                SpeedControlButton.GetComponentInChildren<TextMeshProUGUI>().text = "1x";
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
        Time.timeScale = currentSpeed; // Resume time scale to current speed
        isPaused = false; // Unpause the game
        pauseMenuUI.SetActive(false); // Hide pause menu UI
    }

    public void RestartButton()
    {
        Time.timeScale = 1f; // Reset time scale to normal
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex); // Reload current scene
        isPaused = false; // Unpause the game
        pauseMenuUI.SetActive(false); // Hide pause menu UI
    }

    public void OpenArcana()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(4); // Load arcana scene
    }



}

