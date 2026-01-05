using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Flame Thrower Tower Stats", menuName = "Tower/Flame Thrower Tower Stats")]
public class FlameThrowerTowerStats : ScriptableObject
{
    public float burstInterval; // Wait times between attack
    public float burstDuration; // Time of burst attack
    public float coneAngle; // Angle of attack when facing furthest enemy
    public float flameTickLength; // How often TowerDamage is applied during attack

    // Burn effects
    public float flameBurnTickDuration;
    public float flameBurnTickInterval;
    public int flameBurnDamagePerTick;
}
