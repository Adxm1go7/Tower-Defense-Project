// Scriptable object to store enemy stats

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Enemy/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public GameObject EnemyPrefab;
    public int ID;

    public float MaxHealth;
    public int LivesWorth;
    public float Speed;
    
    public float RegenHealth;
    public float RegenRate;
    public float RegenTime;

    public int SplitCount;
    public Enemy SplitEnemy;
    public int CoinsWorth;

    public float DodgeChance;
}
