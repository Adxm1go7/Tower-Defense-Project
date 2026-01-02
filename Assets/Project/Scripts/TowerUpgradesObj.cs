using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerUpgradesObj", menuName = "Tower/Upgrades")]
public class TowerUpgradesObj : ScriptableObject
{
    public string name;
    public int cost;
    public int damageInc;
    
    public float rangeInc;
    //0.9f is faster than 1.1f
    public float fireRate; 

    public string description;

}
