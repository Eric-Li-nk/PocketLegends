using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSwapper : MonoBehaviour
{
    [SerializeField]
    private MovementType changeInto;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SwapMovement swapMovement = other.GetComponentInParent<SwapMovement>();
            swapMovement.MovementType = changeInto;
        }
    }

}
