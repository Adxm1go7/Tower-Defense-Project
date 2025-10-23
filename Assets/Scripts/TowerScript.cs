using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    // Start is called before the first frame update

    public int towerID;
    public string towerName;
    public float towerRange;
    public int towerDamage;
    public float towerFireRate;
    public int towerCost;
    public int sellValue;
    public string elementType;
    private Enemy currentEnemy;
    public float timeForNextAttack;
    public GameManager gameManager;
        
    void Start()
    {
        // Initialize tower properties
        // i think we manually change this if its the first time we making the tower
        towerID = 1;
        towerName = "Basic Tower";
        towerCost = 50;
        towerRange = 5.0f;
        towerDamage = 1;
        towerFireRate = 0.10f;
        sellValue = (int)(towerCost * 0.75f);
        elementType = "Neutral";
        timeForNextAttack = 0f;
        gameManager = GameManager.Instance; 
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentEnemy);
        timeForNextAttack -= Time.deltaTime;
        if (currentEnemy == null || Vector3.Distance(transform.position, currentEnemy.transform.position) > towerRange)
        {

            FindTarget();
        }

        if (currentEnemy != null && timeForNextAttack <= 0)
        {
            AttackEnemies();
            LookAtEnemies();
            timeForNextAttack = towerFireRate;
        }

    }

    
    void FindTargetTest()
    {
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        if (allEnemies.Length == 0)
        {
            return;
        }

        Enemy nearest = null;
        float smallest = Mathf.Infinity;

        foreach (Enemy enemy in allEnemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < smallest && distanceToEnemy <= towerRange && enemy.Health > 0)
            {
                smallest = distanceToEnemy;
                nearest = enemy;
            }
        }

        currentEnemy = nearest;

    }

    void FindTarget()
    {

        if (EnemySummoner.ExistingEnemies == null || EnemySummoner.ExistingEnemies.Count == 0)
        {
            FindTargetTest();
            return;
        }

        EnemySummoner.ExistingEnemies.RemoveAll(e => e == null);
        Enemy nearest = null;
        float smallest = Mathf.Infinity;

        // Debug.Log($"Enemies in scene: {EnemySummoner.ExistingEnemies.Count}");
        foreach (Enemy enemy in EnemySummoner.ExistingEnemies)
        {

            if (enemy == null)
            {
                continue;
            }
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < smallest && distanceToEnemy <= towerRange && enemy.Health > 0)
            {
                smallest = distanceToEnemy;
                nearest = enemy;
            }

        }
        currentEnemy = nearest;
        Debug.Log($"Enemies in scene: {EnemySummoner.ExistingEnemies.Count}");

    }
    
    void LookAtEnemies()
    {
        Vector3 direction = currentEnemy.transform.position - transform.position;
        direction.y = 0f;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation *= Quaternion.Euler(0, 180f, 0);
        transform.rotation = lookRotation;

    }

    void AttackEnemies()
    {
        if (currentEnemy != null)
        {
            currentEnemy.TakeDamage(towerDamage);

        }
        Debug.Log("HIT HIT HIR");

    }


}
