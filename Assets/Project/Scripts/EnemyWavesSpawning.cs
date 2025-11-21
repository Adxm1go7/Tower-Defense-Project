using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class TestSpawnEnemy : MonoBehaviour
{
    private int waveNumber;
    private float spawnTimer;
    
    private bool cycleFinished;
    private List<float[]> enemyCycle = new List<float[]>(); //List contains --> [[numberOfEnemy, enemyType, intervalBetweenEnemies], ...]

    private int enemyNumber; // spawned enemy number
    private int enemyCycleIndex;

    void Awake()
    {
        Wave1();
        resetIndexes();
        cycleFinished = false;
    }

    void Start()
    {
        waveNumber = 1;
    }

    void Update()
    {
        if (cycleFinished == false) { 
            spawnTimer += Time.deltaTime;


            if (spawnTimer >= enemyCycle[enemyCycleIndex][2])
            {
                spawnTimer = 0f;
                // Call SummonEnemy to spawn one enemy
                EnemySummoner.SummonEnemy((int)enemyCycle[enemyCycleIndex][1]);
                enemyNumber++;

                if (enemyNumber >= enemyCycle[enemyCycleIndex][0]) // Check if current enemy spawn cycle is reached
                {
                    enemyCycleIndex++;
                    enemyNumber = 0;
                }

                if (enemyCycleIndex >= enemyCycle.Count) // Checks when to begin new wave
                {
                    cycleFinished = true;
                }
            }
        }
    }


    private void resetIndexes() {
        enemyNumber = 0;
        enemyCycleIndex = 0;
    }

    private void Wave1() {
        resetIndexes();
        enemyCycle = new List<float[]>
        {
            new float[] {3f, 0f, 2f}, // {number of enemies, type of enemy, interval between spawns}
            new float[] {6f, 1f, 1f},
            new float[] {2f, 2f, 3f},
        };
    }


}
