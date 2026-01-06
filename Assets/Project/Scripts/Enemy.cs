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
    EnemyStats SplitEnemy; // Enemy that comes out 
    private int CoinsWorth;
    private float DodgeChance;

    private string Element; //Could Change this to an ElementID Integer

    private float aliveTimer;
    private float lastDamageTime; // Alive time at which enemy last took damage
    private float lastRegenTime; // Alive time at which enemy regenerated last
    Transform canvas;

    private Coroutine burnCoroutine;
    private float burnEndTime;

    private Coroutine freezeCoroutine;
    private float freezeEndTime;


    public EnemyMovement movement;

    private void Awake(){
        movement = GetComponent<EnemyMovement>();
    }
    
    public void Start(){
        ID = enemyStats.ID;
        MaxHealth = enemyStats.MaxHealth * GameManager.Instance.currentDifficulty.HealthMultiplier; // Difficulty multiplier
        MaxHealth = Mathf.Round(MaxHealth); // Round to nearest INT
        Speed = enemyStats.Speed * GameManager.Instance.currentDifficulty.SpeedMultiplier; // Difficulty multiplier
        LivesWorth = enemyStats.LivesWorth;
        RegenHealth = enemyStats.RegenHealth;
        RegenRate = enemyStats.RegenRate;
        RegenTime = enemyStats.RegenTime;
        SplitCount = enemyStats.SplitCount;
        SplitEnemy = enemyStats.SplitEnemy;
        CoinsWorth = enemyStats.CoinsWorth;
        DodgeChance = enemyStats.DodgeChance;

        canvas = transform.Find("Canvas");
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
        if (DodgeChance > 0f){
            if (Random.Range(0f, 1f) <= DodgeChance){
                Debug.Log("Dodge");
                return;
            }
        }
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
            GameManager.Instance.addCoins(CoinsWorth);

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
            Enemy e = EnemySummoner.SummonEnemy(SplitEnemy.ID, transform.position);
            e.GetComponent<EnemyMovement>().setWaypointIndex(wIndex);
        }
    }

    public void ApplyBurn(int damagePerTick, float tickInterval, float duration){
        burnEndTime = aliveTimer + duration;
        if (burnCoroutine == null){
            burnCoroutine = StartCoroutine(Burn(damagePerTick, tickInterval));
        }
    }

    private IEnumerator Burn(int damagePerTick, float tickInterval){
        while (aliveTimer <=  burnEndTime){
            yield return new WaitForSeconds(tickInterval); // Pauses coroutine and returns after timeout
            TakeDamage(damagePerTick);
        }

        burnCoroutine = null;
    }

    public void ApplySlowDown(float slowDownMult, float duration){
        freezeEndTime = aliveTimer + duration;
        if (freezeCoroutine == null){
            freezeCoroutine = StartCoroutine(SlowDownEffect(slowDownMult));
        }
    }

    private IEnumerator SlowDownEffect(float slowDownMult){
        movement.slowDownMult = slowDownMult;
        while (aliveTimer <= freezeEndTime){
            yield return null;
        }
        movement.slowDownMult = 1f;
        freezeCoroutine = null;
    }

}
