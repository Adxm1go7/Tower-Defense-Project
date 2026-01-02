using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private WavesSequence levelData;
    
    private float timer;

    private int currentWaveIndex;
    private int currentWaveChunkIndex;
    private int currentEnemyIndex;

    private Wave waveData;
    private WaveChunk waveChunkData;

    private Vector3 spawnVector;

    bool levelPaused;

    // Start is called before the first frame update
    void Start()
    {
        levelPaused = false;

        currentWaveIndex = 0;
        currentWaveChunkIndex = 0;
        currentEnemyIndex = 0;

        waveData = levelData.waves[0];
        waveChunkData = waveData.waveChunks[0];

        timer = waveChunkData.spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (!levelPaused){
            if (timer <= 0){
                SpawnNext();
            }
            timer -= Time.deltaTime; 
        }
    }

    void SpawnNext(){ // Spawns next enemy
        spawnVector = EnemySummoner.getSpawnPoint(); // Gets Enemies spawn (sometimes enemies spawn from other enemies)
        EnemySummoner.SummonEnemy(waveChunkData.enemy.ID, spawnVector); // Spawns enemy
        currentEnemyIndex ++;

        timer = waveChunkData.spawnInterval;

        if (currentEnemyIndex >= waveChunkData.enemyCount){  //Moves to next wave chunk
            NextWaveChunk();
            currentEnemyIndex = 0;
        }
    }

    void NextWaveChunk(){ // Switches to next wave chunk
        currentWaveChunkIndex++;
        if (currentWaveChunkIndex >= waveData.waveChunks.Count){ // Check if next wave
            NextWave();
            currentWaveChunkIndex = 0;
        }

        waveChunkData = waveData.waveChunks[currentWaveChunkIndex];
    }

    void NextWave(){ // Switches to next wave
        currentWaveIndex ++;
        GameManager.Instance.setCurrentRound(currentWaveIndex+1);
        if (currentWaveIndex >= levelData.waves.Count){
            Debug.Log("Level Completed");
            levelPaused = true;
        }else{
            waveData = levelData.waves[currentWaveIndex];
            timer = waveData.TimeBeforeNextWave;
        }
    }

}
