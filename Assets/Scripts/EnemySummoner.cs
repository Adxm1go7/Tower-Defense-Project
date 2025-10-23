using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySummoner : MonoBehaviour
{
    public static List<Enemy> ExistingEnemies; //List of spawned, alive enemies
    public static Dictionary<int, GameObject> EnemyPrefabs; //Components of EnemyData class, int is ID 
    public static Dictionary<int, Queue<Enemy>> EnemyObjectPools; //Multiple enemy types need multiple queues

    public int enemyIDToSpawn = 0; // Which enemy ID to spawn
    public float spawnInterval = 2f; // How often to spawn (seconds)
    private float spawnTimer = 0f;
    void Start()
    {
        EnemyPrefabs = new Dictionary<int, GameObject>();
        EnemyObjectPools = new Dictionary<int, Queue<Enemy>>();
        ExistingEnemies = new List<Enemy>();

        EnemyData[] Enemies = Resources.LoadAll<EnemyData>("Enemies"); //Going through all directories until it reaches resource folders and puts a "\"

        foreach (EnemyData enemy in Enemies)
        {
            EnemyPrefabs.Add(enemy.EnemyID, enemy.EnemyPrefab);
            EnemyObjectPools.Add(enemy.EnemyID, new Queue<Enemy>());
        }
    }

    public static Enemy SummonEnemy(int EnemyID)
    {
        Enemy SummonedEnemy = null;

        if (EnemyPrefabs.ContainsKey(EnemyID)) //Check if enemyID exists by checking if prefab is stored with its ID in the prefabDictionary
        {
            Queue<Enemy> ReferencedQueue = EnemyObjectPools[EnemyID];

            if (ReferencedQueue.Count > 0)
            {
                //Dequeue Enemy and init

                SummonedEnemy = ReferencedQueue.Dequeue();
                SummonedEnemy.init();
            }
            else
            {
                //Instantiate new instance of enemy and init
                GameObject NewEnemy = Instantiate(EnemyPrefabs[EnemyID], new Vector3((float)-9.5, (float)1.38, (float)9.03), Quaternion.identity);
                SummonedEnemy.init();
            }

            ExistingEnemies.Add(SummonedEnemy);
        }
        else
        {
            Debug.Log($"Enemy with ID of {EnemyID} Does not exist");
            return null;
        }

        return SummonedEnemy;
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;

            // Call SummonEnemy to spawn one enemy
            SummonEnemy(enemyIDToSpawn);
        }
    }


}
