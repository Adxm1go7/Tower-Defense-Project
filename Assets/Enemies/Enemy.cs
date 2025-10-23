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


    public void init()
    {
        Health = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <=0)
        {
            Destroy(this.gameObject);
        } 
    }
    

}
