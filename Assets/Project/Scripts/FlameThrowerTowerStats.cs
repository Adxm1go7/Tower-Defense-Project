using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Flame Thrower Tower Stats", menuName = "Tower/Flame Thrower Tower Stats")]
public class FlameThrowerTowerStats : ScriptableObject
{
    public float burstInterval;
    public float burstDuration;
    public float coneAngle;
    public float flameDamagePerTick;
    public float flameTickLength;
}
