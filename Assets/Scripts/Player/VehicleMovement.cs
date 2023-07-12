using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VehicleMovement : MonoBehaviour
{
    [SerializeField] private Text SpeedText;
    public Transform centerOfMass;
    
    public float carMass = 500f;
    public float carDrag = 0.05f;
    
    private Wheel[] wheels;

    [Header("Acceleration")]
    public float motorTorque = 100f;
    [Header("Maximum degree of turning")]
    public float maxSteer = 20f;
    
    public float ThrottleInput { get; private set; }
    public float SteerInput { get; private set; }
    
    private Rigidbody rb;
    
    private Vector2 movementInput = Vector2.zero;

    public void Awake()
    {
        wheels = GetComponentsInChildren<Wheel>(true);
        rb = GetComponent<Rigidbody>();
    }

    public void OnEnable()
    {
        rb.mass = carMass;
        rb.drag = carDrag;
        rb.freezeRotation = false;
        rb.centerOfMass = centerOfMass.localPosition;
    }

    public void FixedUpdate()
    {
        SteerInput = movementInput.x;
        ThrottleInput = movementInput.y;
        foreach (var wheel in wheels)
        {
            wheel.SteerAngle = SteerInput * maxSteer;
            wheel.Torque = ThrottleInput * motorTorque;
        }
    }

    public void Update()
    {
        SpeedText.text = (rb.velocity.magnitude * 2.23693629f).ToString("0") + (" m/h");
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
}