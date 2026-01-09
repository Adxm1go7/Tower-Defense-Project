using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed = 720f;
    private Animator anim;

    [Space]

    [SerializeField] GameObject stepRayUpper;
    [SerializeField] GameObject stepRayLower;

    //[SerializeField] float stepHeight = 0.3f;
    [SerializeField] float stepSmooth = 0.1f;

    [SerializeField] private LayerMask pathLayer;

    Vector3 movementVector;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 8;
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetVelocity = movementVector.normalized * speed;
        rb.velocity = new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.z);
        if (movementVector.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementVector);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime);
        }
        if (anim != null)
        {
            anim.SetFloat("Speed", targetVelocity.magnitude);
        }
        stepClimb();
    }

    void OnMove(InputValue moveValue){
        Vector2 moveVector = moveValue.Get<Vector2>();
        movementVector = new Vector3 (moveVector.x, 0f, moveVector.y);
    }

    void stepClimb(){
        if (movementVector.magnitude < 0.1f)
        return;

        Vector3 moveDir = movementVector;
        moveDir.y = 0f;
        moveDir.Normalize();

        Vector3[] directions = 
        {
            moveDir,
            Quaternion.AngleAxis(45f, Vector3.up) * moveDir,
            Quaternion.AngleAxis(-45f, Vector3.up) * moveDir
        };

        foreach (Vector3 dir in directions)
        {
            RaycastHit hitLower;
            if (Physics.Raycast(stepRayLower.transform.position, dir, out hitLower, 0.5f, pathLayer))
            {
                RaycastHit hitUpper;
                if (!Physics.Raycast(stepRayUpper.transform.position, dir, out hitUpper, 0.4f, pathLayer))
                {
                    rb.position += Vector3.up * stepSmooth;
                    break; // prevent double step in one frame
                }
            }
        }
    }
}
