using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes : MonoBehaviour
{

    public static Transform[] nodesArray; 

    void Awake()
    {
        nodesArray = new Transform[transform.childCount];
        for (int i = 0; i < nodesArray.Length; i ++)
        {
            nodesArray[i] = transform.GetChild(i);
        }
    }
}
