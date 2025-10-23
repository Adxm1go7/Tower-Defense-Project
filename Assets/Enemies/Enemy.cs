using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int NodeIndex;
    public int ID;
    public float MaxHealth;
    public float Health;
    public float Speed;
    public string Element; //Could Change this to an ElementID Integer


    public void init()
    {
        Health = MaxHealth;
        NodeIndex = 0;

        if (Nodes.nodesArray.Length > 0)
        {
            transform.position = Nodes.nodesArray[0].position;
            if (Nodes.nodesArray.Length > 1)
                targetNode = Nodes.nodesArray[1];

        }
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
