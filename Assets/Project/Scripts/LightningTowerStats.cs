//SCRIPTABLE OBJECT BEING USED FOR THE LIGHTING TOWER


using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "LightningTowerStats", menuName = "Tower/Lightning Tower Stats")]
public class LightningTowerStats : ScriptableObject
{
    //SCRIPTABLE OBJECT TO STORE TOWER ATTRIBUTES
    public float chainRadius;
    public int maxChains;
    public int damageFallOff;
    public float chainInterval;
}
