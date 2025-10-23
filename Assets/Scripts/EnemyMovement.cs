using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float speed = 8f;

    private Transform target;
    private int wavepointIndex = 0;

    void Start(){
        target = Waypoints.waypointsArray[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.3f){
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint(){
        if (wavepointIndex >= Waypoints.waypointsArray.Length - 1){
            Destroy(gameObject);
        }
        wavepointIndex++;
        target = Waypoints.waypointsArray[wavepointIndex];
    }
}
