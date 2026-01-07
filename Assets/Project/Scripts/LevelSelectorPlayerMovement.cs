using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectorPlayerMovement : MonoBehaviour
{
    private Transform target;
    private int currentLevelIndex;
    private Transform[] waypointsArray;
    private bool movingPlayer;
    private Vector3 direction;

    private float moveSpeed = 6f;

    private int level1Scene = 1;

    void Start()
    {
        waypointsArray = Waypoints.waypointsArray;
        currentLevelIndex = 0;
        movingPlayer = false;
    }

    void Update()
    {
        if (movingPlayer == false)
        {
            CheckForMove();
        }
        else
        {
            movePlayer();
        }
    }

    void CheckForMove()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentLevelIndex < waypointsArray.Length - 1)
            {
                currentLevelIndex++;
                target = waypointsArray[currentLevelIndex];
                movingPlayer = true;
                direction = target.position - transform.position;
                direction.y = 0f;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentLevelIndex > 0)
            {
                currentLevelIndex--;
                target = waypointsArray[currentLevelIndex];
                movingPlayer = true;
                direction = target.position - transform.position;
                direction.y = 0f;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            if (currentLevelIndex == 0) {
                SceneManager.LoadScene(7);
            }
        }
    }
    void movePlayer()
    {
        
        transform.Translate(direction.normalized * moveSpeed * Time.deltaTime, Space.World);
        Vector3 playerPosition = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 nodePosition = new Vector3(target.position.x, 0f, target.position.z);
        if (Vector3.Distance(playerPosition, nodePosition) <= 0.05f)
        {
            movingPlayer = false;
        }
    }  
}
