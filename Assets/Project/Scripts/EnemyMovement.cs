using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Enemy EnemyScript;

    private float speed;

    private Transform target;
    private int waypointIndex;
    private float distanceToNextWaypoint;
    private float enemyMovementProgress; // A value showing how close enemies are to endNode

    void Awake()
    {
        EnemyScript = GetComponent<Enemy>();
        waypointIndex = 1;
    }

    void Start()
    {
        target = Waypoints.waypointsArray[waypointIndex];
        speed = EnemyScript.getSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        distanceToNextWaypoint = Vector3.Distance(transform.position, target.position);
        if (distanceToNextWaypoint <= 0.3f)
        {
            GetNextWaypoint();
        }
        enemyMovementProgress = //Tracks progress of enemy as a float value
        waypointIndex + (1-(distanceToNextWaypoint / Vector3.Distance(Waypoints.waypointsArray[waypointIndex - 1].position, target.position)));
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.waypointsArray.Length - 1)
        {
            Destroy(gameObject);
        }
        else
        {
            waypointIndex++;
            target = Waypoints.waypointsArray[waypointIndex];
        }
    }

    public float getEnemyMovementProgress()
    {
        return enemyMovementProgress;
    }

    public int getWaypointIndex(){
        return waypointIndex;
    }

    public void setWaypointIndex(int i){
        waypointIndex = i;
        target = Waypoints.waypointsArray[waypointIndex];
    }
}
