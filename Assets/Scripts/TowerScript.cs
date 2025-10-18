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
        
    void Start()
    {
        // Initialize tower properties
        towerID = 1;
        towerName = "Basic Tower";
        towerCost = 50;
        towerRange = 5.0f;
        towerDamage = 10;
        towerFireRate = 1.0f;
        sellValue = (int)(towerCost * 0.75f);
        elementType = "Neutral"; 
    }

    // Update is called once per frame
    void Update()
    {
        // Tower behavior logic (e.g., attacking enemies) would go here
        AttackEnemies();
    }

    void AttackEnemies()
    {
        // Implement enemy detection and attack logic


    }
}
