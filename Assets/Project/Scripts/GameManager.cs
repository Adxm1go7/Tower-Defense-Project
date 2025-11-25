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


}

