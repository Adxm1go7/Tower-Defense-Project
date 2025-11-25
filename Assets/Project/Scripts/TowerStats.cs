//SCRIPTABLE OBJECT BEING USED FOR REUSABLE TOWER TEMPLATES WITHOUT EDITING THE TOWERSCRIPT.CS


using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "TowerStats", menuName = "Tower/Tower Stats")]
public class TowerStats : ScriptableObject
{
    //SCRIPTABLE OBJECT TO STORE TOWER ATTRIBUTES

    public int towerID;
    public string towerName;
    public float towerRange;
    public int towerDamage;
    public float towerFireRate;
    public int towerCost;
    public string elementType;


}
