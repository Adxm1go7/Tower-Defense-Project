using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySummoner : MonoBehaviour
{
    //new Vector3((float)-9.5, (float)1.38, (float)9.03)
    private static Vector3 spawnPoint;

    public static List<Enemy> ExistingEnemies; //List of spawned, alive enemies
    public static Dictionary<int, GameObject> EnemyPrefabs; //Components of EnemyData class, int is ID 
    public static Dictionary<int, Queue<Enemy>> EnemyObjectPools; //Multiple enemy types need multiple queues

    public int enemyIDToSpawn = 0; // Which enemy ID to spawn
    public float spawnInterval = 2f; // How often to spawn (seconds)
    private float spawnTimer = 0f;
    void Awake()
    {
        EnemyPrefabs = new Dictionary<int, GameObject>();
        EnemyObjectPools = new Dictionary<int, Queue<Enemy>>();
        ExistingEnemies = new List<Enemy>();

        spawnPoint = transform.position;

        EnemyStats[] Enemies = Resources.LoadAll<EnemyStats>("EnemyScriptableObjects"); //Going through all directories until it reaches resource folders and puts a "\"

        foreach (EnemyStats enemy in Enemies)
        {
            EnemyPrefabs.Add(enemy.ID, enemy.EnemyPrefab);
            EnemyObjectPools.Add(enemy.ID, new Queue<Enemy>());
        }
    }

    public static Enemy SummonEnemy(int EnemyID, Vector3 spawnPoint)
    {
        Enemy SummonedEnemy = null;
        
        if(EnemyPrefabs.ContainsKey(EnemyID)) //Check if enemyID exists by checking if prefab is stored with its ID in the prefabDictionary
        {
            Queue<Enemy> ReferencedQueue = EnemyObjectPools[EnemyID];

            if (ReferencedQueue.Count > 0)
            {
                //Dequeue Enemy and init

                SummonedEnemy = ReferencedQueue.Dequeue();
            }
            else
            {
                //Instantiate new instance of enemy and init
                GameObject NewEnemy = Instantiate(EnemyPrefabs[EnemyID], spawnPoint, Quaternion.identity);
                SummonedEnemy = NewEnemy.GetComponent<Enemy>();
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

    public static Vector3 getSpawnPoint(){
        return spawnPoint;
    }

    /*
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
    */
}
