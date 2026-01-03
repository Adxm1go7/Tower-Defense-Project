using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int numRounds;
    public int currentRound;
    public TextMeshProUGUI RoundText;
    public GameObject EnemyEndNode;



    public Enemy enemyScript;

    public GameObject upgradePanel; // Reference to the upgrade panel UI

    public GameObject towerNameObject; // Reference to the tower name UI object

    public GameObject damageDealtObject; // Reference to the damage dealt UI object 
    public TextMeshPro nameOfTower; //Name of tower text object
    public TextMeshPro DDTower; //Damage dealt by tower text object

    public GameObject SpeedControlButton; // Reference to the speed control button
    public float normalSpeed;
    public float fastSpeed;
    public float ultraFastSpeed;
    public float currentSpeed;

    public DifficultyStats currentDifficulty;

    public GameObject pauseMenuUI; // Reference to the pause menu UI
    public bool isPaused = false;



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
        towerNameObject = levelManager.towerNameObject;
        damageDealtObject = levelManager.damageDealtObject;
        nameOfTower = levelManager.nameOfTower;
        DDTower = levelManager.DDTower;
        SpeedControlButton = levelManager.SpeedControlButton;
        normalSpeed = levelManager.normalSpeed;
        fastSpeed = levelManager.fastSpeed;
        ultraFastSpeed = levelManager.ultraFastSpeed;
        currentSpeed = levelManager.currentSpeed;

        setCurrentRound(1);
    }



    public void setCurrentRound(int round) // sets UI round number
    {
        currentRound = round;
        RoundText.text = "Rounds : " + round.ToString() + " / " + numRounds.ToString();
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
        currentDifficulty = difficulty;
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

        UpgradePanelController UpgradeController = upgradePanel.GetComponent<UpgradePanelController>();
        UpgradeController.SetSelectedTower(tower);
    }

    public int getCoins()
    {
        return levelManager.coins;
    }



}

