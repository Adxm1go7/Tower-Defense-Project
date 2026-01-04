//TowerScript
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
    protected Enemy currentEnemy;
    public float timeForNextAttack;
    public GameManager gameManager; // Controls the flow of the game
    public TowerUpgrades towerUppies;

    public int enemyLayer;

    private int blockedContacts = 0;
        
    protected virtual void Start() // Can be overriden
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

        enemyLayer = 1 << LayerMask.NameToLayer("EnemyLayer");
        Debug.Log(enemyLayer);
    }

    void Awake()
    {
        if (towerUppies == null)
        {
            towerUppies = GetComponent<TowerUpgrades>();
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        timeForNextAttack -= Time.deltaTime;
        FindTarget();
        if (currentEnemy != null){
            AttackTiming();
        }

    }

    protected virtual void AttackTiming(){
        if (timeForNextAttack <= 0f)
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


    protected virtual void FindTarget()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, towerRange, enemyLayer);
        Enemy furthestEnemy = null;
        float bestProgress = 0f;

        foreach (Collider enemyInRange in enemiesInRange)
        {
            Enemy enemy = enemyInRange.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                if (enemy.GetComponentInParent<EnemyMovement>().getEnemyMovementProgress() > bestProgress)
                {
                    furthestEnemy = enemy;
                    bestProgress = enemy.GetComponentInParent<EnemyMovement>().getEnemyMovementProgress();
                }
            }
        }
        if (furthestEnemy != null)
        {
            currentEnemy = furthestEnemy;
        }
    }
    
    protected virtual void LookAtEnemies()
    {
        Vector3 direction = currentEnemy.transform.position - transform.position;
        direction.y = 0f;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation *= Quaternion.Euler(0, 180f, 0);
        transform.rotation = lookRotation;

    }

    protected virtual void AttackEnemies()
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
