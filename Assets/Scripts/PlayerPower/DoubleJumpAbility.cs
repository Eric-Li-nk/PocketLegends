using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;

public class DoubleJumpAbility : MonoBehaviour
{ 
    Rigidbody rb;
    public float jumpHeight = 10;
    public bool grounded;
    public int maxJumpCount = 2;
    public int jumpsRemaining = 0;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>(); // récupère le component Rigidbody sur le joueur
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0) // appui sur la touche de saut et sauts restants
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse); // code de saut ici
            jumpsRemaining -= 1;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor") // condition si le sol est touché
        {
            grounded = true;
            jumpsRemaining = maxJumpCount;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor") // condition si le sol n'est plus touché
        {
            grounded = false;
        }
    }
}