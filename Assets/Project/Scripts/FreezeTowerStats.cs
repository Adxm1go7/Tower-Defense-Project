using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Freeze Tower Stats", menuName = "Tower/Freeze Tower Stats")]
public class FreezeTowerStats : ScriptableObject
{
    public float coneAngle; // Angle of attack when facing furthest enemy

    public float slowDownMult; // Multiplier applied to enemies' speed
    public float slowDuration;
}
