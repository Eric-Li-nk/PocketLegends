using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotNavMesh : MonoBehaviour
{
    [SerializeField] private Transform[] movePositionTransforms;  // Tableau des positions de mouvement
    private NavMeshAgent navMeshAgent;
    private int currentPositionIndex = 0;  // Index de la position actuelle

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if (movePositionTransforms.Length > 0)
            SetDestination(movePositionTransforms[currentPositionIndex]);
    }

    private void Update()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
        {
            // Arrivé à la position actuelle, passer à la suivante
            currentPositionIndex++;
            if (currentPositionIndex >= movePositionTransforms.Length)
                currentPositionIndex = 0;
            
            SetDestination(movePositionTransforms[currentPositionIndex]);
        }
    }

    private void SetDestination(Transform targetTransform)
    {
        navMeshAgent.destination = targetTransform.position;
    }
}   