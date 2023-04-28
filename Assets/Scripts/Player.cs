using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // This class is used to hold most of the player variables

    [Header("Movement variables")] 
    public float maxSpeed;
    public float maxFlySpeed;
    public float jumpForce;
    public float jumpCoolddown;
    public float airMultiplier;
    
    [Header("Drag variables")]
    public float groundDrag;
    public float airDrag;
    
    [Header("Race track variables")]
    public int currentLap;
    public int currentCheckpoint;

}
