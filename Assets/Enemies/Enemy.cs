using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int ID;
    public float MaxHealth;
    public float Health;
    public float Speed;
    public string Element; //Could Change this to an ElementID Integer

    public GameManager gameManager;
    Transform canvas;
    
    public void Start(){
        canvas = transform.Find("Canvas");
        gameManager = GameManager.Instance;
        
        canvas.GetComponent<EnemyHealthText>().setHealthText(Health);
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
