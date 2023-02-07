using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Change in the script
    private float moveSpeed;

    // change in Unity
    [Header("Forward Movement")]
    public float walkSpeed;
    public float sprintSpeed;

    public float groundDrag;
    public bool bGrounded; 

    [Header("Keysbinds")]
    public KeyCode sprintKey = KeyCode.LeftShift;

    public Transform orientation;

    //private
    private float horizontalInput;
    private float verticalInput;
    private Vector3 movementDirection;
    private Rigidbody rb;

    [Header("States")]
    public MovementState state;

    public enum MovementState
    {
        walking,
        sprinting,
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.freezeRotation = true;
    }

    private void Update()
    {
        MyInput();
        SpeedControl();
        StateHandler();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void StateHandler()
    {
        // State --> Sprinting
        if (bGrounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        // State --> Walking
        else if (bGrounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
    }

    private void MovePlayer()
    {
        // You will allways walk in the direction you look at. 
        movementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // On the Ground
        if (bGrounded)
            rb.AddForce(movementDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // Get a limit on the velocity
        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitVelocity = flatVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitVelocity.x, rb.velocity.y, limitVelocity.z);
        }
    }
}