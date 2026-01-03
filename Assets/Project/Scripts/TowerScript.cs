using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerScript : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    //WE CALL THE TOWERSTATS SCRIPTABLE OBJECT 
    public TowerStats towerStats;
    public int towerID;
    public string towerName;
    public float towerRange;
    public int towerDamage;
    public float towerFireRate;
    public int towerCost;
    public int sellValue;
    public string elementType;
    public bool activeTower; //Used by DragDropTower to disable tower attack functionality during placement
    private Enemy currentEnemy;
    public float timeForNextAttack;
    public GameManager gameManager; // Controls the flow of the game
    public TowerUpgrades towerUppies;

    private int blockedContacts = 0;
        
    void Start()
    {
        // INITIALISE THE TOWER ATTRIBUTES FROM THE TOWERSTATS SCRIPTABLE OBJECT
        // UNIQUE FOR EACH TOWER

        towerID = towerStats.towerID;
        towerName = towerStats.towerName;
        towerRange = towerStats.towerRange;
        towerDamage = towerStats.towerDamage;
        towerFireRate = towerStats.towerFireRate;
        towerCost = towerStats.towerCost;
        elementType = towerStats.elementType;
        sellValue = (int)(towerCost * 0.7f);
        timeForNextAttack = 0f;
        gameManager = GameManager.Instance; 
    }

    void Awake()
    {
        if (towerUppies == null)
        {
            towerUppies = GetComponent<TowerUpgrades>();
        }
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


    // METHOD TO CHANGE THE ATTRIBUTES FOR UPGRADES 
    void setAttributes()
    {
        

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
            if (distanceToEnemy < smallest && distanceToEnemy <= towerRange && enemy.getHealth() > 0)
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
            if (distanceToEnemy < smallest && distanceToEnemy <= towerRange && enemy.getHealth() > 0)
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
        if (currentEnemy != null && activeTower==true) //Only attack if tower is active
        {
            currentEnemy.TakeDamage(towerDamage);

        }
        Debug.Log("HIT HIT HIR");

    }

    //Implementing a click event to show tower info to user.
    public void OnPointerClick(PointerEventData eventData)
    {
        // Passes the tower object that was clicked to the GameManager to show its info
        GameManager.Instance.ShowTowerInfo(this);
        UpgradePanelController.Instance.

        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the tower interacts the EnemyPath or another Tower
        if (other.CompareTag("EnemyPath")|| other.CompareTag("Tower"))
        {
            // If it does then increment blockedContacts by 1
            blockedContacts++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EnemyPath") || other.CompareTag("Tower"))
        {
            // If it exits the EnemyPath or another Tower then decrement blockedContacts by 1
            blockedContacts--;
        }
    }

    public bool CanBePlaced()
    {
        // If the tower is not colliding with the EnemyPath or another Tower then it can be placed
        return blockedContacts == 0;
    }
}
