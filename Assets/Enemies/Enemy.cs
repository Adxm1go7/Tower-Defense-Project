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
    Transform canvas;
    
    public void Start(){
        canvas = transform.Find("Canvas");
    }

    public void init()
    {
        Health = MaxHealth;
        canvas.GetComponent<EnemyHealthText>().setHealthText(Health);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        canvas.GetComponent<EnemyHealthText>().setHealthText(Health);
        if (Health <=0)
        {
            Destroy(this.gameObject);
        } 
    }
    

}
