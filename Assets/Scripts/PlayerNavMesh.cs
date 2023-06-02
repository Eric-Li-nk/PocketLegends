using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavMesh : MonoBehaviour
{
    //[SerializeField] private Transform movePositionTransform;
    [SerializeField] private List<Transform> checkpointTransforms;
    [SerializeField] private bool loopCircuit = true;

    private NavMeshAgent navMeshAgent;
    private int currentCheckpointIndex;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    
        if (loopCircuit)
        {
            currentCheckpointIndex = 0;
        }
        else
        {
            currentCheckpointIndex = checkpointTransforms.Count - 1;
        }
    }

    private void Update()
    {
        if (checkpointTransforms.Count == 0)
        {
            Debug.LogWarning("No checkpoints set!");
            return;
        }

        SetDestinationToCurrentCheckpoint();

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
