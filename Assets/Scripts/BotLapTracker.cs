using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BotLapTracker : MonoBehaviour
{

    [Serialize] private int currentLap;
    [Serialize] private int currentCheckpoint;
    public int totalCheckpoint;

    private int outOfBound;
    private int checkpointLayer;

    private Character character;

    public bool raceIsLoop = false;
    public int totalLap;
    
    private void Awake()
    {
        outOfBound = LayerMask.NameToLayer("OutOfBound");
        checkpointLayer = LayerMask.NameToLayer("Checkpoint");
        character = transform.GetComponent<Character>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == checkpointLayer)
        {
            int checkpointName = Convert.ToInt32(other.gameObject.transform.parent.name);
            int nextCheckpoint = GetNextCheckpoint();
            // If it is the next checkpoint, increment the current checkpoint by 1
            if( nextCheckpoint == checkpointName )
            {
                currentCheckpoint = nextCheckpoint;
                character.currentCheckpoint = currentCheckpoint;
                if (currentCheckpoint == totalCheckpoint && raceIsLoop)
                {
                    currentLap++;
                    character.currentLap = currentLap;
                }
            }
        }
        // Resets the position of the player if he is out of bounds to the last checkpoint
        else if(other.gameObject.layer == outOfBound)
            OutOfBounds();
    }

    private void OutOfBounds()
    {
        if (currentCheckpoint == 0 && currentLap == 0)
            transform.position = GameObject.Find("Spawn").transform.position;
        else
            transform.position = GameObject.Find("Checkpoints").transform.Find(currentCheckpoint.ToString()).position;
    }

    private int GetNextCheckpoint()
    {
        if (currentCheckpoint == totalCheckpoint)
            return 1;
        return currentCheckpoint + 1;
    }
}
