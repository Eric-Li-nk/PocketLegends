using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    
    public Character player;
    
    [Header("States")] 
    public bool fly = false;
    
    // Movement variables
    private float moveSpeed;
    private float moveSpeedFly;
    
    private float groundDrag;
    private float airDrag;

    private float jumpForce;
    private float jumpCooldown;
    private float airMultiplier;
    private bool readyToJump;

    [Header("Keybinds")] 
    public KeyCode jumpKey = KeyCode.Space;
    
    [Header("Ground Check")] 
    public LayerMask whatIsGround;
    public float playerHeight;
    [SerializeField] private float distanceToGround;
    [SerializeField] private bool grounded;

    [Header("Slope Handling")] 
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;
    
    public Transform orientation;

    private float horizontalInput;
    private float verticalInput;
    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;
    

    private Vector3 moveDirection;

    private Rigidbody rb;

    private void Awake()
    {
        moveSpeed = player.maxSpeed;
        moveSpeedFly = player.maxFlySpeed;
        groundDrag = player.groundDrag;
        airDrag = player.airDrag;
        jumpForce = player.jumpForce;
        jumpCooldown = player.jumpCooldown;
        airMultiplier = player.airMultiplier;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        ResetJump();
    }

    private void Update()
    {
        // Ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + distanceToGround, whatIsGround);

        MyInput();  
        SpeedControl();
        
        // Handle drag
        if (grounded || OnSlope())
            rb.drag = groundDrag;
        else if (fly)
            rb.drag = airDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = movementInput.x;
        verticalInput = movementInput.y;
        
        // when to jump
        if (jumped && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumped = context.action.triggered;
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (fly)
        {
            // When the player flies, gravity is disabled
            rb.useGravity = false;
            rb.AddForce(moveDirection.normalized * (moveSpeed * 10f), ForceMode.Force);
        }
        else if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * (moveSpeed * 20f), ForceMode.Force);
            // Push the player down in order to keep the player on the slope
            if(rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }
        else if(grounded)
            rb.AddForce(moveDirection.normalized * (moveSpeed * 10f), ForceMode.Force);
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * (moveSpeed * 10f * airMultiplier), ForceMode.Force);
        
        if(!fly)
            rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }
        else
        {
            Vector3 faltVel;
            
            if(!fly)
                faltVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            else
                faltVel = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
            
            // Limit velocity if needed
            if (faltVel.magnitude > moveSpeedFly)
            {   
                Vector3 limitedVel = faltVel.normalized * moveSpeed;
                if(!fly)
                    rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
                else
                    rb.velocity = new Vector3(limitedVel.x, limitedVel.y, limitedVel.z);
            }
        }
        
    }

    private void Jump()
    {
        exitingSlope = true;
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
        exitingSlope = false;
    }

    // Checks if the player is on a slope
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + distanceToGround))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }
    
    // Gets the angle of the slope by using normal
    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}
