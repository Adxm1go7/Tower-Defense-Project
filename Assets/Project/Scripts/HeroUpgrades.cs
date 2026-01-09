using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroUpgrade", menuName = "Hero/Hero Upgrade")]
public class HeroUpgrades : ScriptableObject
{
    // Start is called before the first frame update
    public int cost;
    public int singleDamageInc;
    public int areaDamageInc;
    public float singleRateDec;
    public float areaRateDec;
    public float aoeRadiusInc;
}
