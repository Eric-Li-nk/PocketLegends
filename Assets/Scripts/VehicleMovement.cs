using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleMovement : MonoBehaviour
{
    [SerializeField] private Text SpeedText;
    public Transform centerOfMass;
    
    public WheelCollider wheelColliderLeftFront;
    public WheelCollider wheelColliderRightFront;
    public WheelCollider wheelColliderLeftBack;
    public WheelCollider wheelColliderRightBack;
    
    public Transform wheelLeftFront;
    public Transform wheelRightFront;
    public Transform wheelLeftBack;
    public Transform wheelRightBack;

    [Header("Acceleration")]
    public float motorTorque = 100f;
    [Header("Maximum degree of turning")]
    public float maxSteer = 20f;
    private Rigidbody rb;
    
    

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Keeps the vehicle from falling over
        rb.centerOfMass = centerOfMass.localPosition;
    }

    public void FixedUpdate()
    {
        wheelColliderLeftBack.motorTorque = Input.GetAxis("Vertical") * motorTorque;
        wheelColliderRightBack.motorTorque = Input.GetAxis("Vertical") * motorTorque;
        wheelColliderLeftFront.steerAngle = Input.GetAxis("Horizontal") * maxSteer;
        wheelColliderRightFront.steerAngle = Input.GetAxis("Horizontal") * maxSteer;
    }

    public void Update()
    {
        // Makes the wheels model turn
        var pos = Vector3.zero;
        var rot = Quaternion.identity;
        
        wheelColliderLeftFront.GetWorldPose(out pos, out rot);
        wheelLeftFront.position = pos;
        wheelLeftFront.rotation = rot;
        
        wheelColliderRightFront.GetWorldPose(out pos, out rot);
        wheelRightFront.position = pos;
        wheelRightFront.rotation = rot;
        
        wheelColliderLeftBack.GetWorldPose(out pos, out rot);
        wheelLeftBack.position = pos;
        wheelLeftBack.rotation = rot;
        
        wheelColliderRightBack.GetWorldPose(out pos, out rot);
        wheelRightBack.position = pos;
        wheelRightBack.rotation = rot;

        SpeedText.text = (rb.velocity.magnitude * 2.23693629f).ToString("0") + (" m/h");
    }
}