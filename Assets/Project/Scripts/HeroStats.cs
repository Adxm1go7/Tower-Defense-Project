// Scriptable object to store enemy stats

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroStats", menuName = "Hero/HeroStats")]
public class HeroStats : ScriptableObject
{
    public int areaAttackDamage;
    public float areaAttackRadius;
    public float areaAttackRate;

    public int singleAttackDamage;
    public float singleAttackRadius;
    public float singleAttackRate;
}
