using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCam : MonoBehaviour, AxisState.IInputAxisProvider
{
    [Header("References")] 
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    private bool reverseView = false; // variable pour inverser la vue

    [HideInInspector] public InputAction horizontal;
    [HideInInspector] public InputAction vertical;
    [HideInInspector] public InputAction objOrientation;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // inverser la vue si la touche "o" est enfoncée
        if (Input.GetKeyDown(KeyCode.O))
        {
            reverseView = !reverseView;
        }

        // déterminer la direction de la vue en fonction de la variable "reverseView"
        Vector3 viewDir = reverseView ? -(player.position - new Vector3(transform.position.x, player.position.y, transform.position.z)) : player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        // rotate player object
        float horizontalInput = objOrientation.ReadValue<Vector2>().x;
        float verticalInput = objOrientation.ReadValue<Vector2>().y;
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (inputDir != Vector3.zero)
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
    }

    public float GetAxisValue(int axis)
    {
        switch (axis)
        {
            case 0: return horizontal.ReadValue<Vector2>().x;
            case 1: return horizontal.ReadValue<Vector2>().y;
            case 2: return vertical.ReadValue<float>();
        }

        return 0;
    }
}