using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int NodeIndex;
    public int ID;
    public float MaxHealth;
    public float Health;
    public float Speed;
    public string Element; //Could Change this to an ElementID Integer

    private Transform targetNode;


    public void init()
    {
        Health = MaxHealth;
        NodeIndex = 0;

        if (Nodes.nodesArray.Length > 0)
        {
            transform.position = Nodes.nodesArray[0].position;
            if (Nodes.nodesArray.Length > 1)
                targetNode = Nodes.nodesArray[1];

        }
    }

    void Update()
    {
        if (targetNode == null) return;
        nextNode();
    }

    void nextNode()
    {
        Vector3 dir = targetNode.position - transform.position;
        transform.Translate(dir.normalized * Speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, targetNode.position) < 0.2f)
        {
            GetNextNode();
        }
    }

    void GetNextNode()
    {
        if (NodeIndex < Nodes.nodesArray.Length - 2)
        {
            NodeIndex++;
            targetNode = Nodes.nodesArray[NodeIndex + 1];
        }
        else
        {
            lastNodeReached();
        }
    }

    private void lastNodeReached()

    {
        Debug.Log("Reached the end");
        gameObject.SetActive(false);
    }

}