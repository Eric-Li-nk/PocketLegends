using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavMesh : MonoBehaviour
{
    //[SerializeField] private Transform movePositionTransform;
    public List<Transform> checkpointTransforms;
    public bool loopCircuit;

    private NavMeshAgent navMeshAgent;
    public int currentCheckpointIndex = 0;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        SetDestinationToCurrentCheckpoint();
        
        // On attend que le navMeshAgent termine de trouver la route
        if (navMeshAgent.pathPending)
            return;
        
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            
            currentCheckpointIndex++;

            if (currentCheckpointIndex >= checkpointTransforms.Count)
            {
                if (loopCircuit)
                    currentCheckpointIndex = 0;
                else
                    currentCheckpointIndex = checkpointTransforms.Count - 1;
            }

            SetDestinationToCurrentCheckpoint();
        }
    }

    private void SetDestinationToCurrentCheckpoint()
    {
        navMeshAgent.destination = checkpointTransforms[currentCheckpointIndex].position;
    }
}
