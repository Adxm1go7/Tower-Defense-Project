// Scriptable object to store enemy stats

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyStats", menuName = "Difficulty/DifficultyStats")]
public class DifficultyStats : ScriptableObject
{
    public int ID;

    public string Name;
    public float HealthMultiplier;
    public float SpeedMultiplier;
}