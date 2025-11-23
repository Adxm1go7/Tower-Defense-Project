using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelNodeText : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Camera.main.transform.position - transform.position; // Get camera direction

        dir.x = 0f;   // Remove left and right tilting of text

        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(dir); // Rotates text to face camera direction
        }

        transform.Rotate(0, 180, 0); // Flips text to correct orientation ( back to camera )
        
    }
}
