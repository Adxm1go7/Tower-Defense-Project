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


    public GameObject SpeedControlButton; // Reference to the speed control button
    public float normalSpeed = 1f;
    public float fastSpeed = 2f;
    public float ultraFastSpeed = 3f;
    public float currentSpeed = 1f;

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
    }
}
