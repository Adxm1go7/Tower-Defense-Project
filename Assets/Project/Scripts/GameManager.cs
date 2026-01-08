using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int numRounds;
    public int currentRound;
    public TextMeshProUGUI RoundText;
    public GameObject EnemyEndNode;

    public CameraController cameraController;

    public Enemy enemyScript;

    public GameObject upgradePanel; // Reference to the upgrade panel UI

    public TextMeshProUGUI nameOfTower; //Name of tower text object
    public TextMeshProUGUI DDTower; //Damage dealt by tower text object
    public TextMeshProUGUI SpecialAtk; //Special attack text object

    public Slider GameSpeedSlider; // Reference to the speed control button
    public float normalSpeed;
    public float fastSpeed;
    public float ultraFastSpeed;
    public float currentSpeed;

    public DifficultyStats currentDifficulty;

    public GameObject pauseMenuUI; // Reference to the pause menu UI
    public bool isPaused = false; // To track if the game is paused

    public int currentScene;
    public int previousScene;



    public LevelManager levelManager; // Contains information and references to everything within the level

    void Awake() // Singleton design pattern
    {
        // Prevent duplicates
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Stays across scenes
        DontDestroyOnLoad(gameObject);
    }

    public void StartLevel(){
        numRounds = levelManager.numRounds;
        currentRound = levelManager.currentRound;
        RoundText = levelManager.RoundText;
        EnemyEndNode = levelManager.EnemyEndNode;
        enemyScript = levelManager.enemyScript;
        upgradePanel = levelManager.upgradePanel;
        nameOfTower = levelManager.nameOfTower;
        DDTower = levelManager.DDTower;
        SpecialAtk = levelManager.SpecialAtk;
        GameSpeedSlider = levelManager.GameSpeedSlider;
        normalSpeed = levelManager.normalSpeed;
        fastSpeed = levelManager.fastSpeed;
        ultraFastSpeed = levelManager.ultraFastSpeed;
        currentSpeed = levelManager.currentSpeed;
        cameraController = levelManager.mainCamera.GetComponent<CameraController>();

        setCurrentRound(1);
    }



    public void setCurrentRound(int round) // sets UI round number
    {
        currentRound = round;
        RoundText.text = round.ToString() + " / " + numRounds.ToString();
    }

    public void enemySuccess(Collider enemyCollider)
    {

        enemyScript = enemyCollider.GetComponentInParent<Enemy>(); // Gets Enemy.cs from passed enemy
        int deductLives = enemyScript.getLivesWorth();

        if (levelManager.health - deductLives <= 0)
        {
            levelManager.health = 0;
            Debug.Log("Game Over!");
            SceneManager.LoadScene(3); // Loads Death screen
        }
        else
        {
            levelManager.health -= deductLives;
        }
    }

    

    public void addCoins(int amount)
    {
        levelManager.coins += amount;
    }

    public bool canPlaceTower(int cost)
    {
        return levelManager.coins >= cost;
    }
    
    public void deductCoins(int amount)
    {
        levelManager.coins -= amount;
    }

    public void SetDifficulty(DifficultyStats difficulty){ // Called from Home screen
        Debug.Log(difficulty.name + " Recieved at gameManager");
        currentDifficulty = difficulty;
    }

    public void ShowTowerInfo(TowerScript tower)
    {
        // Activate the upgrade panel

        levelManager.CloseSidePanel();

        Debug.Log("Showing info for tower: " + tower.towerName);

        cameraController.FocusOnTower(tower.transform);
        
        upgradePanel.SetActive(true);

        nameOfTower.text = tower.towerName;
        DDTower.text = "Damage: " + tower.towerDamage.ToString();
        SpecialAtk.text = "Special: ";

        UpgradePanelController UpgradeController = upgradePanel.GetComponent<UpgradePanelController>();
        UpgradeController.SetSelectedTower(tower);
    }

    public int getCoins()
    {
        return levelManager.coins;
    }



}

