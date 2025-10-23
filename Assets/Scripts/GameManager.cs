using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int health;
    private int numRounds;
    private int currentRound;
    private int coins;
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI RoundText;
    public GameObject Enemy;
    public GameObject EnemyEndNode;
    public static GameManager Instance;
    

    // Start is called before the first frame update
    void Start()
    {
        health = 250;
        numRounds = 50;
        currentRound = 0;
        coins = 250;
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

    public void enemySuccess()
    {
        Destroy(Enemy);
        if (health > 0)
        {
            if (health - 10 <= 0)
            {
                health = 0;
                Debug.Log("Game Over!");
            }
            else
            {
                health -= 1;
            }
        }
    }

    public void addCoins(int amount)
    {
        coins += amount;
    }

    public bool canPlaceTower(int cost)
    {
        Debug.Log("Checking if can place tower with cost: " + cost + " Current coins: " + coins);
        return coins >= cost;
    }
    
    public void deductCoins(int amount)
    {
        coins -= amount;
    }


}

