using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCam : MonoBehaviour
{
    public Camera mainCamera;
    
    public float sensX;
    public float sensY;
    private float xRotation;
    private float yRotation;
    
    public Transform orientation;
    public Transform playerObj;

    private Vector2 lookInput = Vector2.zero;

    private LayerMask playerLayerMask;

    private void Awake()
    {
        playerLayerMask = gameObject.layer + 4;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        mainCamera.cullingMask &= ~(1 << playerLayerMask);
    }

    private void OnDisable()
    {
        mainCamera.cullingMask |= 1 << playerLayerMask;
    }

    private void Update()
    {

        float mouseX = lookInput.x * Time.deltaTime * sensX;
        float mouseY = lookInput.y * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        // rotates camera and orientation of player
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        playerObj.forward = orientation.forward;
    }
    
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
}
