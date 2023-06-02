using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IComparable<Character>
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
    public int rank;

    // Ajout des variables nécessaires pour le mouvement
    public bool isGrounded; // Pour détecter si le bot est au sol
    public Rigidbody rigidbody; // Pour gérer la physique du mouvement

    public int CompareTo(Character pl)
    {
        var res = pl.currentLap.CompareTo(currentLap);
        if (res == 0)
            res = pl.currentCheckpoint.CompareTo(currentCheckpoint);
        if (res == 0)
            res = distanceToNextCheckpoint.CompareTo(pl.distanceToNextCheckpoint);
        return res;
    }

    private void Awake()
    {
        // Récupérer le composant Rigidbody attaché à l'objet
        rigidbody = GetComponent<Rigidbody>();
    }
}