using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData", menuName = "Create EnemyData")]
public class EnemyData : ScriptableObject
{
    public GameObject EnemyPrefab;
    public int EnemyID;
}
