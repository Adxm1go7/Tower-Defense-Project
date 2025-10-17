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
    

    // Start is called before the first frame update
    void Start()
    {
        health = 250;
        numRounds = 50;
        currentRound = 0;
        coins = 250;
        setCurrentRound();

    }

    // Update is called once per frame
    void Update()
    {
        HealthText.text = "Health : " + health.ToString();
        CoinText.text = "Coins : " + coins.ToString();
    }

    public void setCurrentRound()
    {
        RoundText.text = "Rounds : " + currentRound.ToString() + " / " + numRounds.ToString();
    }
}
