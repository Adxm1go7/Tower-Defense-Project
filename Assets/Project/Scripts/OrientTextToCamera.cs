using UnityEngine;

public class OrientTextToCamera : MonoBehaviour
{
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        Vector3 dir = cam.transform.position - transform.position;
        dir.x = 0f; 
        transform.rotation = Quaternion.LookRotation(-dir);
    }
}
