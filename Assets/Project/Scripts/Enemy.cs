using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int ID;
    public float MaxHealth;
    public float Health;
    public float Speed;
    [SerializeField] private int livesWorth; // how much lives player loses if this enemy passes end node
    public string Element; //Could Change this to an ElementID Integer

    public GameManager gameManager;
    Transform canvas;
    
    public void Start(){
        canvas = transform.Find("Canvas");
        gameManager = GameManager.Instance;
        
        canvas.GetComponent<EnemyHealthText>().setHealthText(Health);
    }

    public int getLivesWorth()
    {
        return livesWorth;
    }

    public void init()
    {
        Health = MaxHealth;

    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        canvas.GetComponent<EnemyHealthText>().setHealthText(Health);
        if (Health <=0)
        {
            if (EnemySummoner.ExistingEnemies.Contains(this))
            {
                EnemySummoner.ExistingEnemies.Remove(this);
            }
            Destroy(this.gameObject);
            gameManager.addCoins(10);
        } 
    }
    

}
