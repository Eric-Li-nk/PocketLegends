using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // This class is used to hold most of the player variables

    [Header("Check if player")] 
    public bool isPlayer;
    
    [Header("Movement variables")] 
    public float maxSpeed;
    public float maxFlySpeed;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    
    [Header("Drag variables")]
    public float groundDrag;
    public float airDrag;
    
    [Header("Race track variables")]
    public int currentLap;
    public int currentCheckpoint;

    public float distanceToNextCheckpoint;

}
