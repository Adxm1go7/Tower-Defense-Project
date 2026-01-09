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

    private int level1Scene = 8;
    private int level2Scene = 6;
    private int level3Scene = 7;
    private int level4Scene = 9;


    public Light Level1Light;
    public Light Level2Light;
    public Light Level3Light;
    public Light Level4Light;

    public GameObject LoadingScreenOverlay;
    public bool loadScene = false;

    void Start()
    {
        waypointsArray = Waypoints.waypointsArray;
        currentLevelIndex = 0;
        movingPlayer = false;
        loadScene = false;
        LoadingScreenOverlay.SetActive(false);
    }

    void Update()
    {
        if (movingPlayer == false && loadScene == false)
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
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
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
        if (loadScene) return;

        Vector3 pos = transform.position;
        Vector3 targetPos = target.position;

        pos.y = 0f;
        targetPos.y = 0f;

        Vector3 toTarget = targetPos - pos;
        float sqrDistance = toTarget.sqrMagnitude;

        // Move
        Vector3 move = toTarget.normalized * moveSpeed * Time.deltaTime;

        // Clamp so we never overshoot
        if (move.sqrMagnitude >= sqrDistance)
        {
            transform.position = target.position;
            movingPlayer = false;
            return;
        }

        transform.position += move;
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
        loadScene = true;
        LoadingScreenOverlay.SetActive(true);
        if (currentLevelIndex == 0) 
        {
            LoadingScreenOverlay.GetComponent<LoadingScene>().LoadScene(level1Scene);
        }
        else if(currentLevelIndex == 1)
        {
            LoadingScreenOverlay.GetComponent<LoadingScene>().LoadScene(level2Scene);
        }else if (currentLevelIndex == 2)
        {
            LoadingScreenOverlay.GetComponent<LoadingScene>().LoadScene(level3Scene);
        }else if(currentLevelIndex == 3)
        {
            LoadingScreenOverlay.GetComponent<LoadingScene>().LoadScene(level4Scene);
        }
    } 
}
