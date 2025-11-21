using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EnemySummoner.SummonEnemy(1);
        Debug.Log("TEST");
    }


}
