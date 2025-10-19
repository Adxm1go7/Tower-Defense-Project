using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody rb;
    [SerializeField] private float speed;

    [Space]

    [SerializeField] GameObject stepRayUpper;
    [SerializeField] GameObject stepRayLower;
    [SerializeField] float stepHeight = 0.3f;
    [SerializeField] float stepSmooth = 0.1f;






    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 8;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized * speed;
        rb.velocity = move;

        stepClimb();
    }

    void stepClimb(){
        RaycastHit hitLower;
        if (Physics.Raycast(stepRayLower.transform.position, rb.velocity.normalized, out hitLower, 0.3f)){
            RaycastHit hitUpper;
            if (!Physics.Raycast(stepRayUpper.transform.position, rb.velocity.normalized, out hitUpper, 0.4f)){
                rb.position -= new Vector3(0f, -stepSmooth, 0f);
            }
        }
    }
}
