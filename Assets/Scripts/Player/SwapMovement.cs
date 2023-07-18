using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum MovementType
{
    Walk,
    Drive,
    Fly
}
[RequireComponent(typeof(PlayerInput),typeof(PlayerMovement),typeof(VehicleMovement))]
public class SwapMovement : MonoBehaviour
{

    private MovementType movementType;
    public MovementType MovementType
    {
        get
        {
            return movementType;
        }
        set
        {
            if (movementType != value)
            {
                movementType = value;
                SwapMovementType(movementType);
            }
        }
    }
    
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private VehicleMovement vehicleMovement;
    
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private GameObject carGameObject;

    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject carCamera;
    
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        vehicleMovement = GetComponent<VehicleMovement>();
    }

    private void SwapMovementType(MovementType type)
    {
        if( type == MovementType.Walk)
            SwapToWalk();
        else if( type == MovementType.Drive)
            SwapToDrive();
    }

    private void SwapToWalk()
    {
        playerInput.SwitchCurrentActionMap("PlayerControl");
        vehicleMovement.enabled = false;
        playerMovement.enabled = true;
        carGameObject.SetActive(false);
        carCamera.SetActive(false);
        playerGameObject.SetActive(true);
        playerCamera.SetActive(true);
    }

    private void SwapToDrive()
    {
        playerInput.SwitchCurrentActionMap("CarControl");
        playerMovement.enabled = false;
        vehicleMovement.enabled = true;
        playerGameObject.SetActive(false);
        playerCamera.SetActive(false);
        transform.rotation = playerGameObject.transform.rotation;
        carGameObject.SetActive(true);
        carCamera.SetActive(true);
    }
}
