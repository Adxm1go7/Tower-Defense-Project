// Scriptable object to store parts of enemy wave

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveChunk", menuName = "WaveData/WaveChunk")]
public class WaveChunk : ScriptableObject
{
    public EnemyStats enemy;
    public int enemyCount;
    public float spawnInterval;
}