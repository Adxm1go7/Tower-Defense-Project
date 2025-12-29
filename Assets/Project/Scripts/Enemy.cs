using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyStats enemyStats;

    private int ID;
    private float MaxHealth;
    private float Health;
    private float Speed;
    private int LivesWorth; // how much lives player loses if this enemy passes end node
    private float RegenHealth;
    private float RegenRate;
    private float RegenTime;
    private int SplitCount;
    Enemy SplitEnemy; // Enemy that comes out 
    private int CoinsWorth;

    private string Element; //Could Change this to an ElementID Integer

    private float aliveTimer;
    private float lastDamageTime; // Alive time at which enemy last took damage
    private float lastRegenTime; // Alive time at which enemy regenerated last
    public GameManager gameManager;
    Transform canvas;


    public EnemyMovement movement;

    private void Awake(){
        movement = GetComponent<EnemyMovement>();
    }
    
    public void Start(){
        ID = enemyStats.ID;
        MaxHealth = enemyStats.MaxHealth;
        Speed = enemyStats.Speed;
        LivesWorth = enemyStats.LivesWorth;
        RegenHealth = enemyStats.RegenHealth;
        RegenRate = enemyStats.RegenRate;
        RegenTime = enemyStats.RegenTime;
        SplitCount = enemyStats.SplitCount;
        SplitEnemy = enemyStats.SplitEnemy;
        CoinsWorth = enemyStats.CoinsWorth;

        canvas = transform.Find("Canvas");
        gameManager = GameManager.Instance;
        Health = MaxHealth;
        canvas.GetComponent<EnemyHealthText>().setHealthText(Health);
        aliveTimer = 0f;
        lastRegenTime = 0f;
    }

    public void Update()
    {
        aliveTimer += Time.deltaTime;
        if (RegenHealth > 0f){
            Regen();
        }
    }

    public int getLivesWorth()
    {
        return LivesWorth;
    }
    
    public float getHealth()
    {
        return Health;
    }

    public float getSpeed()
    {
        return Speed;
    }

    public int getID(){
        return ID;
    }

    public void TakeDamage(int damage)
    {

        Health -= damage;
        lastDamageTime = aliveTimer;
        canvas.GetComponent<EnemyHealthText>().setHealthText(Health);
        if (Health <=0)
        {
            if (EnemySummoner.ExistingEnemies.Contains(this))
            {
                EnemySummoner.ExistingEnemies.Remove(this);
            }
            if (SplitEnemy != null){
                SplitUponDeath();
            }
            gameManager.addCoins(CoinsWorth);

            Destroy(this.gameObject);
        } 
    }

    public void Regen(){
        if ((aliveTimer - lastRegenTime >= RegenRate) && (aliveTimer - lastDamageTime >= RegenTime)){
            Health = Mathf.Min(Health+RegenHealth, MaxHealth);
            canvas.GetComponent<EnemyHealthText>().setHealthText(Health);
            lastRegenTime = aliveTimer;
        }
    }

    public void SplitUponDeath(){
        int wIndex = movement.getWaypointIndex();
        for (int i = 0; i < SplitCount; i++)
        {
            Enemy e = EnemySummoner.SummonEnemy(SplitEnemy.getID(), transform.position);
            e.GetComponent<EnemyMovement>().setWaypointIndex(wIndex);
        }
    }
}
