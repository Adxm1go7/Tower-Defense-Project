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

    private float moveSpeed = 12f;

    private int level1Scene = 1;
    private int level2Scene = 7;

    public Light Level1Light;
    public Light Level2Light;
    public Light Level3Light;
    public Light Level4Light;

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

        if (currentLevelIndex == 0)
        {
            DisableLights();
            Level1Light.enabled = true;
        }
        else if (currentLevelIndex == 1)
        {
            DisableLights();
            Level2Light.enabled = true;
        }
        else if (currentLevelIndex == 2)
        {
            DisableLights();
            Level3Light.enabled = true;
        }
        else if (currentLevelIndex == 3)
        {
            DisableLights();
            Level4Light.enabled = true;
        }
    }

    void DisableLights()
    {
        Level1Light.enabled = false;
        Level2Light.enabled = false;
        Level3Light.enabled = false;
        Level4Light.enabled = false;
    }

    void CheckForMove()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            nextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            PreviousLevel();
        }
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            PlayGame();
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

    public void nextLevel()
    {
        Debug.Log("Next Level");
        if (currentLevelIndex < waypointsArray.Length - 1)
            {
                currentLevelIndex++;
                target = waypointsArray[currentLevelIndex];
                movingPlayer = true;
                direction = target.position - transform.position;
                direction.y = 0f;
            }
    }

    public void PreviousLevel()
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

    public void PlayGame()
    {
        if (currentLevelIndex == 0) {
            SceneManager.LoadScene(level1Scene);
        }
        else if(currentLevelIndex == 1){
                SceneManager.LoadScene(level2Scene);
        }
    } 
}
