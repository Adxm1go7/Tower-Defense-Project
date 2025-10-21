using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{

    public static Transform[] waypointsArray; 

    void Awake()
    {
        waypointsArray = new Transform[transform.childCount];
        for (int i = 0; i < waypointsArray.Length; i ++)
        {
            waypointsArray[i] = transform.GetChild(i);
        }
    }
}
